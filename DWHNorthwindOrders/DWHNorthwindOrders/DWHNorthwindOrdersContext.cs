using DWHNorthwindOrders.DWHNorthwindOrders.Entities;
using DWHNorthwindOrders.Northwind.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DWHNorthwindOrders.DWHNorthwindOrders
{
    public class DWHNorthwindOrdersContext : DbContext
    {
        public DbSet<DimDate> DimDate { get; set; }
        public DbSet<DimProduct> DimProduct { get; set; }
        public DbSet<DimCustomer> DimCustomer { get; set; }
        public DbSet<DimCategory> DimCategory { get; set; }
        public DbSet<DimShipper> DimShipper { get; set; }
        public DbSet<DimEmployee> DimEmployee { get; set; }
        public DbSet<FactOrder> FactOrder { get; set; }
        public DbSet<FactCustomerAttended> FactCustomerAttended { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=DWHNorthwindOrders;User Id=ejose;Password=Pancake.s01!;TrustServerCertificate=True;")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DimDate>()
                .HasKey(d => d.DateID);
            modelBuilder.Entity<DimDate>()
                .Property(d => d.DateYear).IsRequired();
            modelBuilder.Entity<DimDate>()
                .Property(d => d.DateMonth).IsRequired();

            modelBuilder.Entity<DimProduct>()
                .HasKey(p => p.ProductID);
            modelBuilder.Entity<DimProduct>()
                .Property(p => p.ProductName).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<DimProduct>()
                .HasOne<DimCategory>()
                .WithMany()
                .HasForeignKey(p => p.CategoryID);
            modelBuilder.Entity<DimProduct>()
                .Property(p => p.UnitPrice).HasColumnType("money");
            modelBuilder.Entity<DimProduct>()
                .Property(p => p.Quantity).IsRequired();

            modelBuilder.Entity<DimCustomer>()
                .HasKey(c => c.CustomerID);
            modelBuilder.Entity<DimCustomer>()
                .Property(c => c.CompanyName).IsRequired().HasMaxLength(40);

            modelBuilder.Entity<DimCategory>()
                .HasKey(c => c.CategoryID);
            modelBuilder.Entity<DimCategory>()
                .Property(c => c.CategoryName).IsRequired().HasMaxLength(40);

            modelBuilder.Entity<DimShipper>()
                .HasKey(s => s.ShipperID);
            modelBuilder.Entity<DimShipper>()
                .Property(s => s.CompanyName).IsRequired().HasMaxLength(40);

            modelBuilder.Entity<DimEmployee>()
                .HasKey(e => e.EmployeeID);
            modelBuilder.Entity<DimEmployee>()
                .Property(e => e.Employee).IsRequired().HasMaxLength(60);

            modelBuilder.Entity<FactOrder>()
                .ToTable("FactOrder")
                .HasKey(f => f.OrderID);

            modelBuilder.Entity<FactOrder>()
                .Property(f => f.CustomerID)
                .HasMaxLength(5);

            modelBuilder.Entity<FactOrder>()
                .HasKey(f => new { f.OrderID, f.ProductID });

            modelBuilder.Entity<FactCustomerAttended>()
                .ToTable("FactCustomerAttended")
                .HasKey(f => new { f.CustomerAttended, f.EmployeeID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
