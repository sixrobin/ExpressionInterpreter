using System.Collections.Generic;

namespace ExpressionInterpreter
{
    public abstract class Expression
    {
    }

    public class NumberExpression : Expression
    {
        public NumberExpression(string value)
        {
            Value = value;
        }

        public string Value;
    }

    public class IdentifierExpression : Expression
    {
        public IdentifierExpression(string value)
        {
            Value = value;
        }
        
        public string Value;
    }

    public class BinaryExpression : Expression
    {
        public BinaryExpression(Expression left, Expression right, string op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }
        
        public readonly Expression Left;
        public readonly Expression Right;
        public readonly string Operator;
    }

    public class UnaryExpression : Expression
    {
        public UnaryExpression(Expression right, string sign)
        {
            Right = right;
            Sign = sign;
        }
        
        public readonly Expression Right;
        public readonly string Sign;
    }

    public class FunctionExpression : Expression
    {
        public FunctionExpression(string functionName, List<Expression> args)
        {
            FunctionName = functionName;
            Args = args;
        }
        
        public readonly string FunctionName;
        public readonly List<Expression> Args;
    }
}