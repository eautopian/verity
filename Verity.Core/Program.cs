using Verity.Core.ExpressionEvaluator;
using Verity.Core.Lexing;
using Verity.Core.Parsing;

bool run = true;
while (run)
{
	Console.WriteLine("Enter an expression");
	string? expression = await Console.In.ReadLineAsync();

	if (expression is string input)
	{
		try
		{
			var lexerResult = new Lexer(input).Tokenize();
			var parserResult = new Parser(lexerResult).Parse();
			var evaluator = new ExpressionEvaluator(parserResult.Node);
			Console.WriteLine($"-> {input} == {evaluator.Evaluate()}");
			Console.WriteLine();
		}
		catch
		{
			Console.WriteLine("Failed to parse properly");
			throw;
		}
	}
}
