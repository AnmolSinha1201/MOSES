using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace MOSES
{
	public class Runtime
	{
		public Runtime() : this("testFile.txt")
		{}

		public Interop interop = new Interop();
		MosesVisitor visitor = new MosesVisitor();
		IParseTree tree = null;
		public Runtime(string fileName) : this(fileName, new SymbolTable())
		{}

		internal Runtime(string fileName, SymbolTable STable)
		{
			var reader = File.OpenText(fileName);
			var input = new AntlrInputStream(reader);
			reader.Close();
			var lexer = new MosesLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);

			var parser = new MosesParser(tokens);
			tree = parser.chunk();
			//Console.WriteLine(tree.ToStringTree(parser));

			var collector = new CollectorVisitor();
			collector.STable = STable;
			collector.Visit(tree);

			interop.STable = STable;
			interop.MVisitor = visitor;
			visitor.STable = STable;
			visitor.interop = interop;
		}

		//seperate execution to allow host to add its own functions
		public void execute()
		{
			visitor.Visit(tree);
		}
	}
}
