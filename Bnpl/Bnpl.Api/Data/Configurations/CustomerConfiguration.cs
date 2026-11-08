using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bnpl.Domain.Entities;
using Bnpl.Domain.Validation;

namespace Bnpl.Api.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(Customer.MaxNameLength);
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(EmailNormalization.MaxLength);
            builder.HasIndex(c => c.Email)
                .IsUnique();
            builder.Property(c => c.CreatedAt)
                .IsRequired();
        }
    }
}
