using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using KomShop.Web.Controllers;
using KomShop.Web.Abstract;
using System.Collections.Generic;
using KomShop.Web.Entities;
using Ninject.Planning.Targets;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KomSho.Tests
{
    [TestClass]
    public class HomePageTests
    {
        [TestMethod]
        public void CanShowPromotedProducts()   //Czy potrafi wybrać oferty promowane.
        {
            //Przygotowanie
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.items).Returns(new List<Product>
            {
                new Product{ProductID = 1, Promoted = true},
                new Product{ProductID = 2, Promoted = false},
                new Product{ProductID = 3, Promoted = true}
            });

            HomeController target = new HomeController(mock.Object);

            //działanie
            List<Product> result = target.GetPromoted().ToList();

            //asercje
            Assert.IsTrue(result[0].ProductID == 1 && result[0].Promoted == true);
            Assert.IsTrue(result[1].ProductID == 3 && result[1].Promoted == true);
        }
        [TestMethod]
        public void CanShowBestsellers()
        {
            //przygotowanie
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.items).Returns(new List<Product>
            {
                new Product {ProductID = 1, SoldPieces = 10},
                new Product{ProductID = 2, SoldPieces = 5},
                new Product{ProductID = 3, SoldPieces = 8},
                new Product{ProductID = 4, SoldPieces = 3},
                new Product{ProductID = 5, SoldPieces = 1},
                new Product{ProductID = 6, SoldPieces = 16},
            });
            var target = new HomeController(mock.Object);

            //działanie
            List<Product> result = target.GetBestsellers().ToList();

            //asercje
            Assert.IsTrue(result.Count() == 5);
            Assert.IsTrue(result[0].ProductID == 6);
            Assert.IsTrue(result[1].ProductID == 1);
        }
        [TestMethod]
        public void CanShowLastWatched()
        {
            //przygotowanie
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.items).Returns(new List<Product>
            {
                new Product{ProductID = 1},
                new Product{ProductID = 2},
                new Product{ProductID = 3},
                new Product{ProductID = 4},
            });
            List<int?> products = new List<int?> { 1, 3 };
            var controllerContext = new Mock<ControllerContext>();
            var target = new HomeController(mock.Object);
            //działanie
            controllerContext.SetupGet(x => x.HttpContext.Session["Latest"]).Returns(products);
            target.ControllerContext = controllerContext.Object;
            List<Product> result = target.GetLatest().ToList();
            //asercje
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result[0].ProductID == 3);
            Assert.IsTrue(result[1].ProductID == 1);
        }
    }
}
