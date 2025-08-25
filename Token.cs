namespace ExpressionInterpreter
{
    public class Token
    {
        public enum TokenType
        {
            NUMBER,
            IDENTIFIER,
            PLUS,
            MINUS,
            MULTIPLY,
            DIVIDE,
            OPEN_PARENTHESIS,
            CLOSE_PARENTHESIS,
            END,
        }
    
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    
        public TokenType Type { get; }
        public string Value { get; }
    }
}