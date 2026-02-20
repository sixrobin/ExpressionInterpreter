namespace ExpressionInterpreter
{
    public abstract class AExpression
    {
        public abstract double Evaluate(EvaluationContext context);
        public double Evaluate() => Evaluate(EvaluationContext.Empty);
    }
}