using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Controllers
{
    public class OrdersController : Controller
    {
        private IProductRepository productRepository;   //Repozytorium produktów
        private IOrderDetailsRepository orderDetailsRepository; //Repozytorium szczegółów zamówień
        private IOrdersRepository ordersRepository; //Repozytorium zamówień
        private IDeliveryRepository deliveryRepository; //Repozytorium dostaw.
        private Cart cart { get; set; } //Nowy koszyk.
        private OrderModel orderModel { get; set; }  //Model zamówienia.
        private OrderDetails orderDetails { get; set; } //Szczegół zamówienia.
        public OrdersController(IProductRepository product,
                                IOrderDetailsRepository details,
                                IOrdersRepository order,
                                IDeliveryRepository delivery)
        {
            productRepository = product;
            orderDetailsRepository = details;
            ordersRepository = order;
            deliveryRepository = delivery;
        }
        public ActionResult FinalizeOrder() //Podsumowanie zamówienia.
        {
            if(Session["ID_User"] != null)  //Jeżeli użytkownik jest zalogowany.
            {
                orderModel = new OrderModel //Tworzenie nowego modelu zamówienia.
                {
                    DeliveryDetails = deliveryRepository.GetDeliveryDetails((int)Session["ID_User"]),   //Przypisanie danych adresowych użytkownika.
                    Cart = (Cart)Session["cart"]    //Przypisanie koszyka użytkownika.
                };

                return View(orderModel); //Wygenerowanie widoku rejestracji z przekazaniem modelu.
            }
            else
            {
                TempData["message"] = "Jeśli chcesz dokończyć zamówienie musisz się zalogować .";   //Feedback
                return RedirectToAction("Index", "Cart");   //Ponowne wygenerowanie widoku koszyka.
            }
        }
        [HttpPost]
        public ActionResult FinalizeOrder(OrderModel orderdetails)  //Finalizowanie zamówienia.
        {
            cart = (Cart)Session["cart"];   //Przypisanie koszyka użytkownika.
            bool AnyValueIsNull = orderdetails.DeliveryDetails.GetType().GetProperties().All(p => p.GetValue(orderdetails.DeliveryDetails) != null); //Sprawdza czy wszystkie dane adresowe zostały uzupełnione.
            if(cart.Products.Count() != 0 && AnyValueIsNull == false) //Jeżeli koszyk nie jest pusty i wszystkie dane adresowe zostały uzupełnione.
            {
                int id = ordersRepository.Orders.Select(x => x.Delivery_ID).DefaultIfEmpty().Max() + 1;     //ID nowego zamówienia.
                int user_id = (int)Session["ID_User"];  //Przypisanie ID użytkownika.
                ordersRepository.AddOrder(user_id, cart.ComputeTotalValue());   //Dodanie nowego zamówienia.
                foreach(var item in cart.Products)  //Dla każdego produktu w koszyku.
                {
                    orderDetails = new OrderDetails //Nowy szczegół zamówienia.
                    {
                        Order_ID = id,
                        Product_ID = item.ProductID,
                        User_ID = user_id,
                        Quantity = item.Quantity,
                        Category = item.Category
                    };
                    productRepository.SellProduct(orderDetails);    //Usuwanie przedmiotu z magazynu.
                    orderDetailsRepository.AddOrderDetail(orderDetails);    //Dodanie nowego szczegółu zamówienia.
                }
                deliveryRepository.AddDelivery(orderdetails.DeliveryDetails);   //Dodanie danych adresowych zamówienia.
                Session["cart"] = new Cart();   //Wyczyszczenie koszyka.
                return View("Success"); //Wygenerowanie widoku potwierdzającego złożenie zamówienia.
            }
            else if (AnyValueIsNull == true)    //Jeżeli nie wszystkie wartości zostały uzupełnione.
            {
                orderdetails.Cart = cart;   //Przypisanie wartości do modelu
                return View(orderdetails);  //Wygenerowanie widoku z przekazaniem modelu.
            }
            else
            {
                return RedirectToAction("Index", "Home");   //W każdym innym przypadku.
            }
        }
    }
}