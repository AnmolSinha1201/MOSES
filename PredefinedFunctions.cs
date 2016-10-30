using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MOSES.Interop;
using static MOSES.SymbolTable;

namespace MOSES
{
	class PredefinedFunctions
	{
		static PredefinedFunctions()
		{
			funcList.Add("count", funcBuilder(Count, 0));
			funcList.Add("clone", funcBuilder(Clone, 0));
			funcList.Add("hasKey", funcBuilder(HasKey, 1));
			funcList.Add("remove", funcBuilder(Remove, 1));
		}
		internal static Dictionary<string, List<functionDef>> funcList = new Dictionary<string, List<functionDef>>();
		
		static List<functionDef> funcBuilder(functionDelegate d, int paramCount)
		{
			var func = new functionDef() { _delegate = new functionDelegate(d), minParamCount = paramCount };
			for (int i = 0; i < paramCount; i++)
				func.functionParamterList.Add(new functionDef.functionParameter());
			return new List<functionDef>() { func };
		}

		static object Remove(object context, IContainer[] args)
		{
			var cDef = (classDef)context;
			if (cDef.varTable.ContainsKey(args[0].value.ToString()))
				cDef.varTable.Remove(args[0].value.ToString());
			return null;
		}

		static object HasKey(object context, IContainer[] args)
		{
			var cDef = (classDef)context;
			return cDef.varTable.ContainsKey((string)args[0].value);
		}

		static object Count(object context, IContainer[] args)
		{
			var cDef = (classDef)context;
			return cDef.varTable.Count;
		}

		static object Clone(object context, IContainer[] args)
		{
			var cDef = (classDef)context;
			classDef retVal = new classDef();

			//no need to clone classes and functions value by value because they can not change.
			retVal.classTable = new Dictionary<string, classDef>(cDef.classTable, StringComparer.InvariantCultureIgnoreCase);
			retVal.funcTable = new Dictionary<string, List<functionDef>>(cDef.funcTable, StringComparer.InvariantCultureIgnoreCase);
			retVal.varTable = cDef.varTable.ToDictionary(entry => entry.Key, entry => new variable() { value = entry.Value.value, vType = entry.Value.vType }, StringComparer.InvariantCultureIgnoreCase);

			retVal.__new = cDef.__new;
			retVal.__delete = cDef.__delete;
			retVal.__call = cDef.__call;
			retVal.__set = cDef.__set;
			retVal.__get = cDef.__get;
			return retVal;
		}
	}
}
