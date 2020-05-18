using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KomShop.Web.Data
{
    public class EfOrderContext : IOrdersRepository
    {
        private EfDbContext context = new EfDbContext();
        public IEnumerable<Orders> Orders
        {
            get
            {
                return context.Orders.Include(x => x.User).Include(x => x.OrderDetails);
            }
        }
        public void AddOrder(Orders order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }
    }
}