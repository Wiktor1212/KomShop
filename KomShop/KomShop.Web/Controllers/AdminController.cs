using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KomShop.Web.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository products;
        private IUserRepository users;
        private IOrdersRepository orders;
        private IDeliveryRepository deliveries;
        private IOrderDetailsRepository ordersDetails;

        public AdminController(IProductRepository productRepository,
                                   IUserRepository userRepository,
                                   IOrdersRepository ordersRepository,
                                   IDeliveryRepository deliveryRepository,
                                   IOrderDetailsRepository orderDetailsRepository)
        {
            products = productRepository;
            users = userRepository;
            orders = ordersRepository;
            deliveries = deliveryRepository;
            ordersDetails = orderDetailsRepository;
        }
        public ActionResult Index()
        {
            User user = users.Users.FirstOrDefault(x => x.User_ID == Convert.ToInt32(Session["ID_User"])); //Przypisywanie użytkownika.
            if(user.IsAdmin == true)    //Jeżeli użytkownik ma uprawnienia.
            {
                return View();  //Wygeneruj widok
            }
            else    //Jeżeli użytkownik nie ma uprawnień.
            {
                return RedirectToAction("Index", "Home");   //Przekieruj do strony głównej.
            }
        }
        public ViewResult ChooseCategory()   //Strona wybierania kategorii dla nowego produktu.
        {
            return View();
        }
        public ActionResult AddOrEditProduct(string category, int id = 0)   //Edytor produktu.
        {
            ViewBag.Category = category;
            if (id != 0)
                return View(products.GetProduct(id));
            else
                return View(products.GetNewProduct(category));
        }
       [HttpPost]
        public ActionResult AddOrEditProduct(string category, //Dodawanie lub edycja produktu.
                                       FormCollection formCollection,
                                       HttpPostedFileBase image)  
        {
            if(ModelState.IsValid)
            {
                if (formCollection["Product_ID"] == "0")
                    products.AddProduct(category, formCollection, image);
                else
                    products.EditProduct(category, formCollection, image);

                TempData["message"] = "Akcja przebiegła pomyślnie.";  //Feedback.

                return RedirectToAction("ShowProducts");  //Przekierowanie do wszystkich produktów.
            }
            else
            {
                return RedirectToAction("AddOrEdit", new { category = category, id = formCollection["Product_ID"] });
            }
        }
        public ViewResult ShowProducts()    //Pokazuje wszystkie produkty z możliwością usuwania lub edycji.
        {
            return View(products.items);
        }
        public RedirectToRouteResult RemoveProduct(string category, int id) //Usuwanie produktów.
        {
            products.RemoveProduct(category, id);
            
            TempData["message"] = "Pomyślnie usunięto produkt"; //Feedback.

            return RedirectToAction("ShowProducts");    //Przekierowanie do wyświetlenia wszystkich produktów.
        }
        public ViewResult PromotedProducts()    //Zwraca listę promowanych produktów.
        {
            return View(products.items.Where(x => x.Promoted == true).ToList());
        }
        public RedirectToRouteResult AddPromotion(string searchTerm)    //Dodaje promowanie produktu.
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                TempData["message"] = "Wystąpił błąd - ta wartość musi zostać uzupełniona.";
            }
            else if (products.items.Where(x => x.Promoted == true).Count() == 9)
            {
                TempData["message"] = "Wystąpił błąd - na liście jest maksymalna ilość produktów.";
            }
            else
            {
                var productId = (int?)products.items.FirstOrDefault(x => x.Title == searchTerm).ProductID ?? null;

                if (productId == null)
                {
                    TempData["message"] = "Wystąpił błąd - nie odnaleziono produktu.";
                }
                else
                {
                    TempData["message"] = "Pomyślnie dodano.";
                    products.AddPromotion((int)productId);
                }
            }
            return RedirectToAction("PromotedProducts");
        }
        public RedirectToRouteResult RemovePromotion(int productId) //Usuwa promocje produktu.
        {
            products.RemovePromotion(productId);
            return RedirectToAction("PromotedProducts");
        }
        public JsonResult GetTitles(string term)    //Zwraca tytuły produktów dla których możliwe jest nadanie promowania
        {
            List<string> titles = products.items.Where(x => x.Title.ToLower().Contains(term.ToLower()) && x.Promoted == false).Select(y => y.Title).ToList();

            return Json(titles, JsonRequestBehavior.AllowGet);
        }
    }
}