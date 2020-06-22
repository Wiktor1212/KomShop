using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Models
{
    public class FiltersProperties
    {
        public string Name { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public List<SelectListItem> Values { get; set; }
    }
}