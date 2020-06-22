using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomShop.Web.Abstract
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> Orders { get; } //Zwraca zamówienia.
        void AddOrder(int ID_User, decimal price);  //Dodaje zamówienie do bazy danych.
        ShowOrderModel GetOrderModel(Order order); //Zwraca zamówienie w postaci modelu.
    }
}
