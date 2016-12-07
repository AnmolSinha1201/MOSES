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
		class controlFlow
		{
			public enum flowType { _continue, _break, _return }
			public object value = null;
			public flowType type;
		}

		public override object VisitBreak([NotNull] MosesParser.BreakContext context)
		{
			return new controlFlow() { type = controlFlow.flowType._break };
		}

		public override object VisitContinue([NotNull] MosesParser.ContinueContext context)
		{
			return new controlFlow() { type = controlFlow.flowType._continue };
		}

		public override object VisitLoop([NotNull] MosesParser.LoopContext context)
		{
			var container = Helper.toVarTypeImmediate(Visit(context.exp()));
			if (container.vType != Interop.variableType.INT)
			{ return null; } //error
			object retVal = null;
			for (int i = 0; i < (Int64)container.value; i++)
			{
				STable.setVariable(null, "M_Index", i);
				foreach (var lBlock in context.segmentBlock().loopBlock())
				{
					retVal = Visit(lBlock.innerfunctionBlock());
					if (retVal?.GetType() == typeof(controlFlow))
						break;
				}

				if (retVal?.GetType() == typeof(controlFlow))
				{
					if (((controlFlow)retVal).type == controlFlow.flowType._continue)
						continue;
					else if (((controlFlow)retVal).type == controlFlow.flowType._break)
						break;
					else
						return retVal;
				}
			}
			return null;
		}

		public override object VisitWhileLoop([NotNull] MosesParser.WhileLoopContext context)
		{
			object retVal = null;
			int i = 0;
			while (Helper.isTrue(Visit(context.exp())))
			{
				STable.setVariable(null, "M_Index", i++);
				foreach (var lBlock in context.segmentBlock().loopBlock())
				{
					retVal = Visit(lBlock.innerfunctionBlock());
					if (retVal?.GetType() == typeof(controlFlow))
						break;
				}

				if (retVal?.GetType() == typeof(controlFlow))
				{
					if (((controlFlow)retVal).type == controlFlow.flowType._continue)
						continue;
					else if (((controlFlow)retVal).type == controlFlow.flowType._break)
						break;
					else
						return retVal;
				}
			}
			return null;
		}

		public override object VisitForLoop([NotNull] MosesParser.ForLoopContext context)
		{
			if (context.varAssign() != null)
				Visit(context.varAssign());
			object retVal = null;
			while (true)
			{
				if (!Helper.isTrue(Visit(context.exp(0))))
					break;

				foreach (var lBlock in context.segmentBlock().loopBlock())
				{
					retVal = Visit(lBlock.innerfunctionBlock());
					if (retVal?.GetType() == typeof(controlFlow))
						break;
				}

				if (retVal?.GetType() == typeof(controlFlow))
				{
					if (((controlFlow)retVal).type == controlFlow.flowType._continue)
						continue;
					else if (((controlFlow)retVal).type == controlFlow.flowType._break)
						break;
					else
						return retVal;
				}
				if (context.exp(1) != null)
					Visit(context.exp(1));
			}
			return null;
		}

		public override object VisitLoopParse([NotNull] MosesParser.LoopParseContext context)
		{
			string val = Visit(context.exp(0))?.ToString();
			object retVal = null;

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
					retVal = Visit(lBlock.innerfunctionBlock());
					if (retVal?.GetType() == typeof(controlFlow))
						break;
				}

				if (retVal?.GetType() == typeof(controlFlow))
				{
					if (((controlFlow)retVal).type == controlFlow.flowType._continue)
						continue;
					else if (((controlFlow)retVal).type == controlFlow.flowType._break)
						break;
					else
						return retVal;
				}
			}
			return null;
		}
	}
}
