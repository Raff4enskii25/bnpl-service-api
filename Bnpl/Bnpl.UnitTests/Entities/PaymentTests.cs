using System;
using Xunit;
using Bnpl.Domain.Entities;

namespace Bnpl.UnitTests.Entities
{
    public class PaymentTests
    {
        private static readonly DateTimeOffset _testDate = new DateTimeOffset(2026, 11, 05, 0, 0, 0, TimeSpan.Zero);

        [Fact]
        public void Payment_ValidData_CreateSuccessfully()
        {
            // Arrange
            int installmentId = 1;
            decimal amount = 100m;
            DateTimeOffset paidAt = _testDate;
            
            // Act
            var payment = new Payment(installmentId, amount, paidAt);
            
            // Assert
            Assert.Equal(amount, payment.Amount);
            Assert.Equal(paidAt, payment.PaidAt);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-1)]
        [InlineData(0)]
        public void Payment_InvalidAmount_ThrowsArgumentOutOfRangeException(decimal amount)
        {
            // Arrange
            int installmentId = 1;
            DateTimeOffset paidAt = _testDate;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Payment(installmentId, amount, paidAt));
        }
    }
}
