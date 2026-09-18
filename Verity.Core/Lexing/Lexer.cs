using Verity.Core.Text;

namespace Verity.Core.Lexing;

public sealed class Lexer(string input)
{
	private readonly string _input = input;
	private readonly int _length = input.Length;
	private int _position = 0;

	public LexerResult Tokenize()
	{
		var tokens = new List<Token>();

		foreach (var token in CollectTokens())
			tokens.Add(token);

		return new LexerResult(tokens);
	}

	private IEnumerable<Token> CollectTokens()
	{
		var tokens = new List<Token>();

		while (!IsEof())
		{
			char character = Current();
			int start = _position;

			if (AtWhitespace())
			{
				yield return LexWhitespace(start);
				continue;
			}

			if (AtNumber())
			{
				yield return LexNumber(start);
				continue;
			}

			var operation = OperationsMatcher.TryMatch(_input, start);
			if (operation is SyntaxKind matchedOperation)
			{
				Advance(1);
				yield return MakeToken(matchedOperation, start);
				continue;
			}

			throw new Exception("unexpected character " + character);
		}

		yield return new Token(SyntaxKind.Eof);
	}

	private Token LexWhitespace(int start)
	{
		AdvanceWhile(AtWhitespace);
		return MakeToken(SyntaxKind.Whitespace, start);
	}

	private Token LexNumber(int start)
	{
		AdvanceWhile(AtNumber);
		return MakeToken(SyntaxKind.Number, start);
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

	private Token MakeToken(SyntaxKind kind, int start, bool? stripText = false) =>
		new(kind, stripText == true ? "" : _input[start.._position]);

	private void Advance(int amount = 1) => _position += amount;

	private bool AtWhitespace() => char.IsWhiteSpace(Current());

	private bool AtNumber() => char.IsNumber(Current());

	private char Current() => Peek(0);

	private char Peek(int offset = 0) =>
		IsEof(offset) ? _input[_length - 1] : _input[_position + offset];

	private bool IsEof(int offset = 0) => _position + offset >= _length;
}
