namespace ExpressionInterpreter
{
    using System;
    using System.Collections.Generic;

    public class Evaluator
    {
        private readonly Dictionary<string, double> _variables;

        public Evaluator(Dictionary<string, double> variables)
        {
            _variables = variables;
        }

        public double Evaluate(Expression expression)
        {
            switch (expression)
            {
                case NumberExpression number:
                    return double.Parse(number.Value, System.Globalization.CultureInfo.InvariantCulture);

                case IdentifierExpression identifier:
                    if (_variables.TryGetValue(identifier.Value, out double value))
                        return value;
                    throw new Exception($"Could not parse {nameof(IdentifierExpression)} value {identifier.Value} to a valid double.");

                case BinaryExpression binary:
                    double left = Evaluate(binary.Left);
                    double right = Evaluate(binary.Right);

                    return binary.Operator switch
                    {
                        "+" => left + right,
                        "-" => left - right,
                        "*" => left * right,
                        "/" => right != 0 ? left / right : throw new DivideByZeroException(),
                        // TODO: Module
                        // TODO: Power
                        _ => throw new Exception($"Unknown operator {binary.Operator} to evaluate expression.")
                    };

                default:
                    throw new Exception($"Unknown expression type {expression.GetType()}");
            }
        }
    }
}