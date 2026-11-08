using System;
using Xunit;
using Bnpl.Domain.Validation;

namespace Bnpl.UnitTests.Validation
{
    public class EmailNormalizationTests
    {
        [Theory]
        [InlineData(" email@mail.ru")]
        [InlineData("\temail@mail.ru")]
        [InlineData(" eMAil@mail.ru   ")]
        public void Normalize_ValidData_NormalizeCorrectly(string email)
        {
            // Arrange
            string expectedEmail = "email@mail.ru";

            // Act && Assert
            Assert.Equal(expectedEmail, EmailNormalization.Normalize(email));
        }


        [Fact]
        public void Normalize_NullString_ThrowsArgumentNullException()
        {
            // Arrange
            string email = null!;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => EmailNormalization.Normalize(email));
        }


        [Theory]
        [InlineData("")]
        [InlineData($"\n")]
        [InlineData($"\t")]
        [InlineData("     ")]
        public void Normalize_SpacesStrings_ThrowsArgumentException(string email)
        {
            // Act && Assert
            Assert.Throws<ArgumentException>(() => EmailNormalization.Normalize(email));
        }


        [Theory]
        [InlineData("em ail@mail.ru")]
        [InlineData("em\tail@mail.ru")]
        [InlineData("em\nail@mail.ru")]
        public void Normalize_SpacesWithinAString_ThrowsArgumentException(string email)
        {
            // Act && Assert
            Assert.Throws<ArgumentException>(() => EmailNormalization.Normalize(email));
        }


        [Fact]
        public void Normalize_EmailToLong_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            string email = new string('a', EmailNormalization.MaxLength + 1);

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => EmailNormalization.Normalize(email));
        }
    }
}
