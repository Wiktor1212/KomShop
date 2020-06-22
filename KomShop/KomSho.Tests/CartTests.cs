using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using KomShop.Web.Abstract;
using KomShop.Web.Controllers;
using KomShop.Web.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KomShop.Tests
{
    [TestClass]
    public class CartTests
    {
        Mock<IProductRepository> productRepo()
        {
            var productRepoMock = new Mock<IProductRepository>();
            productRepoMock.Setup(x => x.items).Returns(new List<Product>
            {
                new Product{ProductID = 1, Title = "Produkt1", Price = 20m, Category = "Procesory"},
                new Product{ProductID = 2, Title = "Produkt2", Price = 15m, Category = "Procesory"},
                new Product{ProductID = 4, Title = "Produkt4", Price = 60m, Category = "Karty graficzne"},
                new Product{ProductID = 3, Title = "Produkt3", Price = 10m, Category = "Procesory"},
            });
            return productRepoMock;
        }
        [TestMethod]
        public void Can_add_product()
        {
            //przygotowanie
            var cart = new Cart();
            cart.productRepository = productRepo().Object;
            //działanie
            cart.AddItem(1, 2);
            cart.AddItem(3, 1);

            //asercje
            Assert.AreEqual(cart.Products.Count, 2);
        }
        [TestMethod]
        public void Can_remove_product()
        {
            //przygotowanie
            var cart = new Cart();
            cart.productRepository = productRepo().Object;
            cart.AddItem(1, 2);
            cart.AddItem(2, 3);
            cart.AddItem(3, 1);

            //działanie
            cart.RemoveItem(2);

            //asercje
            Assert.AreEqual(cart.Products.Count, 2);
            Assert.AreEqual(cart.Products[0].ProductID, 1);
            Assert.AreEqual(cart.Products[1].ProductID, 3);
        }
        [TestMethod]
        public void Can_change_quantity()
        {
            //przygotowanie
            var cart = new Cart();
            cart.productRepository = productRepo().Object;
            cart.AddItem(3, 2);

            //działanie
            cart.ChangeQuantity(3, 10);

            //asercje
            Assert.AreEqual(cart.Products.Count, 1);
            Assert.AreEqual(cart.Products[0].Quantity, 10);
        }
    }
}
