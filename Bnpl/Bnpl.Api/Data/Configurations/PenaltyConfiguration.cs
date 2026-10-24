using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bnpl.Domain.Entities;

namespace Bnpl.Api.Data.Configurations
{
    public class PenaltyConfiguration : IEntityTypeConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.Reason)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataConstants.PenaltyReasonMaxLength);
            builder.Property(p => p.AccrualDate)
                .IsRequired();
            builder.HasOne(p => p.Installment)
                .WithOne(i => i.Penalty)
                .HasForeignKey<Penalty>(p => p.InstallmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
