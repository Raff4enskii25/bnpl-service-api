using System;
using System.ComponentModel.DataAnnotations;

namespace Bnpl.Api.Validation
{
    public class MaxAmountAttribute : ValidationAttribute
    {
        public decimal MaxAmount { get; }
        
        public MaxAmountAttribute(double maxAmount)
        {
            MaxAmount = Convert.ToDecimal(maxAmount);
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
                return true;

            if (value is decimal amount)
                return amount <= MaxAmount;

            return false;
        }
    }
}
