namespace ExpressionInterpreter
{
    using System;
    using System.Collections.Generic;

    public class Lexer
    {
        public Lexer(string text)
        {
            _text = text;
        }
    
        private readonly string _text;
        private int _position;
    
        private char GetCurrent()
        {
            return _position < _text.Length ? _text[_position] : '\0';
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

            while (GetCurrent() != '\0')
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
                else
                {
                    switch (GetCurrent())
                    {
                        case '+':
                            tokens.Add(new Token(Token.TokenType.PLUS, "+"));
                            break;
                        case '-':
                            tokens.Add(new Token(Token.TokenType.MINUS, "-"));
                            break;
                        case '*':
                            tokens.Add(new Token(Token.TokenType.MULTIPLY, "*"));
                            break;
                        case '/':
                            tokens.Add(new Token(Token.TokenType.DIVIDE, "/"));
                            break;
                        case '(':
                            tokens.Add(new Token(Token.TokenType.OPEN_PARENTHESIS, "("));
                            break;
                        case ')':
                            tokens.Add(new Token(Token.TokenType.CLOSE_PARENTHESIS, ")"));
                            break;
                        default:
                            throw new Exception($"Unexpected character {GetCurrent()} in {nameof(Lexer)}");
                    }
                
                    Next();
                }
            }

            tokens.Add(new Token(Token.TokenType.END, ""));
            return tokens;
        }
    }
}