using System.ComponentModel.DataAnnotations;
using Bnpl.Domain.Entities;
using Bnpl.Domain.Validation;

namespace Bnpl.Api.Dto
{
    public record CreateCustomerRequest
    {
        [Required]
        [MaxLength(Customer.MaxNameLength)]
        public string? Name { get; init; }
        
        [Required]
        [MaxLength(EmailNormalization.MaxLength)]
        [EmailAddress]
        public string? Email { get; init; }
    }
}
