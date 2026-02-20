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
        
        public AExpression Parse()
        {
            AExpression expression = ParseAddSubtract();

            if (GetCurrent().Type != Token.TokenType.END)
                throw new Exception($"Expected {Token.TokenType.END} after expression parsing, found {GetCurrent().Type} instead.");

            return expression;
        }
        
        public AExpression ParseAddSubtract()
        {
            AExpression node = ParseMultiplyDivideModulo();

            while (GetCurrent().Type == Token.TokenType.PLUS || GetCurrent().Type == Token.TokenType.MINUS)
            {
                string op = GetCurrent().Value;
                Next();
                AExpression right = ParseMultiplyDivideModulo();
                node = new ExpressionBinary(node, right, op);
            }

            return node;
        }

        private AExpression ParseMultiplyDivideModulo()
        {
            AExpression node = ParseExponent();

            while (GetCurrent().Type == Token.TokenType.MULTIPLY || GetCurrent().Type == Token.TokenType.DIVIDE || GetCurrent().Type == Token.TokenType.MODULO)
            {
                string op = GetCurrent().Value;
                Next();
                AExpression right = ParseExponent();
                node = new ExpressionBinary(node, right, op);
            }

            return node;
        }

        private AExpression ParseExponent()
        {
            AExpression node = ParseLeaf();

            while (GetCurrent().Type == Token.TokenType.POWER)
            {
                string op = GetCurrent().Value;
                Next();
                AExpression right = ParseLeaf();
                node = new ExpressionBinary(node, right, op);
            }

            return node;
        }
        
        private AExpression ParseLeaf()
        {
            switch (GetCurrent().Type)
            {
                case Token.TokenType.PLUS:
                    Next();
                    return new ExpressionUnary(ParseLeaf(), "+");
                
                case Token.TokenType.MINUS:
                    Next();
                    return new ExpressionUnary(ParseLeaf(), "-");
                
                case Token.TokenType.NUMBER:
                    string value = GetCurrent().Value;
                    Next();
                    return new ExpressionNumber(value);
                
                case Token.TokenType.IDENTIFIER:
                    string id = GetCurrent().Value;
                    Next();

                    // Function.
                    if (GetCurrent().Type == Token.TokenType.OPEN_PARENTHESIS)
                    {
                        Next();
                        List<AExpression> args = new List<AExpression>();
                        
                        if (GetCurrent().Type != Token.TokenType.CLOSE_PARENTHESIS)
                        {
                            do
                            {
                                args.Add(ParseAddSubtract());
                                if (GetCurrent().Type == Token.TokenType.COMMA)
                                    Next();
                                else
                                    break;
                            }
                            while (true);
                        }

                        Next();
                        return new ExpressionFunction(id, args);
                    }
                    
                    return new ExpressionIdentifier(id);
                
                case Token.TokenType.OPEN_PARENTHESIS:
                    Next();
                    AExpression expression = ParseAddSubtract();
                    Next();
                    return expression;
                
                default:
                    throw new Exception($"Unexpected leaf token {GetCurrent().Type}.");
            }
        }
    }
}