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
        public DbSet<Processor> Processors { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(x => x.ID_User);
            modelBuilder.Entity<Processor>().HasKey(x => new { x.Product_ID });
            modelBuilder.Entity<GPU>().HasKey(x => x.Product_ID);
            modelBuilder.Entity<Orders>().HasKey(x => new { x.Order_ID, x.ID_User });
            modelBuilder.Entity<OrderDetails>().HasKey(x => new { x.Order_ID, x.Product_ID });

            modelBuilder.Entity<Orders>().HasMany(x => x.OrderDetails).WithRequired(x => x.Order);
            modelBuilder.Entity<Orders>().HasRequired(x => x.User).WithMany(x => x.Orders);

            modelBuilder.Entity<OrderDetails>().HasRequired(x => x.Processor).WithMany(x => x.OrderDetails);

            modelBuilder.Entity<Processor>().HasMany(x => x.OrderDetails).WithRequired(x => x.Processor);
            modelBuilder.Entity<GPU>().HasMany(x => x.OrderDetails).WithRequired(x => x.GPU);

            modelBuilder.Entity<Users>().HasMany(x => x.Orders).WithRequired(x => x.User);
        }*/
    }
}