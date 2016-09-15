﻿using System;
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
		public override object VisitClassDecl([NotNull] MosesParser.ClassDeclContext context)
		{
			bSkip = false;
			STable.newClassDef(context.NAME().ToString());
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

			for (int i = 0; i < contextPList.functionParameterNoDefault().Count(); i++)
			{
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionParameterNoDefault(i).NAME().ToString(),
					byRef = contextPList.functionParameterNoDefault(i).@ref() != null
				};
				parameterList.Add(fParam);
			}

			for (int i = 0; i < contextPList.functionParameterDefault().Count(); i++)
			{
				var fParam = new SymbolTable.functionDef.functionParameter()
				{
					name = contextPList.functionParameterDefault(i).NAME().ToString(),
					defaultValue = Visit(contextPList.functionParameterDefault(i).constExp()).ToString(),
					byRef = contextPList.functionParameterDefault(i).@ref() != null
				};
				parameterList.Add(fParam);
			}

			if (contextPList.functionPrameterVariadic() != null)
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
				minParamCount = contextPList.functionParameterNoDefault().Count()
			};

			STable.addFunction(null, context.functionDef().NAME().ToString(), function);
			
			return false;
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
				STable.addVariable(null, context.NAME().ToString(), Visit(context.constExp()));
			return base.VisitLocalConstVarAssign(context);
		}
	}
}
