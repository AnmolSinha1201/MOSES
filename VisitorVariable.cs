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
		internal SymbolTable.classDef cDef = null;
		string vName = null;
		internal SymbolTable STable = null;

		public override object VisitVariableFetch([NotNull] MosesParser.VariableFetchContext context)
		{
			Visit(context.complexVariable());
			return STable.getVariable(cDef, vName)?.value;
		}

		public override object VisitVarAssign([NotNull] MosesParser.VarAssignContext context)
		{
			object val = Visit(context.exp());
			Visit(context.complexVariable());
			STable.setVariable(cDef, vName, val);
			Console.WriteLine(vName + " = " + val);
			return val;
		}

		public override object VisitComplexVariable([NotNull] MosesParser.ComplexVariableContext context)
		{
			cDef = context.@this() == null ? null : STable.getParent(); //null = currentCDef
			//Console.WriteLine(context.var().NAME() + "" + cDef == null);
			if (context.variableOrFunction() != null) //this.?varOrFunction.var
				Visit(context.variableOrFunction());
			if (context.var() != null)
				vName = context.var().NAME().ToString();
			else //this.?varOrFunction[key]
				vName = Visit(context.exp()) as string;
			return false;
		}

		public override object VisitVariableOrFunction([NotNull] MosesParser.VariableOrFunctionContext context)
		{
			// var / functionCall / variableOrFunction.variableOrFunction managed by filter
			if (context.exp() != null) // variableOrFunction[exp]. 1st part already visited
			{
				Visit(context.variableOrFunction(0));
				if (cDef == null) //chaining error
				{ }
				var cDefTemp = cDef;
				object val = Visit(context.exp());
				cDef = STable.getVariable(cDefTemp, val as string)?.value as SymbolTable.classDef;
			}
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
				//var position = context.Start;
				//Helper.throwError(Helper.errorCode.nonExistentClass, context.Parent.GetText(), position.Line, position.Column);
			}
			return null;
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
