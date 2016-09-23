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

		internal object invokeConstructor(SymbolTable.classDef cDef, Interop.IContainer[] args)
		{
			if (!cDef.__new)
				return null;
			var function = STable.getFunction(cDef, "__new", (args?.Count() ?? 0));
			if ((args?.Count() ?? 0) == 0 && function == null)
				return null;
			var retVal = interop.invokeFunction(cDef, "__new", args);
			return retVal.value;
		}

		internal void invokeDestructor(SymbolTable.classDef cDef)
		{
			if (cDef.__delete)
				interop.invokeFunction(cDef, "__delete", null);
		}
	}
}
