> ‼️ Not all the code is currently uploaded
> A lot of it resides in a .gitignored file
> while I work to clean it up.

# Verity
Verity is a toy language that I hope to develop in the future when I get more skills into either a [DSL](https://en.wikipedia.org/wiki/Domain-specific_language) or a fully fleshed out programming language.

<div align="center">
    <img src="assets/logo.png" width=200 height=200 />
</div>

# Short-term Goal
For the beginning, Verity will start as a console app which will allow you to input mathemetical functions i.e.
```js
2 + 3 * 3
```
and output
```js
11
```

In the future, I plan to get Verity to a working state which allows something simple like;
```js
let x = 69
let y = 420
let z = x + y * (x - y ^ 2)
writeln z
```

# How it works

## Lexer
Located [here](https://github.com/eautopian/verity/blob/main/Verity.Core/Lexing/Lexer.cs)
Every input is ran through the lexer to begin with where it returns data i.e. [Tokens](https://github.com/eautopian/verity/blob/main/Verity.Core/Text/Token.cs);
```
Tokens: List<Token>
```
This can then be used by the Parser (see below)