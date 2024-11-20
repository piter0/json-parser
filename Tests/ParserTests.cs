using json_parser;
using json_parser.Exceptions;

namespace Tests
{
    public class ParserTests
    {
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        public void TryParse_ShouldReturnTrue_IfJsonIsValid(string fileNumber)
        {
            //Arrange
            var input = Input.GetValidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act
            var result = Parser.TryParse(lexer.Tokenize());

            //Assert
            Assert.That(result, Is.True);
        }

        [TestCase("14")]
        public void TryParse_ShouldThrowInvalidCharacterException_IfKeyIsNotQuoted(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<InvalidCharacterException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("15")]
        public void TryParse_ShouldThrowInvalidCharacterException_IfValueIsNotString(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<InvalidCharacterException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("16")]
        public void TryParse_ShouldThrowInvalidCharacterException_IfStringHasInvalidCharacter(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<InvalidCharacterException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("17")]
        public void TryParse_ShouldThrowInvalidCharacterException_IfStringHasSingleQuotes(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<InvalidCharacterException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("18")]
        [TestCase("19")]
        public void TryParse_ShouldThrowSyntaxException_IfJsonEndsWithNonwhiteCharacter(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<SyntaxException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("20")]
        public void TryParse_ShouldThrowSyntaxException_IfThereIsNoKeyAfterComma(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<SyntaxException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("21")]
        public void TryParse_ShouldThrowSyntaxException_IfThereIsNoColonAfterKeyInObject(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<SyntaxException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("22")]
        public void TryParse_ShouldThrowSyntaxException_IfThereIsComaInsteadOfColonAfterKeyInObject(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<SyntaxException>(() => Parser.TryParse(lexer.Tokenize()));
        }

        [TestCase("23")]
        public void TryParse_ShouldThrowSyntaxException_IfThereColonInsteadOfComaInArray(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            //Act & Assert
            Assert.Throws<SyntaxException>(() => Parser.TryParse(lexer.Tokenize()));
        }
    }
}
