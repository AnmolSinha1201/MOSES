using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	partial class ErrorHandler
	{
		internal void throwWarning(string warning)
		{
			if (!supressWarning)
				Console.Error.WriteLine("WARNING : " + warning);
		}

		internal void throwHostError(string error)
		{
			throw new MOSESException("HOSTERROR : " + error);
		}

		internal void throwScriptError(string pos, string parent, string error)
		{
			if (scriptErrorAsStdErr)
				Console.Error.WriteLine($"SCRIPTERROR : @{pos} : {parent} : {error}");
			throw new MOSESException($"SCRIPTERROR : @{pos} : {parent} : {error}");
		}

		internal bool supressWarning = false;
		internal bool scriptErrorAsStdErr = true;

		class MOSESException : Exception
		{
			public MOSESException() : base() { }
			public MOSESException(string message) : base(message) {
				Source = "qwe";
				HResult = 123123;
			}
			public MOSESException(string message, System.Exception inner) : base(message, inner) { }

			protected MOSESException(System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context)
			{ }
		}

		internal const string ClassExists = "Class definition already exists in current context : ";
		internal const string ClassNotExist = "Class definition does NOT exist in current context : ";
		internal const string FunctionExists = "Function definition already exists in current context : ";
		internal const string FunctionNotExist = "Function definition does NOT exist in current context : ";
	}
}
