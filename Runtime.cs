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
		
		object testFunc(object instance, Interop.IContainer[] args)
		{
			object o = interop.newClassDef(null, "qwe");
			interop.setVariable(o, 0, args[0].value);
			return o;
		}

		public Interop interop = new Interop();
		public Runtime(string fileName)
		{
			var reader = File.OpenText(fileName);
			var input = new AntlrInputStream(reader);
			var lexer = new MosesLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);

			var parser = new MosesParser(tokens);
			IParseTree tree = parser.chunk();
			Console.WriteLine(tree.ToStringTree(parser));

			SymbolTable STable = new SymbolTable();
			var visitor = new MosesVisitor();
			interop.STable = STable;
			interop.MVisitor = visitor;
			interop.registerFunction(null, new Interop.functionDelegate(testFunc), "qwe(var){}");

			var collector = new CollectorVisitor();
			collector.STable = STable;
			collector.Visit(tree);
			
			visitor.STable = STable;
			visitor.interop = interop;
			Console.WriteLine(visitor.Visit(tree));
		}
	}
}
