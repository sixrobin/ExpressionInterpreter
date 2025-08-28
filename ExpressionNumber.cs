namespace ExpressionInterpreter
{
	public class ExpressionNumber : Expression
	{
		public ExpressionNumber(string value)
		{
			Value = value;
		}

		public readonly string Value;
		
		public override double Evaluate(EvaluationContext context)
		{
			return double.Parse(Value, System.Globalization.CultureInfo.InvariantCulture);
		}
	}
}
