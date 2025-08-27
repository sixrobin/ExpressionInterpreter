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
                        "/" => right != 0.0 ? left / right : throw new DivideByZeroException(),
                        "%" => left % right,
                        "^" => Math.Pow(left, right),
                        _   => throw new Exception($"Unknown operator {binary.Operator} to evaluate expression."),
                    };
                
                case UnaryExpression unary:
                    return unary.Sign switch
                    {
                        "+" => Evaluate(unary.Right),
                        "-" => -Evaluate(unary.Right),
                        _   => throw new Exception($"Unknown unary sign {unary.Sign} to evaluate expression."),
                    };
                
                case FunctionExpression function:
                    return function.FunctionName switch
                    {
                        "abs"   => Math.Abs(Evaluate(function.Args[0])),
                        "cos"   => Math.Cos(Evaluate(function.Args[0])),
                        "sin"   => Math.Sin(Evaluate(function.Args[0])),
                        "floor" => Math.Floor(Evaluate(function.Args[0])),
                        "ceil"  => Math.Ceiling(Evaluate(function.Args[0])),
                        "round" => Math.Round(Evaluate(function.Args[0])),
                        "min"   => Math.Min(Evaluate(function.Args[0]), Evaluate(function.Args[1])),
                        "max"   => Math.Max(Evaluate(function.Args[0]), Evaluate(function.Args[1])),
                        _       => throw new Exception($"Unknown function name {function.FunctionName}"),
                    };
                
                default:
                    throw new Exception($"Unknown expression type {expression.GetType()}");
            }
        }
    }
}