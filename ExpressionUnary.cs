namespace ExpressionInterpreter
{
	using System;
	
	public class ExpressionUnary : AExpression
	{
		public ExpressionUnary(AExpression right, string sign)
		{
			Right = right;
			Sign = sign;
		}
        
		public readonly AExpression Right;
		public readonly string Sign;
		
		public override double Evaluate(EvaluationContext context)
		{
			return Sign switch
			{
				"+" => Right.Evaluate(context),
				"-" => -Right.Evaluate(context),
				_   => throw new Exception($"Unknown sign {Sign} to evaluate {nameof(ExpressionUnary)}."),
			};
		}
	}
}
