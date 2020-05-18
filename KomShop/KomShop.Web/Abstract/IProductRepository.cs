using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomShop.Web.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Item> items { get; }
        IEnumerable<Item> TakeProcessors { get; }
        IEnumerable<Item> TakeGPUs { get; }
        List<Item> Sort(List<Item> repo, string SortType);
        void SellProduct(int productId, int quantity);
    }
}
