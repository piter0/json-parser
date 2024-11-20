using json_parser.Exceptions;
using json_parser.Models;

namespace json_parser
{
    public static class Parser
    {
        public static bool TryParse(List<JsonToken> tokens)
        {
            var result = Parse(tokens);
            if (result.Count == 0)
            {
                return true;
            }
            else
            {
                throw new SyntaxException($"Unexpected non-whitespace character {result[0].Value} " +
                    $"after JSON");
            }
        }

        private static List<JsonToken> Parse(List<JsonToken> tokens)
        {
            if (tokens[0].Type is TokenType.OpenCurlyBracket)
            {
                return ParseObject(tokens[1..]);
            }
            else if (tokens[0].Type is TokenType.OpenSquareBracket)
            {
                return ParseArray(tokens[1..]);
            }

            return ParseValue(tokens);
        }

        private static List<JsonToken> ParseObject(List<JsonToken> tokens)
        {
            if (tokens[0].Type is TokenType.CloseCurlyBracket)
            {
                return tokens[1..];
            }

            while (true)
            {
                var key = tokens[0];

                if (key.Type is TokenType.String)
                {
                    tokens = tokens[1..];
                }
                else
                {
                    throw new SyntaxException($"Expected string key, got: {key.Value}");
                }

                if (tokens[0].Type is not TokenType.Colon)
                {
                    throw new SyntaxException($"Expected colon after key in object, got: {tokens[0].Value}");
                }

                tokens = Parse(tokens[1..]);

                if (tokens[0].Type is TokenType.CloseCurlyBracket)
                {
                    return tokens[1..];
                }
                else if (tokens[0].Type is TokenType.Comma)
                {
                    tokens = tokens[1..];
                }
                else
                {
                    throw new SyntaxException($"Expected comma or }} after value in object, got: {tokens[0].Value}");
                }
            }
        }

        private static List<JsonToken> ParseArray(List<JsonToken> tokens)
        {
            if (tokens[0].Type is TokenType.CloseSquareBracket)
            {
                return tokens[1..];
            }

            while (true)
            {
                tokens = Parse(tokens);

                if (tokens[0].Type is TokenType.CloseSquareBracket)
                {
                    return tokens[1..];
                }
                else if (tokens[0].Type is TokenType.Comma)
                {
                    tokens = tokens[1..];
                }
                else
                {
                    throw new SyntaxException($"Expected comma after element in array, got: {tokens[0].Value}");
                }
            }
        }

        private static List<JsonToken> ParseValue(List<JsonToken> tokens)
        {
            var token = tokens[0];

            if (token.Type is not TokenType.String and not TokenType.Number
                and not TokenType.True and not TokenType.False and not TokenType.Null)
            {
                throw new UnexpectedTokenException(token.Value);
            }

            return tokens[1..];
        }
    }
}