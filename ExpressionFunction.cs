namespace ExpressionInterpreter
{
	using System;
	using System.Collections.Generic;

	public class ExpressionFunction : Expression
	{
		public ExpressionFunction(string functionName, List<Expression> args)
		{
			FunctionName = functionName;
			Args = args;
		}
        
		public readonly string FunctionName;
		public readonly List<Expression> Args;
		
		public override double Evaluate(EvaluationContext context)
		{
			return FunctionName switch
			{
				"abs"   => Math.Abs(Args[0].Evaluate(context)),
				"cos"   => Math.Cos(Args[0].Evaluate(context)),
				"sin"   => Math.Sin(Args[0].Evaluate(context)),
				"floor" => Math.Floor(Args[0].Evaluate(context)),
				"ceil"  => Math.Ceiling(Args[0].Evaluate(context)),
				"round" => Math.Round(Args[0].Evaluate(context)),
				"min"   => Math.Min(Args[0].Evaluate(context), Args[1].Evaluate(context)),
				"max"   => Math.Max(Args[0].Evaluate(context), Args[1].Evaluate(context)),
				_       => throw new Exception($"Unknown function name {FunctionName} to evaluate {nameof(ExpressionFunction)}."),
			};
		}
	}
}
