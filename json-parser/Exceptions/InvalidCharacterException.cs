namespace json_parser.Exceptions
{
    public class InvalidCharacterException(char character, int position) :
            JsonParserException($"Invalid character: {character} at position {position}")
    {
    }
}
