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

			var funcSig = cDef.__call ? STable.fDefVariadicTemplate : STable.getFunction(cDef, context.NAME().ToString(), context.exp().Count());
			if (funcSig == null)
				EHandler.throwScriptError(context.Start.Line, context.Start.Column,
					context.Parent.GetText(), ErrorHandler.FunctionNotExist + context.NAME());

			List<MosesParser.ExpContext> expList = new List<MosesParser.ExpContext>();
			for (int i = 0; i < context.exp().Count(); i++)
				expList.Add(context.exp(i));

			var cDefTemp = cDef;
			var paramList = ToContainerArgs(expList, funcSig?.functionParamterList);
			var val = interop.invokeFunction(cDefTemp, context.NAME().ToString(), paramList)?.value;
			cDef = val as SymbolTable.classDef; //for func().something chaining
			return val;
		}

		public override object VisitReturnBlock([NotNull] MosesParser.ReturnBlockContext context)
		{
			return new controlFlow() { type = controlFlow.flowType._return, value = (context.exp() != null ? Visit(context.exp()) : null) };
		}

		internal object execUDF(Interop.IContainer[] paramList, SymbolTable.functionDef fDef)
		{
			var namedList = addNameToParams(fDef.functionParamterList, paramList);
			STable.newFunctionContext(cDef, namedList);
			object retVal = null;
			foreach (MosesParser.InnerfunctionBlockContext ifb in (MosesParser.InnerfunctionBlockContext[])fDef.functionAST)
			{
				retVal = Visit(ifb);
				if (retVal?.GetType() == typeof(controlFlow) && ((controlFlow)retVal).type == controlFlow.flowType._return)
					return ((controlFlow)retVal).value;
			}
			STable.restoreFunctionContext();
			return retVal;
		}

		internal object execHDF(Interop.IContainer[] paramList, SymbolTable.functionDef fDef)
		{
			return fDef._delegate(cDef, paramList);
		}

		Interop.IContainer[] ToContainerArgs(List<MosesParser.ExpContext> expContext, List<SymbolTable.functionDef.functionParameter> fParam)
		{
			var containerList = new List<Interop.IContainer>();
			if (fParam == null)
				return null;
			for (int i = 0; i < expContext.Count; i++)
			{
				vName = null;
				object val = Visit(expContext[i]);
				SymbolTable.variable var = STable.getVariable(cDef, vName); //check or assign
				
				if (var == null && vName != null) //vName != null : variable
				{
					STable.setVariable(cDef, vName, null); //allocate variable to ensure value is referenced
					var = STable.getVariable(cDef, vName);
				}
				
				if (var == null || !fParam[i].byRef)
					var = new SymbolTable.variable() { value = val, vType = STable.getVarTypeLazy(val) }; //clone if not byref
				containerList.Add(var);
			}
			return containerList.ToArray();
		}


		Dictionary<string, SymbolTable.variable> addNameToParams(List<SymbolTable.functionDef.functionParameter> fDef, Interop.IContainer[] args)
		{
			return fDef.Select((x, i) => new { key = x.name, val = args[i] as SymbolTable.variable }).ToDictionary(e => e.key, e => e.val, StringComparer.InvariantCultureIgnoreCase);
		}
		
	}
}
