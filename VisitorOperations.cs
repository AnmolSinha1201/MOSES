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
		public override object VisitExpAddSub([NotNull] MosesParser.ExpAddSubContext context)
		{
			var num1 = Convert.ToInt64(Visit(context.exp(0)));
			var num2 = Convert.ToInt64(Visit(context.exp(1)));
			if (context.operatorAddSub().GetText() == "+")
				return num1 + num2;
			else //OP = -
				return num1 - num2;
		}
	}
}
