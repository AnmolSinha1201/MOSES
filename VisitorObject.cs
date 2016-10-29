using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace MOSES
{
	partial class MosesVisitor : MosesBaseVisitor<object>
	{
		public override object VisitNewInstance([NotNull] MosesParser.NewInstanceContext context)
		{
			Visit(context.complexVariable());
			var instance = STable.createInstance(cDef, vName);
			if (instance == null)
				EHandler.throwScriptError(context.Start.Line, context.Start.Column, context.Parent.GetText(), ErrorHandler.ClassNotExist + vName);

			if (!instance.__new)
				return instance;

			List<MosesParser.ExpContext> expList = new List<MosesParser.ExpContext>();
			for (int i = 0; i < context.exp().Count(); i++)
				expList.Add(context.exp(i));
			return invokeConstructor(instance, expList);
		}

		public override object VisitNewArray([NotNull] MosesParser.NewArrayContext context)
		{
			var retVal = new SymbolTable.classDef();
			for (int i = 0; i < context.exp().Count(); i ++)
				STable.setVariable(retVal, i.ToString(), Visit(context.exp(i)));
			return retVal;
		}

		public override object VisitNewDictionary([NotNull] MosesParser.NewDictionaryContext context)
		{
			var retVal = new SymbolTable.classDef();
			for (int i = 0; i < context.exp().Count(); i += 2)
				STable.setVariable(retVal, Visit(context.exp(i))?.ToString(), Visit(context.exp(i + 1)));
			return retVal;
		}

		public object invokeConstructor(SymbolTable.classDef cDef, List<MosesParser.ExpContext> expList)
		{
			var function = STable.getFunction(cDef, "__new", expList.Count());
			if (function == null && expList.Count == 0) //to allow default constructor
				return null;
			if (function == null)
				EHandler.throwScriptError(0, 0, null, ErrorHandler.FunctionNotExist + $"__new({expList.Count})");
			var args = ToContainerArgs(expList, function?.functionParamterList);
			var retVal = interop.invokeFunction(cDef, function, args);
			return retVal?.value?? cDef;
		}

		public Interop.IContainer invokeDestructor(SymbolTable.classDef oldValue, SymbolTable.classDef newValue)
		{
			Interop.IContainer retVal = null;
			if (oldValue != null)
			{
				oldValue.referenceCount--;
				if (oldValue.referenceCount == 0 && oldValue.__delete)
				{
					var function = STable.getFunction(cDef, "__delete", 2);
					retVal = interop.invokeFunction(oldValue, function, null);
				}
			}
			if (newValue != null)
				newValue.referenceCount++;
			return retVal;
		}

		public Interop.IContainer invokeMetaCall(SymbolTable.classDef cDef, string functionName, Interop.IContainer[] args)
		{
			var function = STable.getFunction(cDef, "__call", 2);

			var argsObject = new SymbolTable.classDef();
			for (int i = 0; i < args.Count(); i++)
				argsObject.varTable.Add(i.ToString(), (SymbolTable.variable)args[i]);

			var metaArgs = new Interop.IContainer[]
			{
				new Interop.IContainer() {vType = Interop.variableType.STRING, value = functionName },
				new Interop.IContainer() {vType = Interop.variableType.OBJECT, value = argsObject }
			};
			return interop.invokeFunction(cDef, function, metaArgs);
		}

		public Interop.IContainer invokeMetaSet(SymbolTable.classDef cDef, string varName, object value)
		{
			var function = STable.getFunction(cDef, "__set", 2);

			var metaArgs = new Interop.IContainer[]
			{
				new Interop.IContainer() {vType = Interop.variableType.STRING, value = varName },
				new Interop.IContainer() {vType = STable.getVarTypeLazy(value), value = value }
			};
			return interop.invokeFunction(cDef, function, metaArgs);
		}

		public Interop.IContainer invokeMetaGet(SymbolTable.classDef cDef, string varName)
		{
			var function = STable.getFunction(cDef, "__get", 1);

			var metaArgs = new Interop.IContainer[]
			{
				new Interop.IContainer() {vType = Interop.variableType.STRING, value = varName }
			};
			return interop.invokeFunction(cDef, function, metaArgs);
		}
	}
}
