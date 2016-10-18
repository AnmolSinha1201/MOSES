using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	partial class SymbolTable
	{
		Dictionary<string, object> includedFiles = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

		internal void addTree(string key, object tree)
		{
			if (includedFiles.ContainsKey(key))
				return;
			includedFiles.Add(key, tree);
		}

		internal object getTree(string key)
		{
			if (!includedFiles.ContainsKey(key))
				return null;
			return includedFiles[key];
		}
	}
}
