namespace ExpressionInterpreter
{
    using System;
    using System.Collections.Generic;

    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _position;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        private Token GetCurrent()
        {
            return _tokens[_position];
        }

        private void Next()
        {
            _position++;
        }
        
        public Expression Parse()
        {
            Expression expression = ParseAddSubtract();

            if (GetCurrent().Type != Token.TokenType.END)
                throw new Exception($"Expected {Token.TokenType.END} after expression parsing, found {GetCurrent().Type} instead.");

            return expression;
        }
        
        public Expression ParseAddSubtract()
        {
            Expression node = ParseMultiplyDivideModulo();

            while (GetCurrent().Type == Token.TokenType.PLUS || GetCurrent().Type == Token.TokenType.MINUS)
            {
                string op = GetCurrent().Value;
                Next();
                Expression right = ParseMultiplyDivideModulo();
                node = new BinaryExpression(node, right, op);
            }

            return node;
        }

        private Expression ParseMultiplyDivideModulo()
        {
            Expression node = ParseExponent();

            while (GetCurrent().Type == Token.TokenType.MULTIPLY || GetCurrent().Type == Token.TokenType.DIVIDE || GetCurrent().Type == Token.TokenType.MODULO)
            {
                string op = GetCurrent().Value;
                Next();
                Expression right = ParseExponent();
                node = new BinaryExpression(node, right, op);
            }

            return node;
        }

        private Expression ParseExponent()
        {
            Expression node = ParseLeaf();

            while (GetCurrent().Type == Token.TokenType.POWER)
            {
                string op = GetCurrent().Value;
                Next();
                Expression right = ParseLeaf();
                node = new BinaryExpression(node, right, op);
            }

            return node;
        }
        
        private Expression ParseLeaf()
        {
            switch (GetCurrent().Type)
            {
                case Token.TokenType.PLUS:
                    Next();
                    return new UnaryExpression(ParseLeaf(), "+");
                
                case Token.TokenType.MINUS:
                    Next();
                    return new UnaryExpression(ParseLeaf(), "-");
                
                case Token.TokenType.NUMBER:
                    string value = GetCurrent().Value;
                    Next();
                    return new NumberExpression(value);
                
                case Token.TokenType.IDENTIFIER:
                    string id = GetCurrent().Value;
                    Next();
                    return new IdentifierExpression(id);
                
                case Token.TokenType.OPEN_PARENTHESIS:
                    Next();
                    Expression expression = ParseAddSubtract();
                    Next();
                    return expression;
                
                default:
                    throw new Exception($"Unexpected leaf token {GetCurrent().Type}");
            }
        }
    }
}