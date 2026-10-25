using Xunit;
using Bnpl.Domain.Entities;

namespace Bnpl.UnitTests.Entities
{
    public class CustomerTests
    {
        private static readonly DateTimeOffset _testDate = new DateTimeOffset(2025, 10, 25, 0, 0, 0, TimeSpan.Zero);

        [Fact]
        public void Customer_ValidData_CreateSuccessfully()
        {
            // Arrange
            string name = "Test name";
            string email = "test@test.com";
            DateTimeOffset createdAt = _testDate;

            // Act
            var customer = new Customer(name, email, createdAt);

            // Assert
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(createdAt, customer.CreatedAt);
        }

        [Fact]
        public void Customer_NameNull_ThrowsArgumentNull()
        {
            // Arrange
            string name = null!;
            string email = "test@test.com";
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new Customer(name, email, createdAt));
        }

        [Fact]
        public void Customer_EmailNull_ThrowsArgumentNull()
        {
            // Arrange
            string name = "Test name";
            string email = null!;
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new Customer(name, email, createdAt));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Customer_NameWhiteSpace_ThrowsArgument(string name)
        {
            // Arrange
            string email = "test@test.com";
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentException>(() => new Customer(name, email, createdAt));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Customer_EmailWhiteSpace_ThrowsArgument(string email)
        {
            // Arrange
            string name = "Test name";
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentException>(() => new Customer(name, email, createdAt));
        }

        [Fact]
        public void Customer_NameTooLong_ThrowsArgumentOutOfRange()
        {
            // Arrange
            string name = new string('a', Customer.MaxNameLength + 1);
            string email = "test@test.com";
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Customer(name, email, createdAt));
        }


        [Fact]
        public void Customer_EmailTooLong_ThrowsArgumentOutOfRange()
        {
            // Arrange
            string name = "Test name";
            string email = new string('a', Customer.MaxEmailLength + 1);
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Customer(name, email, createdAt));
        }

        [Theory]
        [InlineData("    Test")]
        [InlineData("    Test    ")]
        public void Customer_NameWithSpaces_IsTrimmed(string name)
        {
            // Arrange
            string example = "Test";
            string email = "test@test.com";
            DateTimeOffset createdAt = _testDate;

            // Act
            var customer = new Customer(name, email, createdAt);

            // Assert
            Assert.Equal(example, customer.Name);
        }

        [Theory]
        [InlineData("    TEST@Test.com")]
        [InlineData("    TEST@test.com    ")]
        [InlineData("    TE ST@te  st.com    ")]
        public void Customer_EmailWithSpaces_IsTrimmed(string email)
        {
            // Arrange
            string example = "test@test.com";
            string name = "Test name";
            DateTimeOffset createdAt = _testDate;

            // Act
            var customer = new Customer(name, email, createdAt);

            // Assert
            Assert.Equal(example, customer.Email);
        }
    }
}
