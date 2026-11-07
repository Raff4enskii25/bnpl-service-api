using System;
using Xunit;
using Bnpl.Domain.Validation;

namespace Bnpl.UnitTests.Validation
{
    public class AmountValidationTests
    {
        [Fact]
        public void HasAtMostTwoDecimalPlaces_ValidData_True()
        {
            // Arrange
            decimal amount = 100.05m;

            // Act && Assert
            Assert.True(AmountValidation.HasAtMostTwoDecimalPlaces(amount));
        }

        [Fact]
        public void HasAtMostTwoDecimalPlaces_InvalidData_False()
        {
            // Arrange
            decimal amount = 100.006m;

            // Act && Assert
            Assert.False(AmountValidation.HasAtMostTwoDecimalPlaces(amount));
        }
    }
}
