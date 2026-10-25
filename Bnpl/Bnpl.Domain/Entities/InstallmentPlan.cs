using System;
using Bnpl.Domain.Enums;

namespace Bnpl.Domain.Entities
{
    public class InstallmentPlan
    {
        public const int MaxNumberOfInstallments = 12;
        public const int MinNumberOfInstallments = 2;

        public int Id { get; private set; }
        public int PurchaseId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public int NumberOfInstallments { get; private set; }
        public InstallmentPlanStatus Status { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public Purchase Purchase { get; private set; }
        public ICollection<Installment> Installments { get; private set; } = new List<Installment>();

        public InstallmentPlan(Purchase purchase, int numberOfInstallments, DateTimeOffset createdAt)
        {
            if(purchase == null) throw new ArgumentNullException(nameof(purchase), "Purchase cannot be null.");
            if (numberOfInstallments < MinNumberOfInstallments) throw new ArgumentOutOfRangeException(nameof(numberOfInstallments), $"Number of installments must be greater than or equal to {MinNumberOfInstallments}.");
            if (numberOfInstallments > MaxNumberOfInstallments) throw new ArgumentOutOfRangeException(nameof(numberOfInstallments), $"Number of installments must be less than or equal to {MaxNumberOfInstallments}.");

            PurchaseId = purchase.Id;
            TotalAmount = purchase.Amount;
            NumberOfInstallments = numberOfInstallments;
            Status = InstallmentPlanStatus.Active;
            CreatedAt = createdAt;
        }

        private InstallmentPlan() { }

        public void ChangeStatus(InstallmentPlanStatus status)
        {
            if(Status != InstallmentPlanStatus.Completed)
                Status = status;
        }
    }
}
