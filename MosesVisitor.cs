using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace MOSES
{
	class MosesVisitor : MosesBaseVisitor<object>
	{
		static SymbolTable.classDef cDef = null;
		static string vName = null;
		SymbolTable STable = null;

		public override object VisitVariableFetch([NotNull] MosesParser.VariableFetchContext context)
		{
			Visit(context.complexVariable());
			return STable.getVariable(cDef, vName);
		}

		public override object VisitVarAssign([NotNull] MosesParser.VarAssignContext context)
		{
			object val = Visit(context.exp());
			Visit(context.complexVariable());
			STable.addVariable(cDef, vName, val);
			Console.WriteLine(vName + " = " + val);
			return val;
		}

		public override object VisitComplexVariable([NotNull] MosesParser.ComplexVariableContext context)
		{
			cDef = context.@this() == null ? null : STable.getParent(); //null = currentCDef
			if (context.variableOrFunction() != null) //this.?varOrFunction.var
				Visit(context.variableOrFunction());
			vName = context.var().NAME().ToString();
			return false;
		}

		public override object VisitVariableOrFunction([NotNull] MosesParser.VariableOrFunctionContext context)
		{
			if (context.var() != null) //var
			{ }
			else if (context.functionCall() != null) // functionCall
			{ }
			else if (context.exp() != null) // variableOrFunction[exp]
			{ }
			else //variableOrFunction.variableOrFunction
			{ }
			return base.VisitVariableOrFunction(context);
		}

		public override object VisitVar([NotNull] MosesParser.VarContext context)
		{
			if (context.NAME().ToString() == "_")
				cDef = STable.getGlobalCDef();
			else
				cDef = STable.getCDefFromCDef(cDef, context.NAME().ToString());

			if (cDef == null) //no variable or class found
			{
				var position = context.Start;
				//Helper.throwError(Helper.errorCode.nonExistentClass, context.Parent.GetText(), position.Line, position.Column);
			}
			return false;
		}

		public override object VisitString([NotNull] MosesParser.StringContext context)
		{
			return Helper.decorateString(context.STRING().ToString());
		}

		public override object VisitNumber([NotNull] MosesParser.NumberContext context)
		{
			return context.NUMBER().ToString();
		}
	}
}
