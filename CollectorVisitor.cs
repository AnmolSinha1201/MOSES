using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace MOSES
{
	class CollectorVisitor : MosesBaseVisitor<object>
	{
		bool bSkip = true; //to prevent collecting variables outside classes
		internal SymbolTable STable = null;
		internal ErrorHandler EHandler = null;

		public override object VisitClassDecl([NotNull] MosesParser.ClassDeclContext context)
		{
			bSkip = false;
			if (!STable.newClassDef(context.NAME().ToString()))
			{
				EHandler.throwScriptError($"({context.Start.Line},{context.Start.Column})", 
					$"class {context.NAME()}{{}}", 
					ErrorHandler.ClassExists + context.NAME());
			}
			foreach (MosesParser.ClassBlockContext cbc in context.classBlock())
				Visit(cbc);
			STable.restoreClassDef();
			bSkip = true;
			return false;
		}

		public override object VisitFunctionDecl([NotNull] MosesParser.FunctionDeclContext context)
		{
			var parameterList = new List<SymbolTable.functionDef.functionParameter>();
			var contextPList = context.functionDef().functionParameterList();
			bool isVariadic = false;

			int iters = contextPList == null ? 0 : contextPList.functionParameterNoDefault().Count();
            for (int i = 0; i < iters; i++)
			{
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionParameterNoDefault(i).NAME().ToString(),
					byRef = contextPList.functionParameterNoDefault(i).@ref() != null
				};
				parameterList.Add(fParam);
			}

			iters = contextPList == null ? 0 : contextPList.functionParameterDefault().Count();
			for (int i = 0; i < iters; i++)
			{
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionParameterDefault(i).NAME().ToString(),
					defaultValue = Visit(contextPList.functionParameterDefault(i).constExp()).ToString(),
					byRef = contextPList.functionParameterDefault(i).@ref() != null
				};
				parameterList.Add(fParam);
			}
			
			if (contextPList?.functionPrameterVariadic() != null)
			{
				isVariadic = true;
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionPrameterVariadic().NAME().ToString()
				};
				parameterList.Add(fParam);
			}
			
			var function = new SymbolTable.functionDef()
			{
				functionAST = context.functionBody().innerfunctionBlock(),
				functionParamterList = parameterList,
				isVariadic = isVariadic,
				minParamCount = contextPList == null ? 0 : contextPList.functionParameterNoDefault().Count()
			};

			//Console.WriteLine(STable.addFunction(null, context.functionDef().NAME().ToString(), function) == true);
			if (!STable.addFunction(null, context.functionDef().NAME().ToString(), function))
				EHandler.throwScriptError($"({context.Start.Line},{context.Start.Column})", 
					$"{context.functionDef().NAME()}({context.functionDef().functionParameterList().GetText()}){{}}", 
					ErrorHandler.FunctionExists + context.functionDef().NAME());
			return null;
		}

		public override object VisitString([NotNull] MosesParser.StringContext context)
		{
			if (bSkip)
				return base.VisitString(context);
			return Helper.decorateString(context.STRING().ToString());
		}

		public override object VisitNumber([NotNull] MosesParser.NumberContext context)
		{
			if (bSkip)
				return base.VisitNumber(context);
			return context.NUMBER().ToString();
		}

		public override object VisitLocalConstVarAssign([NotNull] MosesParser.LocalConstVarAssignContext context)
		{
			if (!bSkip)
				STable.setVariable(null, context.NAME().ToString(), Visit(context.constExp()));
			return base.VisitLocalConstVarAssign(context);
		}
	}
}
