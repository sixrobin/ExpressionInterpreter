namespace ExpressionInterpreter
{
	using System;
	
	public class ExpressionBinary : AExpression
	{
		public ExpressionBinary(AExpression left, AExpression right, string op)
		{
			Left = left;
			Right = right;
			Operator = op;
		}
        
		public readonly AExpression Left;
		public readonly AExpression Right;
		public readonly string Operator;

		public override double EvaluateToDouble(EvaluationContext context)
		{
			double left = Left.EvaluateToDouble(context);
			double right = Right.EvaluateToDouble(context);
			
			return Operator switch
			{
				"+" => left + right,
				"-" => left - right,
				"*" => left * right,
				"/" => right != 0.0 ? left / right : throw new DivideByZeroException(),
				"%" => left % right,
				"^" => Math.Pow(left, right),
				"<" => left < right ? 1 : 0,
				">" => left > right ? 1 : 0,
				"=" => Math.Abs(left - right) < 0.0001 ? 1 : 0,
				_   => throw new Exception($"Unknown operator {Operator} to evaluate {nameof(ExpressionBinary)}."),
			};
		}
	}
}
