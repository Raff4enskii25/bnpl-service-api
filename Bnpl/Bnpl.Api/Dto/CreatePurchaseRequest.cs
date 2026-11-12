using Bnpl.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using Bnpl.Domain.Entities;
using Bnpl.Api.Validation;

namespace Bnpl.Api.Dto
{
    public record CreatePurchaseRequest
    {
        [Required]
        public int? CustomerId { get; init; }

        [Required]
        [AmountValidFormat]
        [MinAmount((double)Purchase.MinimumAmount)]
        [MaxAmount((double)Purchase.MaximumAmount)]
        public decimal? Amount { get; init; }

        [MaxLength(Purchase.DescriptionMaxLength)]
        public string? Description { get; init; }
    }
}
