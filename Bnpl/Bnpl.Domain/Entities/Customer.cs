using System;

namespace Bnpl.Domain.Entities
{
    public class Customer
    {
        public const int MaxNameLength = 100;
        public const int MaxEmailLength = 254;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public ICollection<Purchase> Purchases { get; private set; } = new List<Purchase>();

        public Customer(string name, string email, DateTimeOffset createdAt)
        {
            if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
            if(name.Length > MaxNameLength) throw new ArgumentOutOfRangeException(nameof(name), $"Name must be less than or equal to {MaxNameLength} characters.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException(nameof(email));
            if(email.Length > MaxEmailLength) throw new ArgumentOutOfRangeException(nameof(email), $"Email must be less than or equal to {MaxEmailLength} characters.");


            Name = name;
            Email = email.Replace(" ", "").ToLowerInvariant();
            CreatedAt = createdAt;
        }

        private Customer() { }
    }
}
