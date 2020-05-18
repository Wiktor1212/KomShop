using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomShop.Web.Models
{
    public class HomeModel
    {
        public IEnumerable<PromotedThings> PromotedThings;
        public IEnumerable<PoD> PoDs;
    }
}