using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;

namespace KomShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository userRepository;     //Repozytorium użytkowników.
        private IOrdersRepository ordersRepository; //Repozytorium zamówień.
        private IProductRepository productRepository;   //Repozytorium produktów.
        private ShowOrderDetailsModel OrderDetailsModel; //Model widoku ze szczegółami zamówienia.

        public AccountController(IUserRepository repository,
                                 IOrdersRepository orders,
                                 IProductRepository products)
        {
            userRepository = repository;
            ordersRepository = orders;
            productRepository = products;
        }
        public ActionResult Index(User userModel = null)   //strona użytkownika.
        {
            if (Session["UserLogin"] != null)   //Jeżeli użytkownik jest zalogowany.
            {
                if(userModel.User_ID == 0)  //Jeżeli nie przekazano danych użytkownika.
                {
                    User userDetails = userRepository.Users.FirstOrDefault(x => x.User_ID == Convert.ToInt32(Session["ID_User"])); //Wyszukuje użytkownika.
                    if(TempData["message"] != null) //Jeżeli feedback jest przekazywany.
                        userDetails.LoginErrorMessage = TempData["message"].ToString();
                    if (TempData["info"] != null)//Jeżeli feedback jest przekazywany.
                        userModel.ErrorMessage = TempData["info"].ToString();
                    return View(userDetails);   //Wygenerowanie widoku z przekazaniem modelem użytkownika.
                }
                else //Jeżeli dane użytkownika zostały przekazane.
                {
                    if (TempData["message"] != null) //Jeżeli feedback jest przekazywany.
                        userModel.LoginErrorMessage = TempData["message"].ToString();
                    if (TempData["info"] != null) //Jeżeli feedback jest przekazywany.
                        userModel.ErrorMessage = TempData["info"].ToString();
                    userModel.Login = Session["UserLogin"].ToString();  //Login użytkownika.
                    return View(userModel); //Wygenerowanie widoku z przekazaniem modelem użytkownika.
                }
            }
            else    //Jeżeli użytkownik został wylogowany
            {
                return RedirectToAction("Index", "Home");   //Przekierowanie do strony głównej.
            }
        }
        public ActionResult ChangePassword(User userModel) //Zmiana hasła.
        {
            User userDetails = userRepository.Users.FirstOrDefault(x => x.User_ID == Convert.ToInt32(Session["ID_User"]) && x.Login == userModel.Login); //Wyszukuje użytkownika dla którego ma zmienić hasło.
            if(userDetails != null && userModel.NewPassword != null && userModel.NewPassword != userDetails.Password) //Jeżeli jest taki użytkownik, nowe hasło nie jest puste i takie samo jak stare.
            {
                userRepository.ChangePassword(userModel.User_ID, userModel.NewPassword); //Wywołuje funkcje do zmiany hasła.
                TempData["message"] = "Hasło zostało zmienione!";   //Feedback.
                return RedirectToAction("Index"); //Ponowne wygenerowanie strony.
            }
            else if(userDetails == null)    //Jeżeli nie odnaleziono użytkownika.
            {
                TempData["message"] = "Coś poszło nie tak.";    //Feedback.
                return RedirectToAction("Index");   //Ponowne wygenerowanie strony.
            }
            else if (userModel.NewPassword == null) //Jeżeli nie wpisano nowego hasła.
            {
                TempData["message"] = "To pole jest wymagane";  //Feedback.
                return RedirectToAction("Index");  //Ponowne wygenerowanie strony.
            }
            else //Jeżeli nowe hasło jest takie samo jak stare.
            {
                TempData["message"] = "Twoje nowe hasło nie może być takie samo jak stare.";    //Feedback.
                return RedirectToAction("Index", userModel);    //Ponowne wygenerowanie strony.
            }
        }
        [HttpPost]
        public RedirectToRouteResult ChangeAddressData(User userModel)  //Zmiana danych adresowych.
        {
            User userDetails = userRepository.Users.FirstOrDefault(x => x.User_ID == Convert.ToInt32(Session["ID_User"]));  //Wyszukuje użytkownika.
            if (userDetails != null &&
                    userModel.Name == null &&
                    userModel.Surname == null &&
                    userModel.Address == null &&
                    userModel.PostalCode == null &&
                    userModel.City == null &&
                    userModel.Phone == null
                    )   //Jeżeli żadna wartość nie została uzupełniona.
            {
                TempData["info"] = "Co najmniej jedna wartość powinna zostać uzupełniona";  //Feedback.
                return RedirectToAction("Index", userModel);    //Ponowne wygenerowanie strony.
            }
            else if (userDetails != null)   //Jeżeli odnaleziono użytkownika i chociaż jedna wartość została uzupełniona.
            {
                TempData["info"] = "Pomyślnie dodano dane.";    //Feedback.
                userRepository.AddAddressData(userModel);   //Dodaje lub edytuje dane adresowe.
                return RedirectToAction("Index");    //Ponowne wygenerowanie strony.
            }
            else    //Jeżeli nie odnaleziono użytkownika.
            {
                TempData["info"] = "Coś poszło nie tak.";
                return RedirectToAction("Index", userModel);
            }
        }
        public ActionResult ShowOrders() //Wyświetla zamówienia użytkownika.
        {
            List<ShowOrderModel> ordersModel = new List<ShowOrderModel>();  //Lista zamówień.
            if (Session["ID_User"] != null)  //Jeżeli użytkownik jest zalogowany.
            {
                foreach (Order order in userRepository.Users.FirstOrDefault(x => x.User_ID == Convert.ToInt32(Session["ID_User"])).Orders) //Dla każdego zamówienia użytkownika.
                {
                    ordersModel.Add(ordersRepository.GetOrderModel(order)); //Dodaje zamówienia do listy.
                }
                return View(ordersModel);   //Zwraca zamówienia.
            }
            else    //Jeżeli użytkownik nie jest zalogowany.
            {
                return RedirectToAction("Index", "Home");   //Przekierowuje do strony głównej.
            }
        }
        public ActionResult ShowOrderDetails(int id)    //Wyświetla szczegóły zamówienia z id.
        {
            if(Session["UserLogin"] != null)    //Jeżeli użytkownik jest zalogowany.
            {
                List<int> product_Id = new List<int>(); //Lista ID produktów w zamówieniu.
                List<Product> products = new List<Product>();  //Nowa lista produktów.
                Order order = ordersRepository.Orders.Where(x => x.User_ID == Convert.ToInt32(Session["ID_User"])).FirstOrDefault(x => x.Order_ID == id);   //Wybiera konkretne zamówienie użytkownika po id zamówienia
                if (order == null)   //Jeżeli nie znaleziono zamówienia.
                {
                    TempData["message"] = "Nie odnaleziono zamówienia"; //Feedback.
                    return View();  //Wygenerowanie widoku.
                }
                else
                {
                    Product product = new Product();    //Tworzenie nowego produktu.
                    foreach (var detail in order.OrderDetails) //Dla każdego przedmiotu w zamówieniu.
                    {
                        product = productRepository.items.FirstOrDefault(x => x.ProductID == detail.Product_ID);    //Przypisanie konkretnego produktu z repozytorium.
                        products.Add(new Product { ProductID = product.ProductID, Title = product.Title, Price = product.Price, Quantity = detail.Quantity }); //Dodanie do listy produktów.
                    }

                    OrderDetailsModel = new ShowOrderDetailsModel   //Nowy model widoku.
                    {
                        Products = products,    //Lista produktów.
                        Order = ordersRepository.GetOrderModel(order),  //Dane zamówienia.
                        Delivery = order.Delivery //Dane adresowe.
                    };
                
                    return View(OrderDetailsModel); //Wygenerowanie widoku i przekazanie modelu.  
                }
            }
            else    //Jeżeli użytkownik jest wylogowany
            {
                return RedirectToAction("Index", "Home");   //Przeniesienie do strony głownej.
            }
        }
    }
}