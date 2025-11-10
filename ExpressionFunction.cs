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

		private static Func<double, double, double> _rand = (min, max) => min + new Random().NextDouble() * (max - min);
		private static Func<int, int, int> _randi = (min, max) => new Random().Next(min, max + 1);
		
		public readonly string FunctionName;
		public readonly List<Expression> Args;

		public static void SetRand(Func<double, double, double> rand)
		{
			_rand = rand;
		}
		
		public static void SetRandi(Func<int, int, int> randi)
		{
			_randi = randi;
		}

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
				"rand"  => _rand.Invoke(Args[0].Evaluate(context), Args[1].Evaluate(context)),
				"randi" => _randi.Invoke((int)Args[0].Evaluate(context), (int)Args[1].Evaluate(context)),
				_       => throw new Exception($"Unknown function name {FunctionName} to evaluate {nameof(ExpressionFunction)}."),
			};
		}
	}
}
