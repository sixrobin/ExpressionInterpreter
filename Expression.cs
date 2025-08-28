namespace ExpressionInterpreter
{
    public abstract class Expression
    {
        public abstract double Evaluate(EvaluationContext context);
        public double Evaluate() => Evaluate(new EvaluationContext());
    }
}