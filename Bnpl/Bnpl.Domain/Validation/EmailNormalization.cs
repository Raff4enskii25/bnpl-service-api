using System;
using System.Linq;

namespace Bnpl.Domain.Validation
{
    public static class EmailNormalization
    {
        public const int MaxLength = 254;

        public static string Normalize(string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email), "Customer email cannot be null.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("The customer email cannot be empty or whitespace", nameof(email));

            email = email.Trim();

            if (email.Any(char.IsWhiteSpace))
                throw new ArgumentException("The customer email cannot contain spaces inside.", nameof(email));

            email = email.ToLowerInvariant();

            if (email.Length > MaxLength)
                throw new ArgumentOutOfRangeException(nameof(email), $"Email must be less than or equal to {MaxLength} characters.");

            return email;
        }
    }
}
