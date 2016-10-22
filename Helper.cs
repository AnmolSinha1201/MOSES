using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	class Helper
	{
		internal static string decorateString(string value)
		{
			StringBuilder sb = new StringBuilder(value);
			if (value[0] == '"' && value.Last() == '"')
			{
				sb.Remove(0, 1);
				sb.Length--;
			}

			sb.Replace("\\\"", "\"");
			sb.Replace("\\n", "\n");
			sb.Replace("\\t", "\t");
			sb.Replace("\\r", "\r");
			sb.Replace("\\v", "\v");
			sb.Replace("\\\\", "\\");
			return sb.ToString();
		}

		internal static bool isTrue(Interop.IContainer container)
		{
			if (container == null || container.vType == Interop.variableType.NONE)
				return false;
			if (container.value is bool)
				return (bool)container.value;
			if (container.vType == Interop.variableType.INT && (Int64)container.value != 0)
				return true;
			else if (container.vType == Interop.variableType.DOUBLE && (double)container.value != 0)
				return true;
			else if (container.vType == Interop.variableType.STRING && !string.IsNullOrEmpty((string)container.value))
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
			if (val is Int64 || val is Int32)
				type = Interop.variableType.INT;
			else if (val is double || val is float)
				type = Interop.variableType.DOUBLE;
			else if (val is string)
				type = Interop.variableType.STRING;
			else if (val is bool)
				type = Interop.variableType.BOOL;
			else
				type = Interop.variableType.OBJECT;

			if (type == Interop.variableType.INT || type == Interop.variableType.DOUBLE || type == Interop.variableType.BOOL || type == Interop.variableType.OBJECT)
				return new Interop.IContainer { vType = type, value = val };

			long outLong;
			double outDouble;
            if (Int64.TryParse((string)val, out outLong))
				return new Interop.IContainer { vType = Interop.variableType.INT, value = outLong };
			else if (double.TryParse((string)val, out outDouble))
				return new Interop.IContainer { vType = Interop.variableType.DOUBLE, value = outDouble };

			return new Interop.IContainer { vType = Interop.variableType.STRING, value = val };
		}

		internal static Interop.IContainer toIntDouble(object val)
		{
			var container = Helper.toVarTypeImmediate(val);
			if (container == null || container.vType == Interop.variableType.NONE)
				return null;
			if (container.vType != Interop.variableType.INT && container.vType != Interop.variableType.DOUBLE)
				return null; //error
			return container;
		}
	}
}
