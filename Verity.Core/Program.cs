using Verity.Core.Lexing;

Console.WriteLine("Hello, World!");

var lexerResult = new Lexer("12 + 1").Tokenize();
var tokens = lexerResult.Tokens;
foreach (var token in tokens)
	Console.WriteLine(token);
