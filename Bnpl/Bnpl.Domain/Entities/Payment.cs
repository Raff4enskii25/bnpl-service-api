using System;


namespace Bnpl.Domain.Entities
{
    public class Payment
    {
        public int Id { get; private set; }
        public int InstallmentId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTimeOffset PaidAt { get; private set; }
        public Installment Installment { get; private set; }

        public Payment(decimal amount, DateTimeOffset paidAt)
        {
            if(amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), $"Amount must be greater than 0.");

            Amount = amount;
            PaidAt = paidAt;
        }
        
        private Payment() { }
    }
}