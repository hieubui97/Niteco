using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NitecoTest.Models;

namespace NitecoTest.MigrationConfigurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.Address)
                .HasMaxLength(255);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);

            // Seed data
            builder.HasData(
                new Customer()
                {
                    Id = 1,
                    Name = "Hieu bm",
                    Address = "Phu Tho",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            /* TODO Other Rule */
        }
    }
}
