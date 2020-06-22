using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class EditOrderModel
    {
        public Order OrderInfo { get; set; }
        public Delivery DeliveryInfo { get; set; }
        public List<OrderDetails> Details { get; set; }
    }
}