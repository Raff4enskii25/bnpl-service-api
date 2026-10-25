using System;

namespace Bnpl.Domain.Entities
{
    public class Purchase
    {
        public const decimal MinimumAmount = 100;
        public const int DescriptionMaxLength = 500;

        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public Customer Customer { get; private set; }
        public InstallmentPlan InstallmentPlan { get; private set; }

        public Purchase(int customerId, decimal amount, string? description, DateTimeOffset createdAt)
        {
            if (amount < MinimumAmount) throw new ArgumentOutOfRangeException(nameof(amount), $"Amount must be greater than or equal to {MinimumAmount}.");
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (description.IsWhiteSpace()) throw new ArgumentException(nameof(description));
            if (description.Length > DescriptionMaxLength) throw new ArgumentOutOfRangeException(nameof(description), $"Description length must not exceed {DescriptionMaxLength} characters."); 

            CustomerId = customerId;
            Amount = amount;
            Description = description;
            CreatedAt = createdAt;
        }

        private Purchase() { }
    }
}
