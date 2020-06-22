using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class HomeModel
    {
        public IEnumerable<Product> PromotedThings { get; set; }    //Promowane produkty
        public IEnumerable<Product> Bestsellers { get; set; }       //Najlepiej sprzedające się produkty
        public IEnumerable<Product> Latest { get; set; }            //Ostatnio oglądane produkty
    }
}