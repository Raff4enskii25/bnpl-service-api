using System;
using Bnpl.Domain.Entities;
using Bnpl.Domain.Validation;

namespace Bnpl.Domain.Logic
{
    public static class InstallmentsCalculator
    {
        public static IReadOnlyList<InstallmentScheduleItem> Calculate(decimal totalAmount, int numberOfInstallments, DateOnly startDate)
        {
            if(totalAmount > Purchase.MaximumAmount) throw new ArgumentOutOfRangeException(nameof(totalAmount), $"Total amount must be lower than or equal to {Purchase.MaximumAmount}.");
            if(totalAmount < Purchase.MinimumAmount) throw new ArgumentOutOfRangeException(nameof(totalAmount), $"Total amount must be greater than or equal to {Purchase.MinimumAmount}.");
            if(!AmountValidation.HasAtMostTwoDecimalPlaces(totalAmount)) throw new ArgumentOutOfRangeException(nameof(totalAmount), "Amount must not have more than two decimal places.");
            if(numberOfInstallments < InstallmentPlan.MinNumberOfInstallments) throw new ArgumentOutOfRangeException(nameof(numberOfInstallments), $"Number of installments must be greater than or equal to {InstallmentPlan.MinNumberOfInstallments}.");
            if(numberOfInstallments > InstallmentPlan.MaxNumberOfInstallments) throw new ArgumentOutOfRangeException(nameof(numberOfInstallments), $"Number of installments must be less than or equal to {InstallmentPlan.MaxNumberOfInstallments}.");

            decimal regularAmount = Math.Floor((totalAmount / numberOfInstallments) * 100) / 100;
            decimal lastAmount = totalAmount - (regularAmount * (numberOfInstallments - 1));

            var installments = new List<InstallmentScheduleItem>();

            for (int i = 1; i < numberOfInstallments; i++)
            {
                var installment = new InstallmentScheduleItem(regularAmount, i, startDate.AddMonths(i));
                installments.Add(installment);
            }
            var lastInstallment = new InstallmentScheduleItem(lastAmount, numberOfInstallments, startDate.AddMonths(numberOfInstallments));
            installments.Add(lastInstallment);

            return installments;
        }
    }
}
