using Xunit;
using Bnpl.Domain.Entities;

namespace Bnpl.UnitTests.Entities
{
    public class PurchaseTests
    {
        private static readonly DateTimeOffset _testDate = new DateTimeOffset(2025, 10, 25, 0, 0, 0, TimeSpan.Zero);

        [Fact]
        public void Purchase_ValidData_CreateSuccessfully()
        {
            // Arrange
            int customerId = 1;
            decimal amount = Purchase.MinimumAmount;
            string description = "Test purchase";
            DateTimeOffset createdAt = _testDate;

            // Act
            var purchase = new Purchase(customerId, amount, description, createdAt);

            // Assert
            Assert.Equal(customerId, purchase.CustomerId);
            Assert.Equal(amount, purchase.Amount);
            Assert.Equal(description, purchase.Description);
            Assert.Equal(createdAt, purchase.CreatedAt);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Purchase_IncorrectData_ThrowsArgumentOutOfRange(decimal amount)
        {
            // Arrange
            int customerId = 1;
            string description = "Test purchase";
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Purchase(customerId, amount, description, createdAt));
        }

        [Fact]
        public void Purchase_AmountJustBelowMinimum_ThrowsArgumentOutOfRange()
        {
            // Arrange
            int customerId = 1;
            decimal amount = Purchase.MinimumAmount - 0.01m;
            string description = "Test purchase";
            DateTimeOffset createdAt = _testDate;
            
            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Purchase(customerId, amount, description, createdAt));
        }

        [Fact]
        public void Purchase_ValidDescription_CreateSuccessfully()
        {
            // Arrange
            int customerId = 1;
            decimal amount = Purchase.MinimumAmount;
            string description = new string('a', Purchase.DescriptionMaxLength);
            DateTimeOffset createdAt = _testDate;

            // Act
            var purchase = new Purchase(customerId, amount, description, createdAt);

            // Assert
            Assert.Equal(description, purchase.Description);
        }

        [Fact]
        public void Purchase_DescriptionNull_ThrowsArgumentNull()
        {
            // Arrange
            int customerId = 1;
            decimal amount = Purchase.MinimumAmount;
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new Purchase(customerId, amount, null, createdAt));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Purchase_DescriptionWhitespace_ThrowsArgument(string? description)
        {
            // Arrange
            int customerId = 1;
            decimal amount = Purchase.MinimumAmount;
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentException>(() => new Purchase(customerId, amount, description, createdAt));
        }

        [Fact]
        public void Purchase_DescriptionTooLong_ThrowsArgumentOutOfRange()
        {
            // Arrange
            int customerId = 1;
            decimal amount = Purchase.MinimumAmount;
            string description = new string('a', Purchase.DescriptionMaxLength + 1);
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Purchase(customerId, amount, description, createdAt));
        }
    }
}
