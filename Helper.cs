using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	class Helper
	{
		internal static void errorReport(string s)
		{
			Console.WriteLine(s);
		}

		internal static string decorateString(string value)
		{
			StringBuilder sb = new StringBuilder(value);
			if (value[0] == '"' && value.Last() == '"')
			{
				sb.Remove(0, 1);
				sb.Length--;
			}
			return sb.ToString();
		}

		internal static bool isTrue(object value)
		{
			var type = getVarTypeImmediate(value);
			if (type == Interop.variableType.NONE)
				return false;
			if (type == Interop.variableType.INT && Convert.ToInt64(value) != 0)
				return true;
			else if (type == Interop.variableType.DOUBLE && Convert.ToDouble(value) != 0)
				return true;
			else if (type == Interop.variableType.STRING && !string.IsNullOrEmpty(value as string))
				return true;
			else if (type == Interop.variableType.OBJECT)
				return true;
			return false;
		}

		internal static Interop.variableType getVarTypeImmediate(object val)
		{
			if (val == null)
				return Interop.variableType.NONE;
			if ((val as Int64?) != null || (val as Int32?) != null)
				return Interop.variableType.INT;
			if ((val as double?) != null || (val as float?) != null)
				return Interop.variableType.DOUBLE;
			if ((val as string) != null)
				return Interop.variableType.STRING;
			return Interop.variableType.OBJECT;
		}
	}
}
