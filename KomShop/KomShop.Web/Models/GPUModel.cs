using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Models
{
    public class GPUModel
    {
        public List<SelectListItem> Producent = new List<SelectListItem>
        {
            new SelectListItem{Text="Asus"},
            new SelectListItem{Text="Evga"},
            new SelectListItem{Text="Gigabyte"},
            new SelectListItem{Text="Aorus"},
            new SelectListItem{Text="MSI"}
        };
        public List<SelectListItem> Model = new List<SelectListItem>
        {
            new SelectListItem {Text = "GTX 10xx", Value = "GTX 10"},
            new SelectListItem {Text = "GTX 9xx", Value = "GTX 9"},
            new SelectListItem {Text = "RTX 20xx", Value = "RTX 20"},
            new SelectListItem {Text = "RX 5xx", Value = "RX 5"}
        };
        public List<SelectListItem> MemoryType = new List<SelectListItem>
        {
            new SelectListItem {Text="GDDR5"},
            new SelectListItem {Text="GDDR6"}
        };
        public List<SelectListItem> MemorySize = new List<SelectListItem>
        {
            new SelectListItem {Text="4GB", Value="4"},
            new SelectListItem {Text="6GB", Value="6"},
            new SelectListItem {Text="8GB", Value="8"},
            new SelectListItem {Text="11GB", Value="11"}
        };
        public int pricemin, pricemax;
    }
}