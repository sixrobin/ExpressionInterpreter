namespace ExpressionInterpreter
{
    using System.Collections.Generic;

    public class Tester
    {
        public void Run()
        {
            Lexer lexer = new Lexer("B - 9 < 7");
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            AExpression expression = parser.Parse();
            
            bool result1 = expression.EvaluateToBool(new EvaluationContext()
            {
                Object = new TestContext(),
                // Variables = new Dictionary<string, double>()
                // {
                //     { "b", 1 },
                // },
            });
            
            Godot.GD.Print("Test 1: " + result1);
            
            Godot.GD.Print("Test 2: " + new Expression("randi(10, 30)").EvaluateToDouble());
            
            Godot.GD.Print("Test 3: " + new Expression("4 < 5").EvaluateToBool());
            Godot.GD.Print("Test 4: " + new Expression("4 = 5").EvaluateToBool());
            Godot.GD.Print("Test 5: " + new Expression("4 > 5").EvaluateToBool());
        }
    }

    public class TestContext
    {
        public int B { get; set; } = 6;
    }
}