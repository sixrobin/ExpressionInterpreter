namespace ExpressionInterpreter
{
    public abstract class AExpression
    {
        public abstract double EvaluateToDouble(EvaluationContext context);
        public double EvaluateToDouble() => EvaluateToDouble(EvaluationContext.Empty);

        public bool EvaluateToBool(EvaluationContext context) => EvaluateToDouble(context) != 0;
        public bool EvaluateToBool() => EvaluateToBool(EvaluationContext.Empty);
    }
}