using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomShop.Web.Abstract
{
    public interface IDeliveryRepository
    {
        IEnumerable<Delivery> Deliveries { get; }   //Zwraca repozytorium dostaw.
        void AddDelivery(Delivery delivery);    //Dodaje miejsce dostawy do bazy danych.
        Delivery GetDeliveryDetails(int id);    //Zwraca szczegóły dostawy.
    }
}
