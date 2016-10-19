using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	partial class MosesVisitor : MosesBaseVisitor<object>
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

		internal void throwScriptError(string error)
		{
			if (scriptErrorAsStdErr)
				Console.Error.WriteLine("SCRIPTERROR : " + error);
			throw new MOSESException("SCRIPTERROR : " + error);
		}

		internal bool supressWarning = false;
		internal bool scriptErrorAsStdErr = false;

		class MOSESException : System.Exception
		{
			public MOSESException() : base() { }
			public MOSESException(string message) : base(message) { }
			public MOSESException(string message, System.Exception inner) : base(message, inner) { }

			protected MOSESException(System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context)
			{ }
		}
	}
}
