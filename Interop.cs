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

		public delegate object functionDelegate(object context, IContainer[] paramArray);

		HDFVisitor collector = new HDFVisitor();
		public void registerFunction(object context, functionDelegate del, string definition)
		{
			SymbolTable.classDef cDef = (context as SymbolTable.classDef) ?? STable.getGlobalCDef();

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

		public object getVariable(IContainer container, string name)
		{
			return getVariable(container?.value, name);
		}

		public object getVariable(IContainer container, int index)
		{
			return getVariable(container?.value, index.ToString());
		}

		public object getVariable(object context, int index)
		{
			return getVariable(context, index.ToString());
		}

		public object getVariable(object context, string name)
		{
			return STable.getVariable((context as SymbolTable.classDef) ?? STable.getGlobalCDef(), name)?.value;
		}

		public void setVariable(IContainer container, int index, object value)
		{
			setVariable(container?.value, index.ToString(), value);
		}

		public void setVariable(IContainer container, string name, object value)
		{
			setVariable(container?.value, name, value);
		}

		public void setVariable(object context, int index, object value)
		{
			setVariable(context, index.ToString(), value);
		}

		public void setVariable(object context, string name, object value)
		{
			STable.setVariable((context as SymbolTable.classDef) ?? STable.getGlobalCDef(), name, value);
		}

		internal MosesVisitor MVisitor = null;

        public IContainer invokeFunction(object context, string name, IContainer[] args)
		{
			var cDef = (context as SymbolTable.classDef) ?? STable.getGlobalCDef();
            var function = STable.getFunction(cDef, name, args.Count());
			if (function == null)
				return null;
			if (function._delegate != null)
			{
				object val = function._delegate(context, args);
				return new IContainer() { value = val, vType = getVarTypeImmediate(val) };
			}
			else
			{
				var paramList = prepareParams(args, function).ToArray();
				MVisitor.cDef = cDef;
				object val = MVisitor.execUDF(paramList, function);
				return new IContainer() { value = val, vType = getVarTypeImmediate(val) };
			}
		}


		//clone of VisitorFUnction.prepareParams. Kept separate for performance issues.
		internal List<SymbolTable.variable> prepareParams(IContainer[] args, SymbolTable.functionDef fDef)
		{
			var paramList = new List<SymbolTable.variable>();
			int final = fDef.isVariadic ? fDef.functionParamterList.Count - 1 : fDef.functionParamterList.Count;

			for (int i = 0; i < final; i++)
			{
				object val = i < args.Count() ? args[i].value : fDef.functionParamterList[i].defaultValue;
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

		public object newClassDef(object context, string name)
		{ return STable.addClass(context as SymbolTable.classDef, name); }

		public object getClassDef(object context, string name)
		{ return STable.getCDefFromCDef(context as SymbolTable.classDef, name); }

		public object createInstance(object context, string name)
		{ return STable.createInstance(context as SymbolTable.classDef, name); }
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
