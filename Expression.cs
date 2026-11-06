namespace ExpressionInterpreter
{
    using System;
    using RSLib.GE.Debug;

    public class Expression : AExpression
    {
        public Expression(string value)
        {
            Value = value;
        }
        
        public Expression(int value) : this(value.ToString()) { }
        public Expression(double value) : this(value.ToString(System.Globalization.CultureInfo.InvariantCulture)) { }
        
        public readonly string Value;
        
        public override double EvaluateToDouble(EvaluationContext context)
        {
            try
            {
                AExpression expression = new Parser(new Lexer(Value).Tokenize()).Parse();
                return expression.EvaluateToDouble(context);
            }
            catch (Exception e)
            {
                Debugger.Console.Error($"Exception caught during {nameof(Expression)} {Value} evaluation, returning 0. Exception message: {e.Message}");
                return 0;
            }
        }
    }
}