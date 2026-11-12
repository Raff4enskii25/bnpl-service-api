namespace Bnpl.Api.Dto
{
    public record PurchaseResponse(int Id, decimal Amount, string Description, DateTimeOffset CreatedAt);
}
