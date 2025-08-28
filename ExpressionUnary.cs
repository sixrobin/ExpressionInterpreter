namespace ExpressionInterpreter
{
	using System;
	
	public class ExpressionUnary : Expression
	{
		public ExpressionUnary(Expression right, string sign)
		{
			Right = right;
			Sign = sign;
		}
        
		public readonly Expression Right;
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
