namespace json_parser.Models
{
    public class JsonToken(string value, TokenType type)
    {
        public string Value { get; set; } = value;
        public TokenType Type { get; set; } = type;
    }
}