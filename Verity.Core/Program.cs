using Verity.Core.Lexing;

Console.WriteLine("Hello, World!");

var lexerResult = new Lexer("1 + 2 * 3").Tokenize();
var tokens = lexerResult.Tokens;
foreach (var token in tokens)
	Console.WriteLine(token);
