namespace Bnpl.Api.Dto
{
    public record CustomerResponse(int Id, string Name, string Email, DateTimeOffset CreatedAt);
}
