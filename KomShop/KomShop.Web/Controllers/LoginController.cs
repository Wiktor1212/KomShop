using KomShop.Web.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomShop.Web.Entities;

namespace KomShop.Web.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository users;  //Repozytorium użytkowników.
        public LoginController(IUserRepository userRepository)
        {
            users = userRepository;
        }
        public ViewResult Index() //Strona główna logowania.
        {
            User userModel = new User();  //Nowy użytkownik.
            return View(userModel); //Wygenerowanie widoku z przekazaniem modelu.
        }
        [HttpPost]
        public ActionResult Index(User userModel)  //Sprawdza poprawność wprowadzonych danych.
        {
            if(userModel.Login != null && userModel.Password != null)   //Jeżeli wpisano obie wartości.
            {
                User userDetails = users.Users.Where(x => x.Login == userModel.Login && x.Password == userModel.Password).FirstOrDefault();    //Wyszukuje użytkownika o podanym loginie i haśle.
                if (userDetails == null)    //Jeżeli nie wyszuka takiego użytkownika.
                {
                    userModel.LoginErrorMessage = "Zły login lub hasło.";   //Feedback.
                    return View(userModel); //Wygenerowanie widoku z przekazaniem modelu.
                }
                else    //Jeżeli znaleziono użytkownika.
                {
                    Session["ID_User"] = userDetails.User_ID;   //Przypisanie wartości
                    Session["UserLogin"] = userDetails.Login;   // do danych sesji.
                    return RedirectToAction("Index", "Home");   //Przekierowanie do strony głównej.
                }
            }
            else
            {
                return View(userModel); //Wygenerowanie widoku z przekazaniem modelu.
            }
        }
        public RedirectToRouteResult Logout()    //Wylogowanie bez niszczenia koszyka.
        {
            Session["ID_user"] = null;      //Usunięcie danych
            Session["UserLogin"] = null;    //zalogowanego użytkownika.
            return RedirectToAction("Index", "Home");   //Przekierowanie do strony głównej.
        }
        public ViewResult Register()  //Strona główna rejestracji.
        {
            User userModel = new User();  //Stworzenie nowego użytkownika.
            return View(userModel); //Wygenerowanie widoku z przekazaniem modelu.
        }
        [HttpPost]
        public ActionResult Register(User userModel)   //Sprawdza poprawność danych.
        {
            if (ModelState.IsValid)     //Jeżeli wszystkie wartości zostały uzupełnione.
            {
                User userDetails = users.Users.Where(u => u.Login == userModel.Login).FirstOrDefault();    //Sprawdza czy użytkownik o takim samym loginie istnieje.
                if(userDetails ==  null)    //Jeżeli nie istnieje.
                {
                users.Register(userModel);  //Zarejestruj nowego użytkownika.
                TempData["data"] = string.Format("Rejestracja powiodła się, teraz możesz się zalogować {0}", userModel.Login);  //Feedback.
                ModelState.Clear(); //Usuwa błędy ModelState.
                return RedirectToAction("Index");   //Przekierowuje użytkownika do strony logowania.
                }
                else    //Jeżeli taki użytkownik istnieje
                {
                    userModel.LoginErrorMessage = "Użytkownik z taką nazwą już istnieje.";  //Feedback.
                    return View(userModel); //Wygenerowanie widoku rejestracji z przekazaniem modelu.
                }
            }
            else    //Jeżeli nie wszystkie wartości zostały uzupełnione.
            {
                return View(userModel); //Wygenerowanie widoku rejestracji z przekazaniem modelu.
            }
        }
    }
}