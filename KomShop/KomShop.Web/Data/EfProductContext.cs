using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KomShop.Web.Data
{
    public class EfProductContext : IProductRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Item> items
        {
            get
            {
                List<Item> p = (from x in context.Processors
                        select new Item
                        {
                            ProductID = x.Product_ID,
                            Title = x.Title,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            FDetail = SqlFunctions.StringConvert(x.Clock, 5, 1),
                            SDetail = SqlFunctions.StringConvert((double)x.Cores),
                            TDetail = SqlFunctions.StringConvert((decimal)x.Cache),
                            OrderDetails = x.OrderDetails
                        }).ToList();
                List<Item> g = (from x in context.GPUs
                         select new Item
                         {
                             ProductID = x.Product_ID,
                             Title = x.Title,
                             Price = x.Price,
                             Quantity = x.Quantity,
                             FDetail = x.Model,
                             SDetail = x.MemoryType,
                             TDetail = SqlFunctions.StringConvert((decimal)x.Memory),
                             OrderDetails = x.OrderDetails
                         }).ToList();
                List<Item> it = new List<Item>();
                it.AddRange(p);
                it.AddRange(g);
                return it;
            }
        }
        public IEnumerable<Item> TakeProcessors
        {
            get
            {
                return (from x in context.Processors
                        select new Item
                        {
                            ProductID = x.Product_ID,
                            Title = x.Title,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            FDetail = SqlFunctions.StringConvert(x.Clock, 5, 1),
                            SDetail = SqlFunctions.StringConvert((double)x.Cores),
                            TDetail = SqlFunctions.StringConvert((decimal)x.Cache)
                        }).ToList();
            }
        }
        public IEnumerable<Item> TakeGPUs
        {
            get
            {
                return (from x in context.GPUs
                        select new Item
                        {
                            ProductID = x.Product_ID,
                            Title = x.Title,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            FDetail = x.Model,
                            SDetail = x.MemoryType,
                            TDetail = SqlFunctions.StringConvert((decimal)x.Memory)
                        }).ToList();
            }
        }
        public List<Item> Sort(List<Item> repo, string SortType)
        {
            switch(SortType)
            {
                case "Cena: od najdroższych":
                    repo = repo.OrderByDescending(x => x.Price).ToList();
                    break;
                case "Cena: od najtańszych":
                    repo = repo.OrderBy(x => x.Price).ToList();
                    break;
                case "Nazwa A-Z":
                    repo = repo.OrderBy(x => x.Title).ToList();
                    break;
                case "Nazwa Z-A":
                    repo = repo.OrderByDescending(x => x.Title).ToList();
                    break;
                default: break;
            }
            return repo;
        }
        public void SellProduct(int productId, int quantity)
        {
            if (context.Processors.FirstOrDefault(x => x.Product_ID == productId) != null)
            {
                context.Processors.FirstOrDefault(x => x.Product_ID == productId).Quantity -= quantity;
            }
            if(context.GPUs.FirstOrDefault(x => x.Product_ID == productId) != null)
            {
                context.GPUs.FirstOrDefault(x => x.Product_ID == productId).Quantity -= quantity;
            }
            context.SaveChanges();
        }
    }
}