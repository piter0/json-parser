using json_parser;
using json_parser.Exceptions;
using json_parser.Models;

namespace Tests
{
    public class LexerTests
    {
        [TestCase("1")]
        public void Tokenize_ShouldReturnListOfTokens_IfInputIsVaild(string fileNumber)
        {
            // Arrange
            var input = Input.GetValidInput(fileNumber);
            var lexer = new Lexer(input);

            // Act
            var tokens = lexer.Tokenize();

            //Assert
            Assert.That(tokens, Has.Count.EqualTo(21));
            Assert.Multiple(() =>
            {
                Assert.That(tokens[0].Type, Is.EqualTo(TokenType.OpenCurlyBracket));
                Assert.That(tokens[20].Type, Is.EqualTo(TokenType.CloseCurlyBracket));
                Assert.That(tokens[1].Type, Is.EqualTo(TokenType.String));
                Assert.That(tokens[2].Type, Is.EqualTo(TokenType.Colon));
                Assert.That(tokens[3].Type, Is.EqualTo(TokenType.String));
                Assert.That(tokens[4].Type, Is.EqualTo(TokenType.Comma));
                Assert.That(tokens[7].Type, Is.EqualTo(TokenType.Number));
                Assert.That(tokens[11].Type, Is.EqualTo(TokenType.Null));
                Assert.That(tokens[15].Type, Is.EqualTo(TokenType.True));
                Assert.That(tokens[19].Type, Is.EqualTo(TokenType.False));
            });
            Assert.Multiple(() =>
            {
                Assert.That(tokens[3].Value, Is.EqualTo("ABC"));
                Assert.That(tokens[7].Value, Is.EqualTo("-2.1"));
                Assert.That(tokens[11].Value, Is.EqualTo("null"));
                Assert.That(tokens[15].Value, Is.EqualTo("true"));
                Assert.That(tokens[19].Value, Is.EqualTo("false"));
            });
        }

        [TestCase("2")]
        public void Tokenize_ShouldReturnListOfTokens_IfInputHasWhitespaces(string fileNumber)
        {
            // Arrange
            var input = Input.GetValidInput(fileNumber);
            var lexer = new Lexer(input);

            // Act
            var tokens = lexer.Tokenize();

            //Assert
            Assert.That(tokens, Has.Count.EqualTo(9));
            Assert.Multiple(() =>
            {
                Assert.That(tokens[0].Type, Is.EqualTo(TokenType.OpenCurlyBracket));
                Assert.That(tokens[1].Type, Is.EqualTo(TokenType.String));
                Assert.That(tokens[2].Type, Is.EqualTo(TokenType.Colon));
                Assert.That(tokens[3].Type, Is.EqualTo(TokenType.String));
                Assert.That(tokens[4].Type, Is.EqualTo(TokenType.Comma));
                Assert.That(tokens[5].Type, Is.EqualTo(TokenType.String));
                Assert.That(tokens[6].Type, Is.EqualTo(TokenType.Colon));
                Assert.That(tokens[7].Type, Is.EqualTo(TokenType.String));
                Assert.That(tokens[8].Type, Is.EqualTo(TokenType.CloseCurlyBracket));
            });
        }

        [TestCase("1")]
        public void Tokenize_ShouldThrowInvalidCharacterException_IfJsonHasInvalidCharacter(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            // Act and Assert
            Assert.Throws<InvalidCharacterException>(() => lexer.Tokenize());
        }

        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        public void Tokenize_ShouldThrowInvalidCharacterException_IfStringHasInvalidCharacter(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            // Act and Assert
            Assert.Throws<InvalidCharacterInStringException>(() => lexer.Tokenize());
        }

        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        [TestCase("10")]
        public void Tokenize_ShouldThrowInvalidNumberException_IfNumberIsNotValid(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            // Act and Assert
            Assert.Throws<InvalidNumberException>(() => lexer.Tokenize());
        }

        [TestCase("11")]
        [TestCase("12")]
        [TestCase("13")]
        public void Tokenize_ShouldThrowUnexpectedTokenException_IfTokenIsUnknown(string fileNumber)
        {
            // Arrange
            var input = Input.GetInvalidInput(fileNumber);
            var lexer = new Lexer(input);

            // Act and Assert
            Assert.Throws<UnexpectedTokenException>(() => lexer.Tokenize());
        }
    }
}