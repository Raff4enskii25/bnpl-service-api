using System;
using Xunit;
using Bnpl.Domain.Entities;
using Bnpl.Domain.Enums;

namespace Bnpl.UnitTests.Entities
{
    public class PenaltyTests
    {
        private static readonly DateTimeOffset _testDate = new DateTimeOffset(2026, 11, 05, 0, 0, 0, TimeSpan.Zero);

        [Fact]
        public void Penalty_ValidData_CreateSuccessfully()
        {
            // Arrange
            int installmentId = 1;
            decimal amount = 50m;
            var reason = PenaltyReason.Overdue;

            // Act
            var penalty = new Penalty(installmentId, amount, reason, _testDate);

            // Assert
            Assert.Equal(amount, penalty.Amount);
            Assert.Equal(reason, penalty.Reason);
            Assert.Equal(_testDate, penalty.AccrualDate);
        }

        [Theory]
        [InlineData(-50)]
        [InlineData(-1)]
        [InlineData(0)]
        public void Penalty_InvalidAmount_ThrowsArgumentOutOfRangeException(decimal amount)
        {
            // Arrange
            int installmentId = 1;
            var reason = PenaltyReason.Overdue;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Penalty(installmentId, amount, reason, _testDate));
        }
    }
}
