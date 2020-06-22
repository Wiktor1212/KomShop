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

        public IEnumerable<OrderDetails> OrderDetails   //Zwraca repozytorium szczegołów zamówień.
        {
            get
            {
                return context.OrderDetails.Include(x => x.GPU).Include(x => x.Processor).Include(x => x.Order);
            }
        }
        public void AddOrderDetail(OrderDetails orderDetails)   //Dodaje szczegół zamówienia do bazy.
        {
            context.OrderDetails.Add(orderDetails);
            context.SaveChanges();
        }
    }
}