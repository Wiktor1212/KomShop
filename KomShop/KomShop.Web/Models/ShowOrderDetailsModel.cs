using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class ShowOrderDetailsModel
    {
        public List<Product> Products = new List<Product>();
        public Delivery Delivery { get; set; }
        public ShowOrderModel Order { get; set; }
    }
}