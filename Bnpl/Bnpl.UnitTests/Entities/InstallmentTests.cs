using Xunit;
using Bnpl.Domain.Entities;
using Bnpl.Domain.Enums;

namespace Bnpl.UnitTests.Entities
{
    public class InstallmentTests
    {
        private static readonly DateOnly _testDate = new DateOnly(2026, 11, 05);

        [Fact]
        public void Installment_ValidData_CreateSuccessfully()
        {
            // Arrange
            int installmentPlanId = 1;
            int installmentNumber = 1;
            decimal amount = 100m;
            
            // Act
            var installment = new Installment(installmentPlanId, installmentNumber, amount, _testDate);
            
            // Assert
            Assert.Equal(installmentPlanId, installment.InstallmentPlanId);
            Assert.Equal(installmentNumber, installment.InstallmentNumber);
            Assert.Equal(amount, installment.Amount);
            Assert.Equal(_testDate, installment.DueDate);
        }


        [Theory]
        [InlineData(-100)]
        [InlineData(-1)]
        [InlineData(0)]
        public void Installment_IncorrectAmount_ThrowsArgumentOutOfRangeException(decimal amount)
        {
            // Arrange
            int installmentPlanId = 1;
            int installmentNumber = 1;
            
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Installment(installmentPlanId, installmentNumber, amount, _testDate));
        }


        [Fact]
        public void Installment_MarkAsOverdueWhenStatusIsPaid_ThrowsInvalidOperationException()
        {
            // Arrange
            int installmentPlanId = 1;
            int installmentNumber = 1;
            decimal amount = 100m;
            var installment = new Installment(installmentPlanId, installmentNumber, amount, _testDate);

            // Act
            installment.MarkAsPaid();

            // Assert
            Assert.Throws<InvalidOperationException>(() => installment.MarkAsOverdue());
        }


        [Fact]
        public void Installment_MarkAsOverdueWhenStatusIsPending_UpdatesStatusToOverdue()
        {
            // Arrange
            int installmentPlanId = 1;
            int installmentNumber = 1;
            decimal amount = 100m;
            var installment = new Installment(installmentPlanId, installmentNumber, amount, _testDate);
            
            // Act
            installment.MarkAsOverdue();
            
            // Assert
            Assert.Equal(InstallmentStatus.Overdue, installment.Status);
        }


        [Fact]
        public void Installment_MarkAsPaidWhenStatusIsOverdue_UpdatesStatusToPaid()
        {
            // Arrange
            int installmentPlanId = 1;
            int installmentNumber = 1;
            decimal amount = 100m;
            var installment = new Installment(installmentPlanId, installmentNumber, amount, _testDate);
            installment.MarkAsOverdue();

            // Act
            installment.MarkAsPaid();

            // Assert
            Assert.Equal(InstallmentStatus.Paid, installment.Status);
        }


        [Fact]
        public void Installment_CheckStatusAfterCreation_ShouldBePending()
        {
            // Arrange
            int installmentPlanId = 1;
            int installmentNumber = 1;
            decimal amount = 100m;
            // Act
            var installment = new Installment(installmentPlanId, installmentNumber, amount, _testDate);
            // Assert
            Assert.Equal(InstallmentStatus.Pending, installment.Status);
        }
    }
}
