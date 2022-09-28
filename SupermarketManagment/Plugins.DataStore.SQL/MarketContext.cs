using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;

namespace Plugins.DataStore.SQL
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4V37A2A\\SQLEXPRESS;"+
                    "AttachDbFilename=D:\\soft\\SQL Server 2019 Express\\Microsoft SQL\\MSSQL15.SQLEXPRESS\\MSSQL\\DATA\\MarketManagement.mdf;"+
                    "Database=MarketManagement;" +
                    "Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // seeding some data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Beverage", Description = "Beverage" },
                new Category { CategoryId = 2, Name = "Bakery", Description = "Bakery" },
                new Category { CategoryId = 3, Name = "Meat", Description = "Meat" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, CategoryId = 1, Name = "Iced Tea", Price = 1.99, Quantity = 100 },
                new Product() { ProductId = 2, CategoryId = 1, Name = "Ginger Ale", Price = 1.99, Quantity = 200 },
                new Product() { ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Price = 1.50, Quantity = 300 },
                new Product() { ProductId = 4, CategoryId = 2, Name = "White Bread", Price = 1.50, Quantity = 300 }
                );
        }
    }
}
