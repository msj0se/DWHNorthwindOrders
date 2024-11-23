using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWHNorthwindOrders.DWHNorthwindOrders.Entities;
using DWHNorthwindOrders.Northwind.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DWHNorthwindOrders.Northwind
{
    public class NorthwindContext : DbContext
    {
        public DbSet<OrdersDetails> OrdersDetails { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<VwFactOrder> VwFactOrder { get; set; }
        public DbSet<VwFactCustomerAttended> VwFactCustomerAttended { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=northwind;User Id=ejose;Password=Pancake.s01!;TrustServerCertificate=True;")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasKey(c => c.CategoryID);

            modelBuilder.Entity<Orders>()
                .HasKey(o => o.OrderID);

            modelBuilder.Entity<Customers>()
                .HasKey(c => c.CustomerID);

            modelBuilder.Entity<Employees>()
                .HasKey(e => e.EmployeeID);

            modelBuilder.Entity<OrdersDetails>()
                .ToTable("Order Details")
                .HasKey(od => new { od.OrderID, od.ProductID });

            modelBuilder.Entity<Products>()
                .HasKey(p => p.ProductID);

            modelBuilder.Entity<Shippers>()
                .HasKey(s => s.ShipperID);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeID);

            modelBuilder.Entity<OrdersDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);

            modelBuilder.Entity<OrdersDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Shipper)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShipVia);

            modelBuilder.Entity<VwFactOrder>()
                .HasNoKey()
                .ToView("vw_factorder");

            modelBuilder.Entity<VwFactCustomerAttended>()
                .HasNoKey()
                .ToView("vw_factcustomerattended");

            modelBuilder.Entity<FactCustomerAttended>()
                .HasNoKey(); //Emelyn Del Carmen Jose

            base.OnModelCreating(modelBuilder);
        }
    }

}
