using Verity.Core.ExpressionEvaluator;
using Verity.Core.Lexing;
using Verity.Core.Parsing;

namespace Verity.Testing.Expression;

public class ExpressionTest
{
	[Theory]
	[InlineData("1+2", 1 + 2)]
	[InlineData("1-2", 1 - 2)]
	[InlineData("1*2", 1 * 2)]
	[InlineData("1/2", 1f / 2f)]
	[InlineData("1+2+3", 1 + 2 + 3)]
	[InlineData("10-2-3", 10 - 2 - 3)]
	[InlineData("2*3*4", 2 * 3 * 4)]
	[InlineData("24/2/3", 24f / 2f / 3f)]
	[InlineData("1+2*3", 1 + 2 * 3)]
	[InlineData("1-2*3", 1 - 2 * 3)]
	[InlineData("1+6/2", 1 + 6f / 2f)]
	[InlineData("10-6/2", 10 - 6f / 2f)]
	[InlineData("2*3+4", 2 * 3 + 4)]
	[InlineData("2*3-4", 2 * 3 - 4)]
	[InlineData("6/2+4", 6f / 2f + 4)]
	[InlineData("6/2-4", 6f / 2f - 4)]
	[InlineData("1+2*3-4", 1 + 2 * 3 - 4)]
	[InlineData("10-2*3+4", 10 - 2 * 3 + 4)]
	[InlineData("2*3+4/2", 2 * 3 + 4f / 2f)]
	[InlineData("10/2-3*2", 10f / 2f - 3 * 2)]
	[InlineData("24-28", 24 - 28)]
	[InlineData("24/4", 24f / 4f)]
	[InlineData("5*5", 5 * 5)]
	[InlineData("100-25", 100 - 25)]
	public void Expression_Evaluates(string input, float output)
	{
		var lexerResult = new Lexer(input).Tokenize();
		var parserResult = new Parser(lexerResult).Parse();
		var evaluator = new ExpressionEvaluator(parserResult.Node);

		Assert.Equal(evaluator.Evaluate(), output);
	}
}

