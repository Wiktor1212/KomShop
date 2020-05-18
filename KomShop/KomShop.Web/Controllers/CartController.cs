using KomShop.Web.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using System.Diagnostics;

namespace KomShop.Web.Controllers
{
    public class CartController : Controller
    {
        public ViewResult Index(string returnURL)
        {
            return View(new CartViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnURL
            });
        }
        public RedirectToRouteResult AddItem(int ProductID ,decimal Price, string Title, string returnURL, int quantity = 1)
        {
            GetCart().AddItem(ProductID, quantity, Price, Title);
            return RedirectToAction("Index", new { returnURL });
        }
        public RedirectToRouteResult RemoveProcessorFromCart(int ProductID, string returnURL)
        {
            GetCart().RemoveItem(ProductID);
            return RedirectToAction("Index", new { returnURL });
        }
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}