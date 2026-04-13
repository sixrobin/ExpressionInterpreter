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
		
		public override double EvaluateToDouble(EvaluationContext context)
		{
			return Sign switch
			{
				"+" => Right.EvaluateToDouble(context),
				"-" => -Right.EvaluateToDouble(context),
				_   => throw new Exception($"Unknown sign {Sign} to evaluate {nameof(ExpressionUnary)}."),
			};
		}
	}
}
