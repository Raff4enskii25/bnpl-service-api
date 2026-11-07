using Bnpl.Domain.Entities;
using Bnpl.Domain.Logic;
using System;
using Xunit;

namespace Bnpl.UnitTests.Logic
{
    public class InstallmentsCalculatorTests
    {
        private static readonly DateOnly _testDate = new DateOnly(2025, 12, 31);

        [Theory]
        [InlineData(1000.00, 5)]
        [InlineData(200, 3)]
        [InlineData(1000.01, 3)]
        [InlineData(1006.99, 3)]
        [InlineData(100.01, 12)]
        [InlineData(100.01, 2)]
        public void Calculate_ValidData_CheckSumCalculatesCorrectly(decimal totalAmount, int numberOfInstallments)
        {
            // Act
            var installments = InstallmentsCalculator.Calculate(totalAmount, numberOfInstallments, _testDate);

            // Assert
            Assert.Equal(totalAmount, installments.Sum(i => i.Amount));
        }


        [Fact]
        public void Calculate_ValidData_CheckPaymentsCalculatesCorrectly()
        {
            decimal amount = 200.00m;
            int numberOfInstallments = 3;
            decimal[] expectedPayments = { 66.66m, 66.66m, 66.68m };

            // Act
            var installments = InstallmentsCalculator.Calculate(amount, numberOfInstallments, _testDate);

            // Assert
            for (int i = 0; i < numberOfInstallments; i++)
            {
                Assert.Equal(expectedPayments[i], installments[i].Amount);
            }
        }


        [Theory]
        [InlineData(-100.00)]
        [InlineData(0)]
        [InlineData(99.99)]
        [InlineData(1_000_001.01)]
        public void Calculate_InvalidTotalAmount_ThrowsArgumentOutOfRangeException(decimal totalAmount)
        {
            // Arrange
            int numberOfInstallments = 3;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => InstallmentsCalculator.Calculate(totalAmount, numberOfInstallments, _testDate));
        }

        [Theory]
        [InlineData(100.025)]
        [InlineData(100.029)]
        public void Calculate_AmountWithMoreThanTwoDecimalPlaces_ThrowsArgumentOutOfRange(decimal totalAmount)
        {
            // Arrange
            int numberOfInstallments = 3;

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => InstallmentsCalculator.Calculate(totalAmount, numberOfInstallments, _testDate));
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(13)]
        public void Calculate_InvalidNumberOfInstallments_ThrowsArgumentOutOfRangeException(int numberOfInstallments)
        {
            // Arrange
            decimal totalAmount = 1000m;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => InstallmentsCalculator.Calculate(totalAmount, numberOfInstallments, _testDate));
        }


        [Fact]
        public void Calculate_ValidData_CheckDatesCalculatesCorrectly()
        {
            decimal amount = 1000.00m;
            int numberOfInstallments = 4;
            DateOnly[] dates = {
                new DateOnly(2026, 01, 31),
                new DateOnly(2026, 02, 28),
                new DateOnly(2026, 03, 31),
                new DateOnly(2026, 04, 30),
            };

            // Act
            var installments = InstallmentsCalculator.Calculate(amount, numberOfInstallments, _testDate);

            // Assert
            for (int i = 0; i < numberOfInstallments; i++)
            {
                Assert.Equal(dates[i], installments[i].Date);
            }
        }

        [Fact]
        public void Calculate_BissextileYear_CheckDatesCalculatesCorrectly()
        {
            DateOnly date = new DateOnly(2027, 12, 31);
            decimal amount = 1000.00m;
            int numberOfInstallments = 4;
            DateOnly[] dates = {
                new DateOnly(2028, 01, 31),
                new DateOnly(2028, 02, 29),
                new DateOnly(2028, 03, 31),
                new DateOnly(2028, 04, 30),
            };

            // Act
            var installments = InstallmentsCalculator.Calculate(amount, numberOfInstallments, date);

            // Assert
            for (int i = 0; i < numberOfInstallments; i++)
            {
                Assert.Equal(dates[i], installments[i].Date);
            }
        }
    }
}
