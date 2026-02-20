namespace ExpressionInterpreter
{
    using System.Collections.Generic;

    public class Tester
    {
        public void Run()
        {
            Lexer lexer = new Lexer("randi(1, 3)");
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            AExpression expression = parser.Parse();
            
            EvaluationContext context = new()
            {
                Object = new TestContext(),
                Variables = new Dictionary<string, double>
                {
                    { "b", 3 },
                },
            };

            double result1 = expression.Evaluate(new EvaluationContext() {
                Object = new TestContext(),
                Variables = new Dictionary<string, double>(),
            });
            
            Godot.GD.Print("Test 1: " + result1);
            
            Godot.GD.Print("Test 2: " + new Expression("randi(10, 30)").Evaluate());
        }
    }

    public class TestContext
    {
        public int B { get; set; } = 3;
    }
}