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
	}
}
