namespace ExpressionInterpreter
{
    using System;
    using System.Collections.Generic;

    public class Lexer
    {
        private const char CHAR_END = '\0';
        
        private static readonly Dictionary<char, Token> SHARED_TOKENS = new Dictionary<char, Token>()
        {
            {'+', new Token(Token.TokenType.PLUS, "+")},
            {'-', new Token(Token.TokenType.MINUS, "-")},
            {'*', new Token(Token.TokenType.MULTIPLY, "*")},
            {'/', new Token(Token.TokenType.DIVIDE, "/")},
            {'%', new Token(Token.TokenType.MODULO, "%")},
            {'^', new Token(Token.TokenType.POWER, "^")},
            {'(', new Token(Token.TokenType.OPEN_PARENTHESIS, "(")},
            {')', new Token(Token.TokenType.CLOSE_PARENTHESIS, ")")},
            {',', new Token(Token.TokenType.COMMA, ",")},
        };
        
        public Lexer(string text)
        {
            _text = text;
        }
    
        private readonly string _text;
        private int _position;
    
        private char GetCurrent()
        {
            return _position < _text.Length ? _text[_position] : CHAR_END;
        }

        private void Next()
        {
            _position++;
        }
    
        private void SkipWhitespace()
        {
            while (char.IsWhiteSpace(GetCurrent()))
                Next();
        }
    
        private string GetNumber()
        {
            string result = "";
            while (char.IsDigit(GetCurrent()) || GetCurrent() == '.')
            {
                result += GetCurrent();
                Next();
            }
        
            return result;
        }

        private string GetIdentifier()
        {
            string result = "";
            while (char.IsLetterOrDigit(GetCurrent()) || GetCurrent() == '_')
            {
                result += GetCurrent();
                Next();
            }
        
            return result;
        }
    
        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            while (GetCurrent() != CHAR_END)
            {
                SkipWhitespace();

                if (char.IsDigit(GetCurrent()))
                {
                    tokens.Add(new Token(Token.TokenType.NUMBER, GetNumber()));
                }
                else if (char.IsLetter(GetCurrent()))
                {
                    tokens.Add(new Token(Token.TokenType.IDENTIFIER, GetIdentifier()));
                }
                else if (SHARED_TOKENS.TryGetValue(GetCurrent(), out Token token))
                {
                    tokens.Add(token);
                    Next();
                }
                else
                {
                    throw new Exception($"Unexpected character {GetCurrent()} in {nameof(Lexer)}.");
                }
            }

            tokens.Add(new Token(Token.TokenType.END, string.Empty));
            return tokens;
        }
    }
}