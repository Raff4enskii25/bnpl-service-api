using Bnpl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bnpl.Api.Data.Configurations
{
    public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.Property(i => i.InstallmentNumber)
                .IsRequired();
            builder.Property(i => i.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(i => i.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataConstants.StatusMaxLength);
            builder.Property(i => i.DueDate)
                .IsRequired();
            builder.HasOne(i => i.InstallmentPlan)
                .WithMany(ip => ip.Installments)
                .HasForeignKey(i => i.InstallmentPlanId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(p => new { p.InstallmentPlanId, p.InstallmentNumber })
                .IsUnique();
        }
    }
}
