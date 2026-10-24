using System;
using Bnpl.Domain.Enums;

namespace Bnpl.Domain.Entities
{
    public class InstallmentPlan
    {
        public int Id { get; private set; }
        public int PurchaseId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public int NumberOfInstallments { get; private set; }
        public InstallmentPlanStatus Status { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public Purchase Purchase { get; private set; }
        public ICollection<Installment> Installments { get; private set; } = new List<Installment>();

        public InstallmentPlan(int purchaseId, decimal totalAmount, int numberOfInstallments, DateTimeOffset createdAt)
        {
            if (totalAmount <= 0) throw new ArgumentOutOfRangeException(nameof(totalAmount), "Total amount must be greater than zero.");
            if (numberOfInstallments <= 1) throw new ArgumentOutOfRangeException(nameof(numberOfInstallments), "Number of installments must be greater than 1.");
            if (numberOfInstallments > 12) throw new ArgumentOutOfRangeException(nameof(numberOfInstallments), "Number of installments must be less than or equal to 12.");

            PurchaseId = purchaseId;
            TotalAmount = totalAmount;
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
