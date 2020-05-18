using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class PromotedThings
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set;}
        public decimal Price { get; set; }
    }
}