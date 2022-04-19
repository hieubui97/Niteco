using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NitecoTest.Models;

namespace NitecoTest.MigrationConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.ProductId)
                .IsRequired();
            builder.Property(x => x.CustomerId)
                .IsRequired();
            builder.Property(x => x.Amount);
            builder.Property(x => x.OrderDate);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);
            /* TODO Other Rule */
        }
    }
}
