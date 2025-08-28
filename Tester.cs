namespace ExpressionInterpreter
{
    using System.Collections.Generic;

    public class Tester
    {
        public Tester()
        {
        }

        public void Run()
        {
            Lexer lexer = new Lexer("min(5, 2) * 4");
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expression = parser.Parse();
            
            EvaluationContext context = new()
            {
                Variables = new Dictionary<string, double>
                {
                    { "b", 3 },
                },
            };

            double result = expression.Evaluate();
            
            Godot.GD.Print(result);
        }
    }
}