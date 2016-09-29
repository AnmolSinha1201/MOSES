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

			List<MosesParser.ExpContext> expList = new List<MosesParser.ExpContext>();
			for (int i = 0; i < context.exp().Count(); i++)
				expList.Add(context.exp(i));
			
			var function = STable.getFunction(instance, "__new", expList.Count());
			var args = ToContainerArgs(expList, function?.functionParamterList);
            return invokeConstructor(instance, args) ?? instance;
		}

		public object invokeConstructor(SymbolTable.classDef cDef, Interop.IContainer[] args)
		{
			if (!cDef.__new)
				return null;
			var function = STable.getFunction(cDef, "__new", (args?.Count() ?? 0));
			if ((args?.Count() ?? 0) == 0 && function == null)
				return null;
			var retVal = interop.invokeFunction(cDef, "__new", args);
			return retVal.value;
		}

		public void invokeDestructor(SymbolTable.classDef oldValue, SymbolTable.classDef newValue)
		{
			if (oldValue != null)
			{
				oldValue.referenceCount--;
				if (oldValue.referenceCount == 0 && oldValue.__delete)
					interop.invokeFunction(oldValue, "__delete", null);
			}
			if (newValue != null)
				newValue.referenceCount++;
		}

		public Interop.IContainer invokeMetaCall(SymbolTable.classDef cDef, string functionName, Interop.IContainer[] args)
		{
			var function = STable.getFunction(cDef, "__call", 2);
			if (function == null)
				return null;

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

		public void invokeMetaSet(SymbolTable.classDef cDef, string varName, object value)
		{
			var function = STable.getFunction(cDef, "__set", 2);
			if (function == null)
				return;
			var metaArgs = new Interop.IContainer[]
			{
				new Interop.IContainer() {vType = Interop.variableType.STRING, value = varName },
				new Interop.IContainer() {vType = STable.getVarTypeLazy(value), value = value }
			};
			interop.invokeFunction(cDef, function, metaArgs);
		}

		public Interop.IContainer invokeMetaGet(SymbolTable.classDef cDef, string varName)
		{
			var function = STable.getFunction(cDef, "__get", 1);
			if (function == null)
				return null;
			var metaArgs = new Interop.IContainer[]
			{
				new Interop.IContainer() {vType = Interop.variableType.STRING, value = varName }
			};
			return interop.invokeFunction(cDef, function, metaArgs);
		}
	}
}
