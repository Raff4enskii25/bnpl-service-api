using Bnpl.Domain.Entities;
using Bnpl.Domain.Enums;
using Xunit;

namespace Bnpl.UnitTests.Entities
{
    public class InstallmentPlanTests
    {
        private static readonly DateTimeOffset _testDate = new DateTimeOffset(2025, 10, 25, 0, 0, 0, TimeSpan.Zero);

        [Theory]
        [InlineData(2)]
        [InlineData(12)]
        public void InstallmentPlan_ValidData_CreateSuccessfully(int numberOfInstallments)
        {
            // Arrange
            Purchase purchase = GetPurchase();
            DateTimeOffset createdAt = _testDate;

            // Act
            var installmentPlan = new InstallmentPlan(purchase, numberOfInstallments, createdAt);

            // Assert
            Assert.Equal(purchase.Amount, installmentPlan.TotalAmount);
            Assert.Equal(numberOfInstallments, installmentPlan.NumberOfInstallments);
            Assert.Equal(InstallmentPlanStatus.Active, installmentPlan.Status);
            Assert.Equal(createdAt, installmentPlan.CreatedAt);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(13)]
        public void InstallmentPlan_IncorrectNumberOfInstallments_ThrowsArgumentOutOfRange(int numberOfInstallments)
        {
            // Arrange
            Purchase purchase = GetPurchase();
            DateTimeOffset createdAt = _testDate;

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new InstallmentPlan(purchase, numberOfInstallments, createdAt));
        }

        [Fact]
        public void InstallmentPlan_NullPurchase_ThrowsArgumentNullException()
        {
            // Arrange
            Purchase purchase = null!;
            int numberOfInstallments = InstallmentPlan.MinNumberOfInstallments;
            DateTimeOffset createdAt = _testDate;
            
            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new InstallmentPlan(purchase, numberOfInstallments, createdAt));
        }

        [Theory]
        [InlineData(InstallmentPlanStatus.Completed)]
        [InlineData(InstallmentPlanStatus.Overdue)]
        public void InstallmentPlan_ChangeStatusWhenActive_UpdateStatus(InstallmentPlanStatus status)
        {
            // Arrange
            Purchase purchase = GetPurchase();
            int numberOfInstallments = InstallmentPlan.MinNumberOfInstallments;
            DateTimeOffset createdAt = _testDate;
            var installmentPlan = new InstallmentPlan(purchase, numberOfInstallments, createdAt);

            // Act
            installmentPlan.ChangeStatus(status);

            // Assert
            Assert.Equal(status, installmentPlan.Status);
        }

        [Theory]
        [InlineData(InstallmentPlanStatus.Completed)]
        [InlineData(InstallmentPlanStatus.Active)]
        public void InstallmentPlan_ChangeStatusWhenOverdue_UpdateStatus(InstallmentPlanStatus status)
        {
            // Arrange
            Purchase purchase = GetPurchase();
            int numberOfInstallments = InstallmentPlan.MinNumberOfInstallments;
            DateTimeOffset createdAt = _testDate;
            var installmentPlan = new InstallmentPlan(purchase, numberOfInstallments, createdAt);
            installmentPlan.ChangeStatus(InstallmentPlanStatus.Overdue);

            // Act
            installmentPlan.ChangeStatus(status);

            // Assert
            Assert.Equal(status, installmentPlan.Status);
        }

        [Theory]
        [InlineData(InstallmentPlanStatus.Active)]
        [InlineData(InstallmentPlanStatus.Overdue)]
        public void InstallmentPlan_ChangeStatusWhenCompleted_DoNotUpdateStatus(InstallmentPlanStatus status)
        {
            // Arrange
            Purchase purchase = GetPurchase();
            int numberOfInstallments = InstallmentPlan.MinNumberOfInstallments;
            DateTimeOffset createdAt = _testDate;
            var installmentPlan = new InstallmentPlan(purchase, numberOfInstallments, createdAt);
            installmentPlan.ChangeStatus(InstallmentPlanStatus.Completed);

            // Act
            installmentPlan.ChangeStatus(status);

            // Assert
            Assert.Equal(InstallmentPlanStatus.Completed, installmentPlan.Status);
        }


        private Purchase GetPurchase()
        {
            return new Purchase(1, 100, "Test Purchase", _testDate);
        }
    }
}
