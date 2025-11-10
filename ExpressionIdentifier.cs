namespace ExpressionInterpreter
{
	using System;
	using System.Reflection;
	
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

			if (context.Object != null)
			{
				Type contextType = context.Object.GetType();
				
				FieldInfo[] fieldsInfo = contextType.GetFields();
				foreach (FieldInfo fieldInfo in fieldsInfo)
					if (fieldInfo.Name == Value)
						return Convert.ToDouble(fieldInfo.GetValue(context.Object));
				
				PropertyInfo[] propertiesInfo = contextType.GetProperties();
				foreach (PropertyInfo propertyInfo in propertiesInfo)
					if (propertyInfo.Name == Value)
						return Convert.ToDouble(propertyInfo.GetValue(context.Object));
			}
			
			throw new Exception($"Could not parse {nameof(ExpressionIdentifier)} value {Value} to a valid double.");
		}
	}
}
