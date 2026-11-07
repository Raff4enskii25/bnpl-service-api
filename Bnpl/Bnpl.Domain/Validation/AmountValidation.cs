using System;

namespace Bnpl.Domain.Validation
{
    public static class AmountValidation
    {
        public static bool HasAtMostTwoDecimalPlaces(decimal amount)
        {
            return amount == Math.Round(amount, 2);
        }
    }
}
