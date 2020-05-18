using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace KomShop.Web.Models
{
    public class ProductDetailsViewModel
    {
        public Item Item { get; set; }
        public List<PropertyInfo> properties { get; set; }
        public List<string> DetailsInfo { get; set; }
        public List<string> Units { get; set; }
    }
}