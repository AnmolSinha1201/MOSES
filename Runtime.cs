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
		public Interop interop = new Interop();
		MosesVisitor visitor = new MosesVisitor();
		IParseTree tree = null;
		ErrorHandler EHandler = null;
		public bool supressWarning
		{
			get { return EHandler.supressWarning; }
			set { EHandler.supressWarning = value; }
		}
		public bool scriptErrorAsStdErr
		{
			get { return EHandler.scriptErrorAsStdErr; }
			set { EHandler.scriptErrorAsStdErr = value; }
		}

		public bool debugParser
		{
			get { return EHandler.debugParser; }
			set { EHandler.debugParser = value; }
		}

		public Runtime()
		{ }

		public void parseCode(string code)
		{
			var EHandler = new ErrorHandler();
			this.EHandler = EHandler;
			EHandler.parseError = false;
			
			var input = new AntlrInputStream(code);
			sharedRuntime(input, new SymbolTable(), EHandler);
		}

		public Runtime(string fileName) : this(fileName, new SymbolTable(), new ErrorHandler())
		{}

		internal Runtime(string fileName, SymbolTable STable, ErrorHandler EHandler)
		{
			this.EHandler = EHandler;
			EHandler.parseError = false;

			var reader = File.OpenText(fileName);
			var input = new AntlrInputStream(reader);
			reader.Close();
			sharedRuntime(input, STable, EHandler);
		}

		internal void sharedRuntime(AntlrInputStream input, SymbolTable STable, ErrorHandler EHandler)
		{
			var lexer = new MosesLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);

			var parser = new MosesParser(tokens);
			parser.RemoveErrorListeners();
			parser.AddErrorListener(EHandler);
			tree = parser.chunk();
			//Console.WriteLine(tree.ToStringTree(parser));
			if (EHandler.parseError)
				EHandler.throwParseError();

			if (STable.EHandler == null)
				STable.EHandler = EHandler;
			var collector = new CollectorVisitor();
			collector.STable = STable;
			collector.EHandler = EHandler;
			collector.Visit(tree);

			interop.STable = STable;
			interop.MVisitor = visitor;
			interop.EHandler = EHandler;
			visitor.STable = STable;
			visitor.interop = interop;
			visitor.EHandler = EHandler;
		}

		//seperate execution to allow host to add its own functions
		public void execute()
		{
			visitor.Visit(tree);
		}
	}
}
