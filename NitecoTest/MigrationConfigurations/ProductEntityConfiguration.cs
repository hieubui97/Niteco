using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NitecoTest.Models;

namespace NitecoTest.MigrationConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.CategoryId)
                .IsRequired();
            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.Price);
            builder.Property(x => x.Description)
                .HasMaxLength(500);
            builder.Property(x => x.Quantity);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);

            // Seed data
            builder.HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Iphone x",
                    Description = "IPhone",
                    Quantity = 100,
                    Price = 100,
                    CategoryId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            /* TODO Other Rule */
        }
    }
}
