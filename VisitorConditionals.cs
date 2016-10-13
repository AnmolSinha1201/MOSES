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
		public override object VisitIfElseLadder([NotNull] MosesParser.IfElseLadderContext context)
		{
			if (Helper.isTrue(Visit(context.exp())))
				Visit(context.segmentBlock(0));
			else if (context.segmentBlock().Count() == 2)
				Visit(context.segmentBlock(1));

			return null;
		}

		public override object VisitExpTernary([NotNull] MosesParser.ExpTernaryContext context)
		{
			if (Helper.isTrue(Visit(context.exp(0))))
				return Visit(context.exp(1));
			return Visit(context.exp(2));
		}

		public override object VisitExpNullCoalesce([NotNull] MosesParser.ExpNullCoalesceContext context)
		{
			var val = Visit(context.exp(0));
			if (Helper.isTrue(val))
				return val;
			return Visit(context.exp(1));
		}
	}
}
