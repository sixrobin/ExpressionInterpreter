namespace ExpressionInterpreter
{
	using System;
	
	public class ExpressionBinary : Expression
	{
		public ExpressionBinary(Expression left, Expression right, string op)
		{
			Left = left;
			Right = right;
			Operator = op;
		}
        
		public readonly Expression Left;
		public readonly Expression Right;
		public readonly string Operator;

		public override double Evaluate(EvaluationContext context)
		{
			double left = Left.Evaluate(context);
			double right = Right.Evaluate(context);
			
			return Operator switch
			{
				"+" => left + right,
				"-" => left - right,
				"*" => left * right,
				"/" => right != 0.0 ? left / right : throw new DivideByZeroException(),
				"%" => left % right,
				"^" => Math.Pow(left, right),
				_   => throw new Exception($"Unknown operator {Operator} to evaluate {nameof(ExpressionBinary)}."),
			};
		}
	}
}
