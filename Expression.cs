namespace ExpressionInterpreter
{
    public class Expression : AExpression
    {
        public Expression(string value)
        {
            Value = value;
        }
        
        public Expression(int value) : this(value.ToString()) { }
        public Expression(double value) : this(value.ToString(System.Globalization.CultureInfo.InvariantCulture)) { }
        
        public readonly string Value;
        
        public override double Evaluate(EvaluationContext context)
        {
            AExpression expression = new Parser(new Lexer(Value).Tokenize()).Parse();
            return expression.Evaluate(context);
        }
    }
}