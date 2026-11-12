using System.ComponentModel.DataAnnotations;
using Bnpl.Domain.Validation;

namespace Bnpl.Api.Validation
{
    public class AmountValidFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null)
                return true;

            if (value is decimal amount)
            {
                return AmountValidation.HasAtMostTwoDecimalPlaces(amount);
            }

            return false;
        }
    }
}
