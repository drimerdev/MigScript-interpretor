using System;
using System.Collections.Generic;
using System.Linq;

namespace MigScriptInterpreter
{
    public enum TokenType
    {
        Identifier,
        Number,
        String,
        Keyword,
        Symbol,
        OpenParen,
        CloseParen,
        BinaryOperator,
        Let
        // Add more token types as needed
    }

    public struct Token
    {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public int LineNumber { get; }

        public Token(TokenType type, string lexeme, int lineNumber)
        {
            Type = type;
            Lexeme = lexeme;
            LineNumber = lineNumber;
        }
    }

    public class Lexer
    {
        private readonly string _source;
        private int _currentPosition;
        private int _currentLineNumber;

        public Lexer(string source)
        {
            _source = source;
            _currentPosition = 0;
            _currentLineNumber = 1;
        }

        private bool IsAtEnd() => _currentPosition >= _source.Length;

        private char Advance() => _source[_currentPosition++];

        private char Peek() => IsAtEnd() ? '\0' : _source[_currentPosition];

        private void SkipWhitespace()
        {
            while (!IsAtEnd() && char.IsWhiteSpace(Peek()))
            {
                if (Peek() == '\n') _currentLineNumber++;
                _ = Advance();
            }
        }

        private Token LexNextToken()
        {
            SkipWhitespace();
            if (IsAtEnd()) return new Token(TokenType.Symbol, "", _currentLineNumber);

            char c = Advance();
            if (char.IsLetter(c))
            {
                // Identifier or keyword
                string lexeme = c.ToString();
                while (char.IsLetterOrDigit(Peek()) || Peek() == '_')
                {
                    lexeme += Advance();
                }
                // Check if it's a keyword
                if (lexeme == "let" || lexeme == "writl")
                {
                    return new Token(TokenType.Keyword, lexeme, _currentLineNumber);
                }
                return new Token(TokenType.Identifier, lexeme, _currentLineNumber);
            }
            else if (char.IsDigit(c))
            {
                // Number
                string lexeme = c.ToString();
                while (char.IsDigit(Peek()))
                {
                    lexeme += Advance();
                }
                return new Token(TokenType.Number, lexeme, _currentLineNumber);
            }
            else if (c == '"')
            {
                // String
                string lexeme = "";
                while (Peek() != '"' && !IsAtEnd())
                {
                    if (Peek() == '\n') _currentLineNumber++;
                    lexeme += Advance();
                }
                if (Peek() == '"') _ = Advance(); // Consume the closing quote
                return new Token(TokenType.String, lexeme, _currentLineNumber);
            }
            else
            {
                // Symbol
                return new Token(TokenType.Symbol, c.ToString(), _currentLineNumber);
            }
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new();
            while (!IsAtEnd())
            {
                tokens.Add(LexNextToken());
            }
            return tokens;
        }
    }

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
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@     @");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@  @@@@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   @@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  @@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@    @@@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

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
}
