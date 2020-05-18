using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomShop.Web.Abstract
{
    public interface IOrdersRepository
    {
        IEnumerable<Orders> Orders { get; }
        void AddOrder(Orders order);
    }
}
