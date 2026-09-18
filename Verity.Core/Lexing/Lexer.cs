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

			if (AtWhitespace())
			{
				yield return LexWhitespace(_position);
				continue;
			}

			switch (character)
			{
				case '+':
					yield return LexTemporaryReplaceThis(_position, 1, SyntaxKind.Plus, true);
					continue;
				case '-':
					yield return LexTemporaryReplaceThis(_position, 1, SyntaxKind.Minus, true);
					continue;
				case '*':
					yield return LexTemporaryReplaceThis(_position, 1, SyntaxKind.Star, true);
					continue;
				case '/':
					yield return LexTemporaryReplaceThis(_position, 1, SyntaxKind.Slash, true);
					continue;
			}

			if (AtNumber())
			{
				yield return LexNumber(_position);
				continue;
			}

			Console.WriteLine("unexpected character " + character);
			Advance();
			break;
		}

		yield return new Token(SyntaxKind.Eof);
	}

	private Token LexTemporaryReplaceThis(int start, int length, SyntaxKind kind, bool? stripText)
	{
		Advance(length);
		return MakeToken(kind, start, stripText);
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
