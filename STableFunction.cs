﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	partial class SymbolTable
	{
		internal class functionDef
		{
			public object functionAST;
			public List<functionParameter> functionParamterList = new List<functionParameter>();
			public Interop.functionDelegate _delegate = null;
			public bool isVariadic = false;
			public int minParamCount;

			public class functionParameter
			{
				public string name;
				public object defaultValue = null;
				public bool byRef = false;
			}
		}


		//no sorting as sorting overhead would be greater than traversing overhead for a typical script.
		internal void addFunction(classDef cDef, string funcName, functionDef funcDef)
		{
			if (cDef == null)
				cDef = currentClassDef;
			if (!cDef.funcTable.ContainsKey(funcName))
				cDef.funcTable.Add(funcName, new List<functionDef>());

			foreach (var fDef in cDef.funcTable[funcName])
				if (fDef.minParamCount == funcDef.minParamCount &&
					fDef.functionParamterList.Count == funcDef.functionParamterList.Count &&
					fDef.isVariadic == funcDef.isVariadic)
					Helper.errorReport("function signature already exists");
			cDef.funcTable[funcName].Add(funcDef);
		}

		internal functionDef getFunction(classDef cDef, string funcName, int paramCount)
		{
			if (cDef == null)
				cDef = currentClassDef;
			if (!cDef.funcTable.ContainsKey(funcName))
				return null;
			functionDef bestFit = null;
			foreach (var fDef in cDef.funcTable[funcName])
			{
				if (fDef.minParamCount == paramCount)
					return fDef;
				//no need to check variadic case here. Automatically selects biggest signature, overriding smaller variadics
				else if (fDef.minParamCount <= paramCount && fDef.functionParamterList.Count >= paramCount)
					bestFit = fDef;
				//works only if no other fit is already found
				else if (fDef.minParamCount <= paramCount && fDef.isVariadic && bestFit == null)
					bestFit = fDef;
			}
			return bestFit;
		}


		Stack<classDef> functionParentStack = new Stack<classDef>(); //to track this.varOrFunc
		classDef currentParent = null; //to track this.varOrFunc

		internal classDef getParent()
		{
			return currentParent;
		}
	}
}
