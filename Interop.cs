using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSES
{
	class Interop
	{
		public class IContainer
		{
			public object value;
			public variableType vType;
		}

		public enum variableType
		{
			INT, DOUBLE, STRING, OBJECT, NONE
		}


		public delegate object functionDelegate(SymbolTable.classDef instance, object[] paramArray);
	}
}
