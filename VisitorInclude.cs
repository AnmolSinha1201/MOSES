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
		public override object VisitIncludeBlock([NotNull] MosesParser.IncludeBlockContext context)
		{
			string fileName = Visit(context.exp()).ToString();
			var runtime = new Runtime(fileName, STable);
			if (runtime == null)
			{ } //already included
			runtime.execute();
			return base.VisitIncludeBlock(context);
		}
	}
}
