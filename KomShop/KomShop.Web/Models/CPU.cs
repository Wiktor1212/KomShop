using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Models
{
    public class CPU
    {
        public List<SelectListItem> Socket = new List<SelectListItem>
        {
            new SelectListItem{Text="1151"},
            new SelectListItem{Text="AM4"}
        };
        public int pricemin, pricemax;
        public List<SelectListItem> Cores = new List<SelectListItem>
        {
            new SelectListItem{Text="2"},
            new SelectListItem{Text="4"},
            new SelectListItem{Text="6"},
            new SelectListItem{Text="8"},
            new SelectListItem{Text="10"},
            new SelectListItem{Text="12"},
            new SelectListItem{Text="14"},
            new SelectListItem{Text="16"},
            new SelectListItem{Text="18"},
            new SelectListItem{Text="20"}
        };
        public int cachemin, cachemax;
    }
}