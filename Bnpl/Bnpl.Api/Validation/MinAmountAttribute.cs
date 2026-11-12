using System;
using System.ComponentModel.DataAnnotations;

namespace Bnpl.Api.Validation
{
    public class MinAmountAttribute : ValidationAttribute
    {
        public decimal MinAmount { get; }
        public MinAmountAttribute(double minAmount)
        {
            MinAmount = Convert.ToDecimal(minAmount);
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
                return true;

            if (value is decimal amount)
                return amount >= MinAmount;
            
            return false;
        }
    }
}
