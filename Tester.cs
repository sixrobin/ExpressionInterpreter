namespace ExpressionInterpreter
{
    using System.Collections.Generic;

    public class Tester
    {
        public double Run()
        {
            Lexer lexer = new Lexer("randi(1, 3)");
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expression = parser.Parse();
            
            EvaluationContext context = new()
            {
                Object = new TestContext(),
                Variables = new Dictionary<string, double>
                {
                    { "b", 3 },
                },
            };

            double result = expression.Evaluate(new EvaluationContext() {
                Object = new TestContext(),
                Variables = new Dictionary<string, double>(),
            });
            
            Godot.GD.Print(result);

            return result;
        }
    }

    public class TestContext
    {
        public int B { get; set; } = 3;
    }
}