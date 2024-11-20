namespace json_parser.Models
{
    public enum TokenType
    {
        String,
        Number,
        True,
        False,
        Null,
        OpenCurlyBracket,
        CloseCurlyBracket,
        OpenSquareBracket,
        CloseSquareBracket,
        Colon,
        Comma
    }
}