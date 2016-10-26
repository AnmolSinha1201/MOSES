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
		public override object VisitExpOpPow([NotNull] MosesParser.ExpOpPowContext context)
		{
			return OpMath(Visit(context.exp(0)), Visit(context.exp(1)), context.operatorPower().GetText());
		}

		public override object VisitExpMulDivMod([NotNull] MosesParser.ExpMulDivModContext context)
		{
			return OpMath(Visit(context.exp(0)), Visit(context.exp(1)), context.operatorMulDivMod().GetText());
		}

		public override object VisitExpAddSub([NotNull] MosesParser.ExpAddSubContext context)
		{
			return OpMath(Visit(context.exp(0)), Visit(context.exp(1)), context.operatorAddSub().GetText());
		}

		public override object VisitMathVarAssign([NotNull] MosesParser.MathVarAssignContext context)
		{
			Visit(context.complexVariable());
			string op = context.op.Text.Substring(0, context.op.Text.Length - 1);
			var val = OpMath(STable.getVariable(cDef, vName)?.value, Visit(context.exp()), op);
            STable.setVariable(cDef, vName, val);
			return val;
		}

		object OpMath(object o1, object o2, string op)
		{
			var val1 = Helper.toIntDouble(o1)?.value;
			var val2 = Helper.toIntDouble(o2)?.value;
			if (val1 == null || val2 == null)
				return null;

			dynamic left = val1, right = val2;

			switch(op)
			{
				case "+": return left + right;
				case "-": return left - right;
				case "*": return left * right;
				case "/": return left / right;
				case "%": return left % right;
				case "**": return Math.Pow(left, right);
			}
			return null;
		}

		public override object VisitExpConcat([NotNull] MosesParser.ExpConcatContext context)
		{
			var v1 = Visit(context.exp(0))?.ToString();
			var v2 = Visit(context.exp(1))?.ToString();

			return v1 + v2;
		}

		public override object VisitConcatVarAssign([NotNull] MosesParser.ConcatVarAssignContext context)
		{
			Visit(context.complexVariable());
			var val1 = STable.getVariable(cDef, vName)?.value?.ToString();
			var val2 = Visit(context.exp())?.ToString();

			STable.setVariable(cDef, vName, val1 + val2);
			return val1 + val2;
		}

		public override object VisitExpCompare([NotNull] MosesParser.ExpCompareContext context)
		{
			var container1 = Helper.toVarTypeImmediate(Visit(context.exp(0)));
			var container2 = Helper.toVarTypeImmediate(Visit(context.exp(1)));

			string op = context.operatorComparison().GetText();
			if (op == "<" &&
				(container1.vType == Interop.variableType.INT | container1.vType == Interop.variableType.DOUBLE) &&
				(container2.vType == Interop.variableType.INT | container2.vType == Interop.variableType.DOUBLE))
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) < Convert.ToDouble(container2.value);
				return (Int64)container1.value < (Int64)container2.value;
			}
			else if (op == ">" &&
				(container1.vType == Interop.variableType.INT | container1.vType == Interop.variableType.DOUBLE) &&
				(container2.vType == Interop.variableType.INT | container2.vType == Interop.variableType.DOUBLE))
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) > Convert.ToDouble(container2.value);
				return (Int64)container1.value > (Int64)container2.value;
			}
			else if (op == "<=" &&
				(container1.vType == Interop.variableType.INT | container1.vType == Interop.variableType.DOUBLE) &&
				(container2.vType == Interop.variableType.INT | container2.vType == Interop.variableType.DOUBLE))
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) <= Convert.ToDouble(container2.value);
				return (Int64)container1.value <= (Int64)container2.value;
			}
			else if (op == ">=" &&
				(container1.vType == Interop.variableType.INT | container1.vType == Interop.variableType.DOUBLE) &&
				(container2.vType == Interop.variableType.INT | container2.vType == Interop.variableType.DOUBLE))
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) >= Convert.ToDouble(container2.value);
				return (Int64)container1.value >= (Int64)container2.value;
			}
			else if (op == "!=")
				return !container1.value.Equals(container2.value);
			else //if (op == "==")
				return container1.value.Equals(container2.value);

		}

		public override object VisitExpBitwise([NotNull] MosesParser.ExpBitwiseContext context)
		{
			return OpBitwise(Visit(context.exp(0)), Visit(context.exp(1)), context.operatorBitwise().GetText());
		}

		public override object VisitBitwiseVarAssign([NotNull] MosesParser.BitwiseVarAssignContext context)
		{
			Visit(context.complexVariable());
			string op = context.op.Text.Substring(0, context.op.Text.Length - 1);
			var val = OpBitwise(STable.getVariable(cDef, vName)?.value, Visit(context.exp()), op);
			STable.setVariable(cDef, vName, val);
			return val;
		}

		object OpBitwise(object o1, object o2, string op)
		{
			var val1 = Helper.toVarTypeImmediate(o1);
			var val2 = Helper.toVarTypeImmediate(o1);
			if (val1 == null || val2 == null)
				return null;
			if (val1.vType != Interop.variableType.INT || val2.vType != Interop.variableType.INT)
				return null;

			switch (op)
			{
				case "&": return (Int64)val1.value & (Int64)val2.value;
				case "|": return (Int64)val1.value | (Int64)val2.value;
				case "^": return (Int64)val1.value ^ (Int64)val2.value;
				case "<<": return (Int64)val1.value << Convert.ToInt32(val2.value);
				case ">>": return (Int64)val1.value >> Convert.ToInt32(val2.value);
			}
			return null;
		}

		public override object VisitExpAND([NotNull] MosesParser.ExpANDContext context)
		{
			var container1 = Helper.toVarTypeImmediate(Visit(context.exp(0)));
			if (!Helper.isTrue(container1))
				return false;
			var container2 = Helper.toVarTypeImmediate(Visit(context.exp(1)));
			if (!Helper.isTrue(container2))
				return false;
			return true;
		}

		public override object VisitExpOR([NotNull] MosesParser.ExpORContext context)
		{
			var container1 = Helper.toVarTypeImmediate(Visit(context.exp(0)));
			if (Helper.isTrue(container1))
				return true;
			var container2 = Helper.toVarTypeImmediate(Visit(context.exp(1)));
			if (Helper.isTrue(container2))
				return true;
			return false;
		}
	}
}
