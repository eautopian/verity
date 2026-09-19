using Verity.Core.Lexing;
using Verity.Core.Parsing.AST;
using Verity.Core.Text;

namespace Verity.Core.Parsing;

public sealed class Parser(LexerResult lexerResult)
{
	private readonly LexerResult _lexerResult = lexerResult;
	private readonly int _tokenCount = lexerResult.Tokens.Count;
	private int _position = 0;

	// Plus (+), Minus (-)
	private ASTNode ParseExpression()
	{
		AdvanceWhile(AtWhitespace);

		var left = ParseTerm();

		AdvanceWhile(AtWhitespace);

		while (Current.Kind == SyntaxKind.Plus || Current.Kind == SyntaxKind.Minus)
		{
			var kind = Consume().Kind;

			AdvanceWhile(AtWhitespace);

			var right = ParseTerm();

			left = new BinaryExpression(left, kind, right);

			AdvanceWhile(AtWhitespace);
		}

		return left;
	}

	// Star (*), RightSlash (/)
	private ASTNode ParseTerm()
	{
		AdvanceWhile(AtWhitespace);

		var left = ParseFactor();

		AdvanceWhile(AtWhitespace);

		while (Current.Kind == SyntaxKind.Star || Current.Kind == SyntaxKind.RightSlash)
		{
			var kind = Consume().Kind;

			AdvanceWhile(AtWhitespace);

			var right = ParseFactor();

			left = new BinaryExpression(left, kind, right);

			AdvanceWhile(AtWhitespace);
		}

		return left;
	}

	// Numbers/Factors (1, 2, 3, etc)
	private ASTNode ParseFactor()
	{
		AdvanceWhile(AtWhitespace);

		if (Current.Kind != SyntaxKind.Number)
			throw new Exception($"Expected a number, but found {Current.Kind}");

		var token = Consume();

		AdvanceWhile(AtWhitespace);

		return new NumberExpression(int.Parse(token.Text));
	}

	public ParserResult Parse()
	{
		AdvanceWhile(AtWhitespace);

		ASTNode expression = ParseExpression();

		AdvanceWhile(AtWhitespace);

		if (!IsEof(1))
		{
			throw new Exception($"Not reached the end {Current.Kind} {_position}/{_tokenCount}");
		}

		return new ParserResult(expression);
	}

	private bool AdvanceWhile(Func<bool> condition)
	{
		bool workDone = false;

		while (!IsEof() && condition())
		{
			workDone = true;
			Advance();
		}

		return workDone;
	}

	private Token Consume()
	{
		if (IsEof())
			throw new Exception("Attempted to consume past the end of the token stream.");

		return _lexerResult.Tokens[_position++];
	}

	private void Advance(int amount = 1)
	{
		_position += amount;

		if (_position > _tokenCount)
			_position = _tokenCount;
	}

	private bool AtWhitespace() => !IsEof() && Current.Kind == SyntaxKind.Whitespace;

	private Token Current => Peek(0);

	private Token Peek(int offset = 0) => _lexerResult.Tokens[_position + offset];

	private bool IsEof(int offset = 0) => _position + offset >= _tokenCount;
}

