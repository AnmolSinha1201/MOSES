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
			var container1 = Helper.toIntDouble(Visit(context.exp(0)));
			var container2 = Helper.toIntDouble(Visit(context.exp(0)));
			
			return Math.Pow(Convert.ToDouble(container1.value),Convert.ToDouble(container2.value));
		}

		public override object VisitExpMulDivMod([NotNull] MosesParser.ExpMulDivModContext context)
		{
			var container1 = Helper.toIntDouble(Visit(context.exp(0)));
			var container2 = Helper.toIntDouble(Visit(context.exp(1)));
			
			string op = context.operatorMulDivMod().GetText();
			if (op == "*")
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) * Convert.ToDouble(container2.value);
				return (Int64)container1.value * (Int64)container2.value;
			}
			else if (op == "/")
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) / Convert.ToDouble(container2.value);
				return (Int64)container1.value / (Int64)container2.value;
			}
			else if (op == "%")
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) % Convert.ToDouble(container2.value);
				return (Int64)container1.value % (Int64)container2.value;
			}
			else
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return (Int64)(Convert.ToDouble(container1.value) / Convert.ToDouble(container2.value));
				return (Int64)container1.value / (Int64)container2.value;
			}
		}

		public override object VisitExpAddSub([NotNull] MosesParser.ExpAddSubContext context)
		{
			var container1 = Helper.toIntDouble(Visit(context.exp(0)));
			var container2 = Helper.toIntDouble(Visit(context.exp(1)));

			string op = context.operatorAddSub().GetText();
			if (op == "+")
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) + Convert.ToDouble(container2.value);
				return (Int64)container1.value + (Int64)container2.value;
			}
			else
			{
				if (container1.vType == Interop.variableType.DOUBLE || container2.vType == Interop.variableType.DOUBLE)
					return Convert.ToDouble(container1.value) - Convert.ToDouble(container2.value);
				return (Int64)container1.value - (Int64)container2.value;
			}
		}

		public override object VisitExpConcat([NotNull] MosesParser.ExpConcatContext context)
		{
			var v1 = Visit(context.exp(0)).ToString();
			var v2 = Visit(context.exp(1)).ToString();

			return v1 + v2;
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
			var container1 = Helper.toVarTypeImmediate(Visit(context.exp(0)));
			var container2 = Helper.toVarTypeImmediate(Visit(context.exp(1)));

			string op = context.operatorBitwise().GetText();
			if (op == "&")
				return (Int64)container1.value & (Int64)container2.value;
			else if (op == "|")
				return (Int64)container1.value | (Int64)container2.value;
			else if (op == "^")
				return (Int64)container1.value ^ (Int64)container2.value;
			else if (op == "<<")
				return (Int64)container1.value << Convert.ToInt32(container2.value);
			else //if (op == ">>")
				return (Int64)container1.value >> Convert.ToInt32(container2.value);
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
