using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	partial class SymbolTable
	{
		internal class variable : Interop.IContainer
		{ }

		internal void setVariable(classDef cDef, string varName, object value)
		{
			if (cDef == null)
				cDef = currentClassDef;
			if (cDef.varTable.Keys.Contains(varName)) //avoid creating new variable for ref to work
			{
				var var = cDef.varTable[varName];
				var.value = value;
				var.vType = getVarTypeLazy(value);
				cDef.varTable[varName] = var;
			}
			else
				cDef.varTable.Add(varName, new variable() { value = value, vType = getVarTypeLazy(value) });
		}

		internal variable getVariable(classDef cDef, string varName)
		{
            if (cDef == null)
				cDef = currentClassDef;
			if (varName != null && cDef.varTable.ContainsKey(varName))
				return cDef.varTable[varName];
			return null;
		}

		internal Interop.variableType getVarTypeLazy(object value)
		{
			if (value == null)
				return Interop.variableType.NONE;
			Type t = value.GetType();
			if (t == typeof(string))
				return Interop.variableType.STRING;
			else if (t == typeof(Int64) || t == typeof(Int32))
				return Interop.variableType.INT;
			else if (t == typeof(double) || t == typeof(float))
				return Interop.variableType.DOUBLE;
			else
				return Interop.variableType.OBJECT;
		}
	}
}
