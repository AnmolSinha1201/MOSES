﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	partial class SymbolTable
	{
		internal class classDef
		{
			public bool __new = false, __delete = false;
			public int referenceCount = 0;	
			public Dictionary<string, variable> varTable = new Dictionary<string, variable>();
			public Dictionary<string, List<functionDef>> funcTable = new Dictionary<string, List<functionDef>>();
			public Dictionary<string, classDef> classTable = new Dictionary<string, classDef>();
		}

		classDef currentClassDef = new classDef(); //global scope
		static Stack<classDef> classDepthStack = new Stack<classDef>();

		internal void newClassDef(string name)
		{
			var classDefTemp = new classDef();
			currentClassDef.classTable.Add(name, classDefTemp);
			classDepthStack.Push(currentClassDef);
			currentClassDef = classDefTemp;
		}

		internal void restoreClassDef()
		{
			currentClassDef = classDepthStack.Pop();
		}

		internal classDef addClass(classDef cDef, string name)
		{
			cDef = cDef ?? getGlobalCDef();
			if (cDef.classTable.ContainsKey(name))
			{ } //error.. Class already exists
			classDef cDefTemp = new classDef();
			cDef.classTable.Add(name, cDefTemp);
			return cDefTemp;
		}

		internal classDef createInstance(classDef cDef, string name)
		{
			cDef = cDef ?? getGlobalCDef();
			if (!cDef.classTable.ContainsKey(name))
			{ return null; } //error
			classDef retVal = new classDef();
			//don't create new copy for classTable (to prevent static calling)
			retVal.funcTable = new Dictionary<string, List<functionDef>>(cDef.classTable[name].funcTable);
			retVal.varTable = new Dictionary<string, variable>(cDef.classTable[name].varTable);
			retVal.__new = cDef.classTable[name].__new;
			retVal.__delete = cDef.classTable[name].__delete;
			return retVal;
		}

		internal classDef getGlobalCDef()
		{
			if (classDepthStack.Count == 0)
				return currentClassDef;
			return classDepthStack.ElementAt(0);
		}

		internal classDef getCurrentCDef()
		{
			return currentClassDef;
		}


		//priority = variable->class(treated as static)
		internal classDef getCDefFromCDef(classDef cDef, string name)
		{
			if (cDef == null)
				cDef = currentClassDef;
			//.object type works because for any other object type rule varOrFunction.var will be operated upon.
			if (cDef.varTable.ContainsKey(name) && cDef.varTable[name].vType == Interop.variableType.OBJECT)
				return cDef.varTable[name].value as classDef;
			if (!cDef.classTable.ContainsKey(name))
				return null;

			return cDef.classTable[name];
		}
	}
}
