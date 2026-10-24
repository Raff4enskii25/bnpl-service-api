using System;
using Bnpl.Domain.Enums;

namespace Bnpl.Domain.Entities
{
    public class Installment
    {
        public int Id { get; private set; }
        public int InstallmentPlanId { get; private set; }
        public int InstallmentNumber { get; private set; }
        public decimal Amount { get; private set; }
        public InstallmentStatus Status { get; private set; }
        public DateOnly DueDate { get; private set; }
        public InstallmentPlan InstallmentPlan { get; private set; }
        public Payment Payment { get; private set; }
        public Penalty Penalty { get; private set; }

        public Installment(int installmentPlanId, int installmentNumber, decimal amount, DateOnly dueDate)
        {
            if(amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 100.");

            InstallmentPlanId = installmentPlanId;
            InstallmentNumber = installmentNumber;
            Amount = amount;
            Status = InstallmentStatus.Pending;
            DueDate = dueDate;
        }

        private Installment() { }

        public void MarkAsPaid()
        {
            if(Status != InstallmentStatus.Paid)
                Status = InstallmentStatus.Paid;
        }

        public void MarkAsOverdue()
        {
            if(Status == InstallmentStatus.Pending)
                Status = InstallmentStatus.Overdue;
            
            if(Status == InstallmentStatus.Overdue) { }
        }
    }
}
