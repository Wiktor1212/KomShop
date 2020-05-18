using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomShop.Web.Abstract
{
    public interface IPODRepository
    {
        IEnumerable<PoD> PoDs { get; }
    }
}
