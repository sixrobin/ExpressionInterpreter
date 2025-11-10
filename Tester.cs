namespace ExpressionInterpreter
{
    using System.Collections.Generic;

    public class Tester
    {
        public void Run()
        {
            Lexer lexer = new Lexer("min(5, 2) * B");
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

            double result = expression.Evaluate();
            
            Godot.GD.Print(result);
        }
    }

    public class TestContext
    {
        public int B { get; set; } = 3;
    }
}