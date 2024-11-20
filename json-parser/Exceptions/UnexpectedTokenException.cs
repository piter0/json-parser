namespace json_parser.Exceptions
{
    public class UnexpectedTokenException(string token) 
        : JsonParserException($"Unexpected token: {token}")
    {
    }
}
