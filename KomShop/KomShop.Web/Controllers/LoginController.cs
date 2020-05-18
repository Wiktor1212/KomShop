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
        private IUserRepository users;
        public LoginController(IUserRepository userRepository)
        {
            users = userRepository;
        }
        public ActionResult Index()
        {
            Users userModel = new Users();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Index(Users userModel)
        {
            if(userModel.Login != null && userModel.Password != null)
            {
                Users userDetails = users.Users.Where(x => x.Login == userModel.Login && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Zły login lub hasło.";
                    return View(userModel);
                }
                else
                {
                    Session["ID_User"] = userDetails.ID_User;
                    Session["UserLogin"] = userDetails.Login;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(userModel);
            }
        }
        public ActionResult Logout()
        {
            Session["ID_user"] = null;
            Session["UserLogin"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            Users userModel = new Users();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Register(Users userModel)
        {
            if(ModelState.IsValid)
            {
                Users userDetails = users.Users.Where(u => u.Login == userModel.Login).FirstOrDefault();
                if(userDetails ==  null)
                {
                users.Register(userModel);
                TempData["data"] = string.Format("Rejestracja powiodła się, teraz możesz się zalogować {0}", userModel.Login);
                ModelState.Clear();
                return RedirectToAction("Index");
                }
                else
                {
                    userModel.LoginErrorMessage = "Użytkownik z taką nazwą już istnieje.";
                    return View(userModel);
                }
            }
            else
            {
                return View(userModel);
            }
        }
    }
}