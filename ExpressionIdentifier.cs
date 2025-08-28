namespace ExpressionInterpreter
{
	using System;
	
	public class ExpressionIdentifier : Expression
	{
		public ExpressionIdentifier(string value)
		{
			Value = value;
		}
        
		public readonly string Value;
		
		public override double Evaluate(EvaluationContext context)
		{
			if (context.Variables.TryGetValue(Value, out double value))
				return value;
			
			throw new Exception($"Could not parse {nameof(ExpressionIdentifier)} value {Value} to a valid double.");
		}
	}
}
