using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomShop.Web.Abstract
{
    public interface IOrderDetailsRepository
    {
        IEnumerable<OrderDetails> OrderDetails { get; }
        void AddOrderDetail(OrderDetails orderDetails);
    }
}
