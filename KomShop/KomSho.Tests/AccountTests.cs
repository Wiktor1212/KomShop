using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using KomSho.Tests.App_Start;
using KomShop.Web.Abstract;
using KomShop.Web.Controllers;
using KomShop.Web.Data;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KomShop.Tests
{
    [TestClass]
    public class AccountTests
    {
        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
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
        public void Can_change_and_add_address_data()
        {
            //przygotowanie
            var users = new List<User>
            {
                new User{User_ID = 1, Login = "XYZ", City = "Poznan"}
            }.AsQueryable();

            var usersMock = CreateDbSetMock(users);

            var userContext = new EfUserContext(new EfDbContext { Users = usersMock.Object });

            User userModel = new User
            {
                User_ID = 1,
                Login = "ZYX",
                Name = "John",
                City = "Warsaw"
            };

            //działanie
            userContext.AddAddressData(userModel);
            User result = userContext.context.Users.FirstOrDefault();

            //asercje
            Assert.IsTrue(result.Name == "John");
            Assert.IsTrue(result.City == "Warsaw");
            Assert.IsTrue(result.Login == "XYZ");
        }
        [TestMethod]
        public void Can_change_password()
        {
            //przygotowanie
            var users = new List<User>
            {
                new User{User_ID = 1, Login = "XYZ", Password = "123"}
            }.AsQueryable();

            var usersMock = CreateDbSetMock(users);

            var usersContext = new EfUserContext(new EfDbContext { Users = usersMock.Object });

            //działanie
            usersContext.ChangePassword(1, "456");
            User result = usersContext.context.Users.FirstOrDefault();
            //asercje
            Assert.AreEqual(result.Password, "456");
        }
        [TestMethod]
        public void Cant_change_password_if_the_same_like_old()
        {
            //przygotowanie
            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.Users).Returns(new List<User>
            {
                new User{User_ID = 1, Password="123"}
            });
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(x => x.HttpContext.Session["ID_User"]).Returns(1);

            var target = new AccountController(mock.Object, null, null);
            target.ControllerContext = controllerContext.Object;

            var usermodel = new User
            {
                User_ID = 1,
                Password = "123",
                NewPassword = "123"
            };

            //działanie
            target.ChangePassword(usermodel);

            //asercje
            Assert.IsTrue(target.TempData["message"].ToString().Contains("jak stare."));
        }
        [TestMethod]
        public void Can_show_orders()
        {
            //przygotowanie
            var ordersMock = new Mock<IOrdersRepository>();
            ordersMock.Setup(x => x.Orders).Returns(new List<Order>
            {
                new Order{Order_ID = 3, User_ID = 3},
                new Order{Order_ID = 1, User_ID = 1},
                new Order{Order_ID = 2, User_ID = 2},
            });

            var usersMock = new Mock<IUserRepository>();
            usersMock.Setup(x => x.Users).Returns(new List<User>
            {
                new User{User_ID = 1, Orders = ordersMock.Object.Orders.Where(x => x.User_ID == 1).ToList()},
                new User{User_ID = 2, Orders = ordersMock.Object.Orders.Where(x => x.User_ID == 2).ToList()}
            });
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(x => x.HttpContext.Session["ID_User"]).Returns(1);
            var target = new AccountController(usersMock.Object, ordersMock.Object, null);
            target.ControllerContext = controllerContext.Object;
            //działanie
            var result = (List<ShowOrderModel>)((ViewResult)target.ShowOrders()).ViewData.Model;
            //asercje
            Assert.AreEqual(result.Count(), 2);
        }
        [TestMethod]
        public void Can_show_order_details()
        {
            //przygotowanie
            var productMock = new Mock<IProductRepository>();
            productMock.Setup(x => x.items).Returns(new List<Product>
            {
                new Product{ProductID = 1, Title = "Produkt1"},
                new Product{ProductID = 2, Title = "Produkt2"}
            });
            var detailsMock = new Mock<IOrderDetailsRepository>();
            detailsMock.Setup(x => x.OrderDetails).Returns(new List<OrderDetails>
            {
                new OrderDetails{User_ID = 1, Order_ID = 1, Product_ID = 1, Quantity = 3},
                new OrderDetails{User_ID = 1, Order_ID = 1, Product_ID = 2, Quantity = 3}
            });
            var deliveryMock = new Mock<IDeliveryRepository>();
            deliveryMock.Setup(x => x.Deliveries).Returns(new List<Delivery>
            {
                new Delivery{Delivery_ID = 1, Order_ID = 1, Name = "John", Surname = "Smith", City = "Warsaw"}
            });
            var ordersMock = new Mock<IOrdersRepository>();
            ordersMock.Setup(x => x.Orders).Returns(new List<Order>
            {
                new Order {Order_ID = 1, User_ID = 1, Delivery_ID = 1, OrderDetails = detailsMock.Object.OrderDetails.Where(x => x.Order_ID == 1).ToList(), Delivery = deliveryMock.Object.Deliveries.FirstOrDefault()}
            });
            var usersMock = new Mock<IUserRepository>();
            usersMock.Setup(x => x.Users).Returns(new List<User>
            {
                new User{User_ID = 1, Orders = ordersMock.Object.Orders.Where(x => x.User_ID == 1).ToList()}
            });
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(x => x.HttpContext.Session["ID_User"]).Returns(1);
            controllerContext.Setup(x => x.HttpContext.Session["UserLogin"]).Returns("XYZ");
            var target = new AccountController(usersMock.Object, ordersMock.Object, productMock.Object);
            target.ControllerContext = controllerContext.Object;

            //działanie
            var result = (ShowOrderDetailsModel)((ViewResult)target.ShowOrderDetails(1)).ViewData.Model;
            //asercje
            Assert.IsNotNull(result.Delivery);
            Assert.AreEqual(result.Products.Count(), 2);
        }
    }
}
