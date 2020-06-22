using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
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
        public IEnumerable<Order> Orders   //Zwraca repozytorium zamówień.
        {
            get
            {
                return context.Orders.Include(x => x.OrderDetails).Include(x => x.User).Include(x => x.Delivery);
            }
        }
        public void AddOrder(int ID_User, decimal price)    //Dodaje zamówienie do bazy.
        {
            context.Orders.Add(new Order
            {
                User_ID = ID_User,
                Order_ID = context.Orders.Select(x => x.Order_ID).DefaultIfEmpty().Max() + 1,
                Price = price,
                Date = DateTime.Today,
                Status = "Przyjęte",
                Delivery_ID = context.Deliveries.OrderByDescending(x => x.Delivery_ID).Select(x => x.Delivery_ID).FirstOrDefault() + 1,
                DeliveryType = "Kurier - pobranie"
            });
            context.SaveChanges();
        }
        public ShowOrderModel GetOrderModel(Order order)   //Tworzy model z konkretnego zamówienia.
        {
            return new ShowOrderModel
            {
                Order_ID = order.Order_ID,
                Date = order.Date,
                Price = order.Price,
                Status = order.Status
            };
        }
    }
}