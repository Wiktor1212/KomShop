using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class ShowOrderModel
    {
        public int Order_ID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
    }
}