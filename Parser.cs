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
            Expression node = ParseMultiplyDivide();

            while (GetCurrent().Type == Token.TokenType.PLUS || GetCurrent().Type == Token.TokenType.MINUS)
            {
                string op = GetCurrent().Value;
                Next();
                Expression right = ParseMultiplyDivide();
                node = new BinaryExpression(node, right, op);
            }

            return node;
        }

        private Expression ParseMultiplyDivide()
        {
            Expression node = ParseLeaf();

            while (GetCurrent().Type == Token.TokenType.MULTIPLY || GetCurrent().Type == Token.TokenType.DIVIDE)
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
            if (GetCurrent().Type == Token.TokenType.NUMBER)
            {
                string value = GetCurrent().Value;
                Next();
                return new NumberExpression(value);
            }

            if (GetCurrent().Type == Token.TokenType.IDENTIFIER)
            {
                string value = GetCurrent().Value;
                Next();
                return new IdentifierExpression(value);
            }

            if (GetCurrent().Type == Token.TokenType.OPEN_PARENTHESIS)
            {
                Next();
                Expression expression = ParseAddSubtract();
                Next();
                return expression;
            }

            throw new Exception($"Unexpected leaf token {GetCurrent().Type}");
        }
    }
}