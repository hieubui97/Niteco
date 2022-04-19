using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NitecoTest.Models;

namespace NitecoTest.MigrationConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(500);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);

            // Seed data
            builder.HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Iphone",
                    Description = "Description",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            /* TODO Other Rule */
        }
    }
}
