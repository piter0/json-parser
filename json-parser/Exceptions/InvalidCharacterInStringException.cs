namespace json_parser.Exceptions
{
    public class InvalidCharacterInStringException(char character, int position) :
                JsonParserException($"Invalid character in string: {character} at position {position}")
    {
    }
}
