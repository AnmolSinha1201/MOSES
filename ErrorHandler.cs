using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace MOSES
{
	partial class ErrorHandler : BaseErrorListener
	{
		internal bool debugParser = false;
		internal bool parseError = false;

		public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			parseError = true;
			Console.Error.WriteLine($"PARSEERROR : @({line},{charPositionInLine}) : unexpected symbol : {offendingSymbol.Text}");

			if (debugParser)
				Console.Error.WriteLine(msg);
		}

		#region output Methods
		internal void throwParseError()
		{
			var eData = new MOSESException.ErrorData { errorType = 1 };
			throw new MOSESException($"PARSEERROR : @(?,?) : unexpected symbol : ?", eData);
		}

		internal void throwWarning(string warning)
		{
			if (!supressWarning)
				Console.Error.WriteLine("WARNING : " + warning);
		}

		internal void throwHostError(string error)
		{
			var eData = new MOSESException.ErrorData { errorType = 2 };
			throw new MOSESException("HOSTERROR : " + error, eData);
		}

		internal void throwScriptError(int line, int column, string parent, string error)
		{
			if (scriptErrorAsStdErr)
				Console.Error.WriteLine($"SCRIPTERROR :@({line},{column}) : {parent} : {error}");
			var eData = new MOSESException.ErrorData { line = line, column = column, errorType = 3 };
			throw new MOSESException($"SCRIPTERROR : @({line},{column}) : {parent} : {error}", eData);
		}

		internal bool supressWarning = false;
		internal bool scriptErrorAsStdErr = true;
		
		internal const string ClassExists = "Class definition already exists in current context : ";
		internal const string ClassNotExist = "Class definition does NOT exist in current context : ";
		internal const string FunctionExists = "Function definition already exists in current context : ";
		internal const string FunctionNotExist = "Function definition does NOT exist in current context : ";

		#endregion
	}

	class MOSESException : ApplicationException
	{
		public class ErrorData
		{
			public int line = 0, column = 0, errorType = 0;
		}

		public MOSESException(string message, ErrorData eData) : base(message)
		{
			Data.Add("errorData", eData);
		}

		public MOSESException() : base() { }
		public MOSESException(string message) : base(message) { }
		public MOSESException(string message, System.Exception inner) : base(message, inner) { }

		protected MOSESException(System.Runtime.Serialization.SerializationInfo info,
	System.Runtime.Serialization.StreamingContext context)
		{ }
	}
}
