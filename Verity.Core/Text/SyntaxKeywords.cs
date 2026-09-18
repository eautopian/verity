using Verity.Core.Text;

namespace Verity.Core.Text;

public static class SyntaxKeywords
{
	public static readonly Dictionary<string, SyntaxKind> OperationsDictionary = new([
		new KeyValuePair<string, SyntaxKind>("+", SyntaxKind.Plus),
		new KeyValuePair<string, SyntaxKind>("-", SyntaxKind.Minus),
		new KeyValuePair<string, SyntaxKind>("*", SyntaxKind.Star),
		new KeyValuePair<string, SyntaxKind>("/", SyntaxKind.RightSlash),
	]);

	private static readonly Dictionary<SyntaxKind, string> _operationDisplayBySyntax =
		OperationsDictionary.ToDictionary(obj => obj.Value, obj => obj.Key);

	public static string? OperationToString(SyntaxKind syntax) =>
		_operationDisplayBySyntax.GetValueOrDefault(syntax);
}

