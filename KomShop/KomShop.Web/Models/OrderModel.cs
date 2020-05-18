using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class OrderModel
    {
        public Users userDetails { get; set; }
        public Cart Cart { get; set; }
    }
}