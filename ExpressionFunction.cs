namespace ExpressionInterpreter
{
	using System;
	using System.Collections.Generic;

	public class ExpressionFunction : AExpression
	{
		public ExpressionFunction(string functionName, List<AExpression> args)
		{
			FunctionName = functionName;
			Args = args;
		}

		private static Func<double, double, double> _randOverride;
		private static Func<int, int, int> _randiOverride;
		
		public readonly string FunctionName;
		public readonly List<AExpression> Args;

		public static void SetRandOverride(Func<double, double, double> rand) => _randOverride = rand;
		public static void SetRandiOverride(Func<int, int, int> randi) => _randiOverride = randi;

		private double Rand(double min, double max) => _randOverride?.Invoke(min, max) ?? min + new Random().NextDouble() * (max - min);
		private double Randi(int min, int max) => _randiOverride?.Invoke(min, max) ?? new Random().Next(min, max + 1);

		public override double Evaluate(EvaluationContext context)
		{
			return FunctionName switch
			{
				"abs"   => Math.Abs(Args[0].Evaluate(context)),
				"cos"   => Math.Cos(Args[0].Evaluate(context)),
				"sin"   => Math.Sin(Args[0].Evaluate(context)),
				"floor" => Math.Floor(Args[0].Evaluate(context)),
				"ceil"  => Math.Ceiling(Args[0].Evaluate(context)),
				"round" => Args.Count == 1 ? Math.Round(Args[0].Evaluate(context)) : Math.Round(Args[0].Evaluate(context), (int)Args[1].Evaluate(context)),
				"min"   => Math.Min(Args[0].Evaluate(context), Args[1].Evaluate(context)),
				"max"   => Math.Max(Args[0].Evaluate(context), Args[1].Evaluate(context)),
				"rand"  => Rand(Args[0].Evaluate(context), Args[1].Evaluate(context)),
				"randi" => Randi((int)Args[0].Evaluate(context), (int)Args[1].Evaluate(context)),
				_       => throw new Exception($"Unknown function name {FunctionName} to evaluate {nameof(ExpressionFunction)}."),
			};
		}
	}
}
