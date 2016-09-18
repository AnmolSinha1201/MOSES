using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace MOSES
{
	public class Interop
	{
		internal SymbolTable STable = null;
		public class IContainer
		{
			public object value;
			public variableType vType;
		}

		public enum variableType
		{
			INT, DOUBLE, STRING, OBJECT, NONE
		}

		void toImmediateType(IContainer container)
		{
			variableType t = getVarTypeImmediate(container);
			container.vType = t;
			if (t == variableType.INT)
				container.value = Convert.ToInt64(container.value);
			else if (t == variableType.DOUBLE)
				container.value = Convert.ToDouble(container.value);
			else if (t == variableType.STRING)
				container.value = Convert.ToString(container.value);
		}

		public variableType getVarTypeImmediate(IContainer container)
		{
			return getVarTypeImmediate(container.value);
		}

		public variableType getVarTypeImmediate(object val)
		{
			if (val == null)
				return variableType.NONE;
			if ((val as Int64?) != null || (val as Int32?) != null)
				return variableType.INT;
			if ((val as double?) != null || (val as float?) != null)
				return variableType.DOUBLE;
			if ((val as string) != null)
				return variableType.STRING;
			return variableType.OBJECT;
		}

		public delegate object functionDelegate(int? instance, IContainer[] paramArray);


		List<object> objectBuffer = new List<object>();
		internal void wrapParameters(IContainer[] containerArray)
		{
			foreach (var v in containerArray)
			{
				toImmediateType(v);
				if (v.vType == variableType.OBJECT)
					wrap(v);
			}
		}

		internal int wrap(IContainer container)
		{
			objectBuffer.Add(container.value);
			container.value = objectBuffer.Count - 1;
			return (int)container.value;
		}

		internal int wrap(SymbolTable.classDef cDef)
		{
			objectBuffer.Add(cDef);
			return objectBuffer.Count - 1;
		}

		IContainer safeWrap(IContainer container)
		{
			if (getVarTypeImmediate(container) == variableType.OBJECT)
				wrap(container);
			return container;
		}

		HDFVisitor collector = new HDFVisitor();
		public void registerFunction(int? instanceID, functionDelegate del, string definition)
		{
			SymbolTable.classDef cDef = null;
			if (instanceID.HasValue)
				cDef = (SymbolTable.classDef)objectBuffer[instanceID.Value];

			var input = new AntlrInputStream(definition);
			var lexer = new MosesLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			var parser = new MosesParser(tokens);
			IParseTree tree = parser.chunk();
			
			collector.STable = STable;
			collector.cDef = cDef;
			collector.funcDel = del;
			collector.Visit(tree);
		}

		public IContainer getVariable(IContainer container, string name)
		{
			return getVariable((int?)container?.value, name);
		}

		public IContainer getVariable(int contextID, string name)
		{
			return getVariable(contextID, name);
		}
		public IContainer getVariable(IContainer container, int index)
		{
			return getVariable((int?)container?.value, index.ToString());
		}

		public IContainer getVariable(int contextID, int index)
		{
			return getVariable(contextID, index.ToString());
		}

		IContainer getVariable(int? contextID, string name)
		{
			return STable.getVariable(contextID.HasValue? (SymbolTable.classDef)objectBuffer[contextID.Value] : STable.getGlobalCDef(), name);
		}

		public void setVariable(IContainer container, int index, object value)
		{
			setVariable((int?)container?.value, index.ToString(), value);
		}

		public void setVariable(IContainer container, string name, object value)
		{
			setVariable((int?)container?.value, name, value);
		}

		void setVariable(int? contextID, string name, object value)
		{
			STable.setVariable(contextID.HasValue ? (SymbolTable.classDef)objectBuffer[contextID.Value] : STable.getGlobalCDef(), name, value);
		}

		internal MosesVisitor MVisitor = null;
		IContainer invokeFunction(int? contextID, string name, IContainer[] args)
		{
			var cDef = contextID.HasValue ? (SymbolTable.classDef)objectBuffer[contextID.Value] : STable.getGlobalCDef();
            var function = STable.getFunction(cDef, name, args.Count());
			if (function._delegate != null)
			{ }
			else
			{
				var paramList = prepareParams(args, function);
				MVisitor.cDef = cDef;
				object val = MVisitor.execUDF(paramList, function);
				IContainer container = new IContainer() { value = val, vType = getVarTypeImmediate(val) };
				return container;
			}
			return null;
		}


		//clone of VisitorFUnction.prepareParams. Kept separate for performance issues.
		List<SymbolTable.variable> prepareParams(IContainer[] args, SymbolTable.functionDef fDef)
		{
			var paramList = new List<SymbolTable.variable>();
			int final = fDef.isVariadic ? fDef.functionParamterList.Count - 1 : fDef.functionParamterList.Count;

			for (int i = 0; i < final; i++)
			{
				object val = i < args.Count() ? args[i] : fDef.functionParamterList[i].defaultValue;
				SymbolTable.variable var = null;
				if (fDef.functionParamterList[i].byRef)
					var = (SymbolTable.variable)args[i];

				if (var == null)
				{
					var = new SymbolTable.variable();
					var.value = val; var.vType = i < args.Count() ? args[i].vType : STable.getVarTypeLazy(val);
				}
				paramList.Add(var);
			}
			if (!fDef.isVariadic)
				return paramList;

			var variadic = new SymbolTable.classDef();
			for (int i = final; i < args.Count(); i++)
				STable.setVariable(variadic, (i - final).ToString(), args[i]);
			paramList.Add(new SymbolTable.variable() { value = variadic, vType = Interop.variableType.OBJECT });
			return paramList;
		}
	}


	class HDFVisitor : MosesBaseVisitor<object>
	{
		internal SymbolTable.classDef cDef = null;
		internal SymbolTable STable = null;
		internal Interop.functionDelegate funcDel = null;

		public override object VisitFunctionDef([NotNull] MosesParser.FunctionDefContext context)
		{
			var parameterList = new List<SymbolTable.functionDef.functionParameter>();
			var contextPList = context.functionParameterList();
			bool isVariadic = false;

			int iters = contextPList == null ? 0 : contextPList.functionParameterNoDefault().Count();
			for (int i = 0; i < iters; i++)
			{
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionParameterNoDefault(i).NAME().ToString(),
					byRef = contextPList.functionParameterNoDefault(i).@ref() != null
				};
				parameterList.Add(fParam);
			}

			iters = contextPList == null ? 0 : contextPList.functionParameterDefault().Count();
			for (int i = 0; i < iters; i++)
			{
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionParameterDefault(i).NAME().ToString(),
					defaultValue = Visit(contextPList.functionParameterDefault(i).constExp()).ToString(),
					byRef = contextPList.functionParameterDefault(i).@ref() != null
				};
				parameterList.Add(fParam);
			}

			if (contextPList?.functionPrameterVariadic() != null)
			{
				isVariadic = true;
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionPrameterVariadic().NAME().ToString()
				};
				parameterList.Add(fParam);
			}

			var function = new SymbolTable.functionDef()
			{
				_delegate = funcDel,
				functionParamterList = parameterList,
				isVariadic = isVariadic,
				minParamCount = contextPList == null ? 0 : contextPList.functionParameterNoDefault().Count()
			};
			
			STable.addFunction(cDef, context.NAME().ToString(), function);
			return false;
		}
	}
}
