using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bnpl.Domain.Entities;

namespace Bnpl.Api.Data.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Purchase.DescriptionMaxLength);
            builder.Property(p => p.CreatedAt)
                .IsRequired();
            builder.HasOne(p => p.Customer)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
