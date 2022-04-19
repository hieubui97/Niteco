using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NitecoTest.Context.LogingEvent;
using NitecoTest.Helper;
using NitecoTest.Models;

namespace NitecoTest.Context
{
    public class AppDataContext : DbContext
    {
        private readonly string _connectionString;
        public AppDataContext(string connectionString) : base()
        {
            this._connectionString = connectionString;
        }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public static readonly ILoggerFactory loggerFactory =
            LoggerFactory.Create(
                builder =>
                {
                    builder.AddProvider(new SingletonLoggerProvider(new EntityFrameworkMySqlLogger((m) =>
                    {
                        var log = $"SQL Query:\r\n{m.CommandText}\r\nElapsed:{m.Elapsed} milliseconds\r\n\r\n";
                        Console.WriteLine(log);
                        IoHelper.WriteToTextFile("sql-log.txt", log, true);
                    })));
                    builder.AddConsole();

                    //builder.AddFilter(Level => Level == LogLevel.Information);
                }
            );

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .UseSqlServer(_connectionString,
                        sqlServerOptionsAction: o =>
                        {
                            o.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        });
            }

            //// Convert naming convention from PascalCamelCase to SnakeCase.
            //optionsBuilder.UseSnakeCaseNamingConvention();
        }

        /// <summary>
        /// This will call every configuration in src/Infra/Persistence/Configurations/*.cs
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This applies all configuration from all IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");

            base.OnModelCreating(modelBuilder);
        }

        // Add table entities
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
