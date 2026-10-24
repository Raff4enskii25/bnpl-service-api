using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bnpl.Domain.Entities;

namespace Bnpl.Api.Data.Configurations
{
    public class InstallmentPlanConfiguration : IEntityTypeConfiguration<InstallmentPlan>
    {
        public void Configure(EntityTypeBuilder<InstallmentPlan> builder)
        {
            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(i => i.NumberOfInstallments)
                .IsRequired();
            builder.Property(i => i.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataConstants.StatusMaxLength);
            builder.Property(i => i.CreatedAt)
                .IsRequired();
            builder.HasOne(i => i.Purchase)
                .WithOne(p => p.InstallmentPlan)
                .HasForeignKey<InstallmentPlan>(i => i.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
