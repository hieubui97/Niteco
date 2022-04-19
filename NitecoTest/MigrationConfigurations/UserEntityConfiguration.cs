using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NitecoTest.Helper;
using NitecoTest.Models;

namespace NitecoTest.MigrationConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.Password)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);

            // Seed data
            builder.HasData(
                new User()
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    Password = HashHelper.SHA256("123456"),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            /* TODO Other Rule */
        }
    }
}
