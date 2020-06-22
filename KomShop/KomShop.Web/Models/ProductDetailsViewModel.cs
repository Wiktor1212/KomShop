using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace KomShop.Web.Models
{
    public class ProductDetailsViewModel
    {
        public int Product_ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<ProductDetails> Details = new List<ProductDetails>();
    }
}