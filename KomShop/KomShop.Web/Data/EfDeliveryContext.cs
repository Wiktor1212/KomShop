using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KomShop.Web.Data
{
    public class EfDeliveryContext : IDeliveryRepository
    {
        private EfDbContext context = new EfDbContext();
        public IEnumerable<Delivery> Deliveries //Zwraca kolekcję dostaw.
        {
            get
            {
                return context.Deliveries;
            }
        }
        public void AddDelivery(Delivery delivery)  //Dodaje dostawę do bazy danych.
        {
            context.Deliveries.Add(delivery);
            context.SaveChanges();
        }
        public Delivery GetDeliveryDetails(int id)  //Pobiera szczegóły dostawy.
        {
            User user = context.Users.FirstOrDefault(x => x.User_ID == id);
            return new Delivery
            {
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                City = user.City,
                Phone = user.Phone,
                PostalCode = user.PostalCode
            };
        }
    }
}