using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bnpl.Domain.Entities;

namespace Bnpl.Api.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.PaidAt)
                .IsRequired();
            builder.HasOne(p => p.Installment)
                .WithOne(i => i.Payment)
                .HasForeignKey<Payment>(p => p.InstallmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
