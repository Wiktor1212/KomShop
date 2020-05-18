using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Controllers
{
    public class OrdersController : Controller
    {
        private IProductRepository productRepository;
        private IOrderDetailsRepository orderDetailsRepository;
        private IOrdersRepository ordersRepository;
        private IUserRepository userRepository;
        private Cart cart = new Cart();
        private OrderModel orderModel = new OrderModel();
        private OrderDetails orderDetails = new OrderDetails();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FinalizeOrder()
        {
            int UserID = (int)Session["ID_User"];
            orderModel = new OrderModel
            {
                userDetails = userRepository.Users.FirstOrDefault(x => x.ID_User == UserID),
                Cart = (Cart)Session["cart"]
            };

            return View(orderModel);
        }
        [HttpPost]
        public ActionResult FinalizeOrder(Users userModel)
        {
            int id = 0;
            cart = (Cart)Session["cart"];
            IEnumerable<Orders> orders = ordersRepository.Orders.Where(x => x.ID_User == userModel.ID_User);
            if(orders != null)
            {
                id = orders.OrderByDescending(x => x.Order_ID).Select(x => x.Order_ID).FirstOrDefault();
            }
            Orders order = new Orders
            {
                ID_User = userModel.ID_User,
                Order_ID = id + 1,
                Price = cart.ComputeTotalValue(),
                Date = DateTime.Today,
                Status = "Przyjęte"
            };
            ordersRepository.AddOrder(order);
            foreach(var item in cart.Lines.items)
            {
                orderDetails = new OrderDetails
                {
                    Order_ID = id + 1,
                    Product_ID = item.ProductID,
                    Quantity = item.Quantity
                };
                productRepository.SellProduct(item.ProductID, item.Quantity);
                orderDetailsRepository.AddOrderDetail(orderDetails);
            }
            return RedirectToAction("Orders", "Account");
        }
    }
}