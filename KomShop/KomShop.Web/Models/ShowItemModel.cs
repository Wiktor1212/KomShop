using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class ShowItemModel
    {
        public List<Product> Product { get; set; }
        public List<string> DetailsInfo { get; set; }
        public List<string> Units { get; set; }
    }
}