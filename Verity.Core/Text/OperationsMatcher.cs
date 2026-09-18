namespace Verity.Core.Text;

public static class OperationsMatcher
{
	public static SyntaxKind? TryMatch(string input, int position)
	{
		bool success = SyntaxKeywords.OperationsDictionary.TryGetValue(
			input[position].ToString(),
			out var syntax
		);
		return success ? syntax : null;
	}
}

