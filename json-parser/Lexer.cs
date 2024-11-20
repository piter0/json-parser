using json_parser.Exceptions;
using json_parser.Models;
using System.Globalization;
using System.Text;

namespace json_parser
{
    public class Lexer(string input)
    {
        private readonly string input = input;
        private readonly int inputLength = input.Length;
        private int position = 0;
        private readonly string trueText = "true";
        private readonly string falseText = "false";
        private readonly string nullText = "null";
        private readonly int trueLength = 4;
        private readonly int falseLength = 5;
        private readonly int nullLength = 4;

        public List<JsonToken> Tokenize()
        {
            var tokens = new List<JsonToken>();

            while (position < inputLength)
            {
                switch (input[position])
                {
                    case '\"':
                        tokens.Add(JsonString());
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '-':
                    case 'e':
                    case '.':
                        tokens.Add(JsonNumber());
                        break;
                    case 't':
                        tokens.Add(JsonTrue());
                        break;
                    case 'f':
                        tokens.Add(JsonFalse());
                        break;
                    case 'n':
                        tokens.Add(JsonNull());
                        break;
                    case ' ':
                    case '\n':
                    case '\t':
                    case '\r':
                        position++;
                        break;
                    case '{':
                        tokens.Add(new JsonToken("{", TokenType.OpenCurlyBracket));
                        position++;
                        break;
                    case '}':
                        tokens.Add(new JsonToken("}", TokenType.CloseCurlyBracket));
                        position++;
                        break;
                    case '[':
                        tokens.Add(new JsonToken("[", TokenType.OpenSquareBracket));
                        position++;
                        break;
                    case ']':
                        tokens.Add(new JsonToken("]", TokenType.CloseSquareBracket));
                        position++;
                        break;
                    case ':':
                        tokens.Add(new JsonToken(":", TokenType.Colon));
                        position++;
                        break;
                    case ',':
                        tokens.Add(new JsonToken(",", TokenType.Comma));
                        position++;
                        break;

                    default:
                        throw new InvalidCharacterException(input[position], position);
                }
            }

            return tokens;
        }

        private JsonToken JsonString()
        {
            var sb = new StringBuilder();
            for (var i = position + 1; i < inputLength; i++)
            {
                if (input[i] != '\"')
                {
                    if (input[i] == '\\')
                    {
                        if (i + 1 >= input.Length - 1)
                            throw new InvalidCharacterInStringException('\\', i);

                        char next = input[++i];
                        if (next == 'u')
                        {
                            if (i + 4 >= input.Length - 1 || !input.Substring(i + 1, 4).All("0123456789abcdefABCDEF".Contains))
                                throw new InvalidCharacterInStringException(next, i);

                            sb.Append((char)Convert.ToInt32(input.Substring(i + 1, 4), 16));
                            i += 4;
                        }
                        else if ("\\\"/rtnfb".Contains(next))
                        {
                            sb.Append(next switch
                            {
                                'r' => '\r',
                                't' => '\t',
                                'n' => '\n',
                                'f' => '\f',
                                'b' => '\b',
                                _ => next
                            });
                        }
                        else
                        {
                            throw new InvalidCharacterInStringException(next, i);
                        }
                    }
                    else if (input[i] is '\t' or '\r' or '\n')
                    {
                        throw new InvalidCharacterInStringException(input[i], position);
                    }

                    sb.Append(input[i]);
                }
                else
                {
                    position = i + 1;
                    break;
                }
            }

            return new JsonToken(sb.ToString(), TokenType.String);
        }

        private JsonToken JsonNumber()
        {
            int start = position;
            int inputLength = input.Length;

            if (position < inputLength && input[position] == '0')
            {
                if (position + 1 < inputLength && char.IsDigit(input[position + 1]))
                {
                    throw new InvalidNumberException("Leading zeros are not allowed.");
                }
            }
            else if (position < inputLength && input[position] == '.')
            {
                throw new InvalidNumberException("Numbers cannot start with a decimal point.");
            }

            for (; position < inputLength; position++)
            {
                char c = input[position];

                if (!"0123456789-+eE.".Contains(c))
                {
                    break;
                }
            }

            string number = input[start..position];

            if (!double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            {
                throw new InvalidNumberException(number);
            }

            return new JsonToken(number, TokenType.Number);
        }

        private JsonToken JsonTrue()
        {
            var trueToken = input.Substring(position, trueLength);
            if (inputLength - position >= trueLength && trueToken == trueText)
            {
                position += trueLength;
                return new JsonToken(trueText, TokenType.True);
            }

            throw new UnexpectedTokenException(trueToken);
        }

        private JsonToken JsonFalse()
        {
            var falseToken = input.Substring(position, falseLength);
            if (inputLength - position >= falseLength && falseToken == falseText)
            {
                position += falseLength;
                return new JsonToken(falseText, TokenType.False);
            }

            throw new UnexpectedTokenException(falseToken);
        }

        private JsonToken JsonNull()
        {
            var nullToken = input.Substring(position, nullLength);
            if (inputLength - position >= nullLength && nullToken == nullText)
            {
                position += nullLength;
                return new JsonToken(nullText, TokenType.Null);
            }

            throw new UnexpectedTokenException(nullToken);
        }
    }
}
