using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class CatMenuModel
    {
        public List<string> Sections = new List<string>
            (
                new string[]
                {
                    "Laptopy i tablety",
                    "Telefony i GPS",
                    "Komputery stacjonarne",
                    "Podzespoły komputerowe",
                    "Urządzenia peryferyjne",
                    "Gaming",
                    "Foto, TV i audio",
                    "Oprogramowanie",
                    "Akcesoria"
                }
            );
        public List<string> Categories;
    }
}