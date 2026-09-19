> - 🤖 No AI was used in the creation of this, all my own hard work.
> - This is my first project using C# and I made it to learn how C# works
> - This is not a throwaway project, I hope to develop it more and refactor in the future

# Verity
Verity is a toy number "language" that I hope to develop and improve more in the future when I get more skills into either a [DSL](https://en.wikipedia.org/wiki/Domain-specific_language) or a fully fleshed out programming language.

<div align="center">
    <img src="assets/logo.png" width=200 height=200 />
</div>

# How it works (currently)

As of right now Verity is a [REPL](https://en.wikipedia.org/wiki/Read%E2%80%93eval%E2%80%93print_loop) which allows you to input DMAS, BI still needs to be implemented.

REPL -> Lexer -> Parser -> Evaluator

```cs
1 + 1 == 2
1 + 2 * 3 == 7
```

- [x] Unit Testing
- [x] Lexer
- [x] Parser
- [x] Evaluator
- [x] Division, Multiplication, Addition, Subtraction
- [x] REPL
- [ ] Brackets ()    (3 * 2 + 4) * 4 = 40
- [ ] Indices ^2     2+2*3^2 = 2 + (2 * 3 ^ 2) = 20

In the future, I plan to get Verity to a working state which allows something simple like;
```js
let x = 69
let y = 420
let z = x + y * (x - y ^ 2)
writeln z
```

## Lexer
Located [here](https://github.com/eautopian/verity/blob/main/Verity.Core/Lexing/Lexer.cs)
Every input is ran through the lexer to begin with where it returns data i.e. [Tokens](https://github.com/eautopian/verity/blob/main/Verity.Core/Text/Token.cs);
```
Tokens: List<Token>
```
This can then be used by the Parser (see below) 

## Parser
Located [here](https://github.com/eautopian/verity/blob/main/Verity.Core/Parsing/Parser.cs)

The parser first tries to `ParseExpression()` which first requires `ParseTerm()` which first requires `ParseFactor()`, allowing for the proper order of DMAS to take place and it outputs a `ParserResult` which has a `ASTNode` which can be passed to the Evaluator

## Evaluator
Located [here](https://github.com/eautopian/verity/blob/main/Verity.Core/ExpressionEvaluator/Evaluator.cs)

Goes through the `ASTNode` provided recursively looking for a `NumberExpression` which then returns the expression.Value, if it is instead given a `BinaryExpression` it will calculate both the left and right and use the `SyntaxKind` i.e. `SyntaxKind.Star` and realise that it needs to multiply. After all recursion is done, it returns a single float value of the evaluated expression.

## Unit Testing
Located [here](https://github.com/eautopian/verity/blob/main/Verity.Testing)

Unit Testing was done via Xunit, it's quite simple and only has one test at the moment which expects all the expression related features to successfully work.