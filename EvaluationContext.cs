namespace ExpressionInterpreter
{
	using System.Collections.Generic;

	public struct EvaluationContext
	{
		public readonly static EvaluationContext Empty = new()
		{
			Object = null,
			Variables = new Dictionary<string, double>(),
		};
		
		public object Object;
		public Dictionary<string, double> Variables;
	}
}
