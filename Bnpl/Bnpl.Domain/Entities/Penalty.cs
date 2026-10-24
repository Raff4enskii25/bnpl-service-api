using System;
using Bnpl.Domain.Enums;

namespace Bnpl.Domain.Entities
{
    public class Penalty
    {
        public int Id { get; private set; }
        public int InstallmentId { get; private set; }
        public decimal Amount { get; private set; }
        public PenaltyReason Reason { get; private set; }
        public DateTimeOffset AccrualDate { get; private set; }
        public Installment Installment { get; private set; }

        public Penalty(decimal amount, PenaltyReason reason, DateTimeOffset accrualDate)
        {
            if(amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0.");

            Amount = amount;
            Reason = reason;
            AccrualDate = accrualDate;
        }
        
        private Penalty() { }
    }
}
