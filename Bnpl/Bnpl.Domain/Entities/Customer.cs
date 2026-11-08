using System;
using Bnpl.Domain.Validation;

namespace Bnpl.Domain.Entities
{
    public class Customer
    {
        public const int MaxNameLength = 100;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public ICollection<Purchase> Purchases { get; private set; } = new List<Purchase>();

        public Customer(string name, string email, DateTimeOffset createdAt)
        {
            if(name == null) throw new ArgumentNullException(nameof(name), "Customer name cannot be null.");
            
            name = name.Trim();
            
            if (name.IsWhiteSpace()) throw new ArgumentException("The customer name must not consist of spaces.", nameof(name));
            if(name.Length > MaxNameLength) throw new ArgumentOutOfRangeException(nameof(name), $"Name must be less than or equal to {MaxNameLength} characters.");

            Name = name;
            Email = EmailNormalization.Normalize(email);
            CreatedAt = createdAt;
        }

        private Customer() { }
    }
}
