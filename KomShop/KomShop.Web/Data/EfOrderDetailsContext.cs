using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KomShop.Web.Data
{
    public class EfOrderDetailsContext : IOrderDetailsRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<OrderDetails> OrderDetails
        {
            get
            {
                return context.OrderDetails.Include(x => x.Order).Include(x => x.GPU).Include(x => x.Processor);
            }
        }
        public void AddOrderDetail(OrderDetails orderDetails)
        {
            context.OrderDetails.Add(orderDetails);
            context.SaveChanges();
        }
    }
}