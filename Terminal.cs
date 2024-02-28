using MigScriptInterpreter;
using System;
 namespace MigScriptTerminal
{
	public class Interpreter
	{
		public void Interpret(List<Token> tokens)
		{
			foreach (var token in tokens)
			{
				Console.WriteLine($"Token: {token.Lexeme} | Type: {token.Type} | Line: {token.LineNumber}");
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("MigScript");
			Console.WriteLine("Made by drimerdev (2024)");

			string source = "";
			string line;
			while ((line = Console.ReadLine()) != "")
			{
				source += line + "\n";
			}

			Lexer lexer = new(source);
			List<Token> tokens = lexer.Tokenize();

			Interpreter interpreter = new();
			interpreter.Interpret(tokens);
		}
	}

	class Terminal
	{
        static void main(string[] args)
		{
			Console.WriteLine("Terminal");
		}
	}
}