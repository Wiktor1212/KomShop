using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KomShop.Web.Data
{
    public class EfDbContext : DbContext
    {
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.User_ID);
            modelBuilder.Entity<CPU>().HasKey(x => new { x.Product_ID });
            modelBuilder.Entity<GPU>().HasKey(x => x.Product_ID);
            modelBuilder.Entity<Order>().HasKey(x => new { x.Order_ID, x.User_ID });
            modelBuilder.Entity<OrderDetails>().HasKey(x => new { x.Order_ID, x.Product_ID });

            modelBuilder.Entity<OrderDetails>().HasRequired(x => x.Processor).WithMany(x => x.OrderDetails).HasForeignKey(x => x.Product_ID);
            modelBuilder.Entity<OrderDetails>().HasRequired(x => x.GPU).WithMany(x => x.OrderDetails).HasForeignKey(x => x.Product_ID);
            modelBuilder.Entity<OrderDetails>().HasRequired(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => new { x.Order_ID, x.User_ID });
            modelBuilder.Entity<Order>().HasRequired(x => x.User).WithMany(X => X.Orders).HasForeignKey(x => x.User_ID);
            modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithRequired(x => x.Order).HasForeignKey(x => new { x.Order_ID, x.User_ID });
            modelBuilder.Entity<Order>().HasRequired(x => x.Delivery).WithMany(x => x.Orders).HasForeignKey(x => x.Delivery_ID);
        }
    }
}