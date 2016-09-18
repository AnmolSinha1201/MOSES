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
		internal Interop interop = null;
		public override object VisitFunctionDecl([NotNull] MosesParser.FunctionDeclContext context)
		{ return false; }

		public override object VisitComplexFunctionCall([NotNull] MosesParser.ComplexFunctionCallContext context)
		{
			cDef = context.@this() == null ? null : STable.getParent();
			if (context.variableOrFunction() != null) //this.?variableOrFunction.functionCall
				Visit(context.variableOrFunction());
			return Visit(context.functionCall());
		}

		
		public override object VisitFunctionCall([NotNull] MosesParser.FunctionCallContext context)
		{
			if (cDef == null)
				cDef = STable.getGlobalCDef();
			//var funcSig = STable.getFunctionSignature(cDef, context.NAME().ToString());
			var funcSig = STable.getFunction(cDef, context.NAME().ToString(), context.exp().Count());
			if (funcSig == null)
			{ }
			
			var paramList = prepareParams(context, funcSig);
			if (funcSig._delegate != null)
			{
				object val = execHDF(paramList, funcSig);
				cDef = val as SymbolTable.classDef; //for func().something chaining
				return val;
			}
			else
			{
				object val = execUDF(paramList, funcSig);
				cDef = val as SymbolTable.classDef; //for func().something chaining
				return val;
			}
		}

		internal object execUDF(List<SymbolTable.variable> paramList, SymbolTable.functionDef fDef)
		{
			var namedList = addNameToParams(fDef.functionParamterList, paramList);
			STable.newFunctionContext(cDef, namedList);
			object retVal = null;
			foreach (MosesParser.InnerfunctionBlockContext ifb in (MosesParser.InnerfunctionBlockContext[])fDef.functionAST)
			{
				if (ifb.returnBlock() != null)
				{
					retVal =  Visit(ifb.returnBlock().exp());
					break;
				}
				Visit(ifb);
			}
			STable.restoreFunctionContext();
			return retVal;
		}

		internal object execHDF(List<SymbolTable.variable> paramList, SymbolTable.functionDef fDef)
		{
			var cDefTemp = cDef;
			var args = paramList.ToArray();
            interop.wrapParameters(args);
			return fDef._delegate(interop.wrap(cDefTemp), args);
		}

			List<SymbolTable.variable> prepareParams(MosesParser.FunctionCallContext fcContext, SymbolTable.functionDef fDef)
		{
			var paramList = new List<SymbolTable.variable>();
			int final = fDef.isVariadic ? fDef.functionParamterList.Count - 1 : fDef.functionParamterList.Count;

			for (int i = 0; i < final; i++)
			{
				object val = fcContext.exp(i) != null ? Visit(fcContext.exp(i)) : fDef.functionParamterList[i].defaultValue;
				SymbolTable.variable var = null;
				if (fDef.functionParamterList[i].byRef)
					var = STable.getVariable(cDef, vName);
				
				if (var == null)
				{
					var = new SymbolTable.variable();
					var.value = val; var.vType = STable.getVarTypeLazy(val);
				}
				paramList.Add(var);
			}
			if (!fDef.isVariadic)
				return paramList;

			var variadic = new SymbolTable.classDef();
			for (int i = final; i < fcContext.exp().Count(); i++)
				STable.setVariable(variadic, (i - final).ToString(), Visit(fcContext.exp(i)));
			paramList.Add(new SymbolTable.variable() { value = variadic, vType = Interop.variableType.OBJECT });
			return paramList;
		}

		Dictionary<string, SymbolTable.variable> addNameToParams(List<SymbolTable.functionDef.functionParameter> fDef, List<SymbolTable.variable> args)
		{
			return fDef.Select((x, i) => new { key = x.name, val = args[i] }).ToDictionary(e => e.key, e => e.val);
		}
		
	}
}
