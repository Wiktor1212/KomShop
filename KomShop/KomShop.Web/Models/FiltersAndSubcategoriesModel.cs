using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Models
{
    public class FiltersAndSubcategoriesModel
    {
        public List<SelectListItem> Subcategories { get; set; }
        public FiltersModel Filters { get; set; }
    }
}