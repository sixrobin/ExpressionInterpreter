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
            Lexer lexer = new Lexer("(2 + 2) * b");
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Evaluator evaluator = new Evaluator(new Dictionary<string, double>
            {
                { "b", 4 },
            });

            double result = evaluator.Evaluate(parser.Parse());
            Godot.GD.Print(result);
        }
    }
}