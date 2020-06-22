using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class FiltersModel
    {
        public int PriceMin { get; set; }
        public int PriceMax { get; set; }
        public List<FiltersProperties> Filters { get; set; }
    }
}