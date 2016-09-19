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
			
			for (int i = 0; i < context.exp().Count(); i++)
			return base.VisitNewInstance(context);
			return null;
		}

		internal object invokeConstructor(SymbolTable.classDef cDef, Interop.IContainer[] args)
		{
			var function = STable.getFunction(cDef, "__new", args.Count());
			if (args.Count() == 0 && function == null)
				return null;
			var retVal = interop.invokeFunction(cDef, "__new", args);
			return retVal.value;
		}
	}
}
