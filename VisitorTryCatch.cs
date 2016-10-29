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
		public override object VisitTryCatchFinally([NotNull] MosesParser.TryCatchFinallyContext context)
		{
			try { Visit(context.segmentBlock(0)); }
			catch(Exception e)
			{
				if (context.CATCH() != null)
				{
					if (context.NAME() != null)
						STable.setVariable(null, (string)Visit(context.NAME()), e.ToString());
					Visit(context.segmentBlock(1));
				}
			}
			finally { if (context.FINALLY() != null) Visit(context.segmentBlock(2)); }

			return base.VisitTryCatchFinally(context);
		}
	}
}
