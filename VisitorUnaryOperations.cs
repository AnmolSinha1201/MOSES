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
		public override object VisitUnaryVar([NotNull] MosesParser.UnaryVarContext context)
		{
			Interop.IContainer container = null;
            if (context.unaryOp().complexVariable() != null)
			{
				Visit(context.unaryOp().complexVariable());
				container = Helper.toVarTypeImmediate(STable.getVariable(cDef, vName).value);
			}
			else
				container = Helper.toVarTypeImmediate(context.unaryOp().NUMBER().ToString());

			if (container == null || container.vType == Interop.variableType.NONE)
				return null;

			string op = context.unaryOp().operatorUnary().GetText();
			if (op == "-")
				return -(container.vType == Interop.variableType.INT ? (Int64)container.value : (double)container.value);
			else if (op == "!")
				return !Helper.isTrue(container);
			else if (op == "~" && container.vType == Interop.variableType.INT)
				return ~(Int64)container.value;
			else if (op == "+")
				return container.value;
			return null;
		}

		public override object VisitPreIncrDecr([NotNull] MosesParser.PreIncrDecrContext context)
		{
			Visit(context.complexVariable());
			var variable = STable.getVariable(cDef, vName);
            var container = Helper.toVarTypeImmediate(variable.value);
			if (container.vType != Interop.variableType.INT && container.vType != Interop.variableType.DOUBLE)
				return null; //error

			variable.vType = container.vType; variable.value = container.value;
			string op = context.incrDecr().GetText();
			if (op == "++")
			{
				variable.value = (variable.vType == Interop.variableType.INT ? (Int64)variable.value : (double)variable.value) + 1;
				return variable.value;
			}
			else
			{
				variable.value = (variable.vType == Interop.variableType.INT ? (Int64)variable.value : (double)variable.value) - 1;
				return variable.value;
			}
		}

		public override object VisitPostIncrDecr([NotNull] MosesParser.PostIncrDecrContext context)
		{
			Visit(context.complexVariable());
			var variable = STable.getVariable(cDef, vName);
			var container = Helper.toVarTypeImmediate(variable.value);
			if (container.vType != Interop.variableType.INT && container.vType != Interop.variableType.DOUBLE)
				return null; //error

			variable.vType = container.vType; variable.value = container.value;
			string op = context.incrDecr().GetText();
			if (op == "++")
			{
				var retVal = variable.value;
				variable.value = (variable.vType == Interop.variableType.INT ? (Int64)variable.value : (double)variable.value) + 1;
				return retVal;
			}
			else
			{
				var retVal = variable.value;
				variable.value = (variable.vType == Interop.variableType.INT ? (Int64)variable.value : (double)variable.value) - 1;
				return retVal;
			}
		}
	}
}
