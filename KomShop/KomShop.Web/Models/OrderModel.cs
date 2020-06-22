using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class OrderModel
    {
        public Delivery DeliveryDetails { get; set; }
        public Cart Cart { get; set; }
    }
}