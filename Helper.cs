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

		internal static bool isTrue(Interop.IContainer container)
		{
			if (container.vType == Interop.variableType.NONE)
				return false;
			if (container.vType == Interop.variableType.INT && (Int64)container.value != 0)
				return true;
			else if (container.vType == Interop.variableType.DOUBLE && (double)container.value != 0)
				return true;
			else if (container.vType == Interop.variableType.STRING && !string.IsNullOrEmpty(container.value as string))
				return true;
			else if (container.vType == Interop.variableType.OBJECT)
				return true;
			return false;
		}

		internal static bool isTrue(object value)
		{
			var container = toVarTypeImmediate(value);
			return isTrue(container);
		}

		internal static Interop.IContainer toVarTypeImmediate(object val)
		{
			if (val == null)
				return null;
			Interop.variableType type;
			if ((val as Int64?) != null || (val as Int32?) != null)
				type = Interop.variableType.INT;
			else if ((val as double?) != null || (val as float?) != null)
				type = Interop.variableType.DOUBLE;
			else if ((val as string) != null)
				type = Interop.variableType.STRING;
			else
				type = Interop.variableType.OBJECT;

			if (type == Interop.variableType.INT || type == Interop.variableType.DOUBLE || type == Interop.variableType.OBJECT)
				return new Interop.IContainer { vType = type, value = val };

			long outLong;
			double outDouble;
            if (Int64.TryParse(val as string, out outLong))
				return new Interop.IContainer { vType = Interop.variableType.INT, value = outLong };
			else if (double.TryParse(val as string, out outDouble))
				return new Interop.IContainer { vType = Interop.variableType.DOUBLE, value = outDouble };

			return null;
		}
	}
}
