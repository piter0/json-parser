namespace json_parser.Exceptions
{
    public class InvalidNumberException(string number) : 
        JsonParserException($"Number is not valid: {number}")
    {
    }
}