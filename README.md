## json-parser
Simple JSON parser written in C#. Returns `true` if JSON is OK, otherwise throws proper exception.

### usage


```
var json = @"{"key1": "abc", "key2": 42}";

var lexer = new Lexer(json);

Console.WriteLine(Parser.TryParse(lexer.Tokenize())); //true
```