using json_parser.Exceptions;

namespace json_parser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var input = "{\"one\":\"ABC\",\"two\":-2.1,\"three\":null,\"four\":true,\"six\":false}";
                var lexer = new Lexer(input);

                Console.WriteLine(Parser.TryParse(lexer.Tokenize()));
            }
            catch (JsonParserException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
