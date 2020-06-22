using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KomShop.Web.Data;
using KomShop.Web.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KomSho.Tests
{
    [TestClass]
    public class ProductContextTests
    {
        public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
        [TestMethod]
        public void Can_sort_repository()
        {
            //przygotowanie
            var repo = new List<Product>
            {
                new Product{ProductID=1, Price=45.00m},
                new Product{ProductID=2, Price=85.00m},
                new Product{ProductID=3, Price=55.00m},
                new Product{ProductID=4, Price=15.00m},
            };
            var target = new ProductContext();
            //działanie
            var result = target.Sort(repo, "Cena - malejąco");
            //asercje
            Assert.AreEqual(result[0].ProductID, 2);
            Assert.AreEqual(result[1].ProductID, 3);
            Assert.AreEqual(result[2].ProductID, 1);
            Assert.AreEqual(result[3].ProductID, 4);
        }
        /*[TestMethod]
        public void Can_change_quantity_of_product()
        {
            //przygotowanie
            var repo = CreateDbSetMock(new List<Processor>
            {
                new Processor{Product_ID = 1, Quantity = 20},
                new Processor{Product_ID = 2, Quantity = 5}
            });
            var target = new ProductContext();
            target.context.Processors = repo.Object;
            //działanie
            target.SellProduct(new OrderDetails { Product_ID = 1, Quantity = 5 });
            //Asercje
            Assert.AreEqual(target.TakeProcessors.ToList()[0].Quantity, 15);
        }*/
    }
}
