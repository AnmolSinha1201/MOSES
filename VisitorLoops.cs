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
		public override object VisitLoop([NotNull] MosesParser.LoopContext context)
		{
			var container = Helper.toVarTypeImmediate(Visit(context.exp()));
			if (container.vType != Interop.variableType.INT)
			{ return null; } //error
			for (int i = 0; i < (Int64)container.value; i++)
			{
				STable.setVariable(null, "M_Index", i);
				foreach (var lBlock in context.segmentBlock().loopBlock())
				{
					if (string.Equals(lBlock.GetText(), "break", StringComparison.CurrentCultureIgnoreCase))
						break;
					else if (string.Equals(lBlock.GetText(), "continue", StringComparison.CurrentCultureIgnoreCase))
						continue;
					Visit(lBlock.innerfunctionBlock());
				}
			}
			return null;
		}

		public override object VisitWhileLoop([NotNull] MosesParser.WhileLoopContext context)
		{
			Interop.IContainer container;
            bool bContinue;
			int i = 0;
			while (true)
			{
				container = Helper.toVarTypeImmediate(Visit(context.exp()));
				bContinue = Helper.isTrue(container.value);
				if (!bContinue)
					break;
				STable.setVariable(null, "M_Index", i++);
				foreach (var lBlock in context.segmentBlock().loopBlock())
				{
					if (string.Equals(lBlock.GetText(), "break", StringComparison.CurrentCultureIgnoreCase))
						break;
					else if (string.Equals(lBlock.GetText(), "continue", StringComparison.CurrentCultureIgnoreCase))
						continue;
					Visit(lBlock.innerfunctionBlock());
				}
			}
			return null;
		}

		public override object VisitLoopParse([NotNull] MosesParser.LoopParseContext context)
		{
			string val = Visit(context.exp(0))?.ToString();
			if (val == null)
				return null;
			char[] delim = Visit(context.exp(1))?.ToString()?.ToCharArray();
			string[] splitStr = delim.Count() == 0 ? val.ToCharArray().Select(c => c.ToString()).ToArray() : val.Split(delim);
			for (int i = 0; i < splitStr.Count(); i++)
			{
				STable.setVariable(null, "M_LoopField", splitStr[i]);
				STable.setVariable(null, "M_Index", i);
				foreach (var lBlock in context.segmentBlock().loopBlock())
				{
					if (string.Equals(lBlock.GetText(), "break", StringComparison.CurrentCultureIgnoreCase))
						break;
					else if (string.Equals(lBlock.GetText(), "continue", StringComparison.CurrentCultureIgnoreCase))
						continue;
					Visit(lBlock.innerfunctionBlock());
				}
			}
			return null;
		}
	}
}
