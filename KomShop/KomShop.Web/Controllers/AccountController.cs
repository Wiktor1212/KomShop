using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;

namespace KomShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository userRepository;

        public AccountController(IUserRepository repository)
        {
            userRepository = repository;
        }
        public ActionResult Index(Users userModel = null)
        {
            if (Session["UserLogin"] != null)
            {
                if(userModel.ID_User == 0)
                {
                    Users userDetails = userRepository.Users.FirstOrDefault(x => x.ID_User == Convert.ToInt32(Session["ID_User"]));
                    if(TempData["message"] != null)
                        userDetails.LoginErrorMessage = TempData["message"].ToString();
                    if (TempData["info"] != null)
                        userModel.ErrorMessage = TempData["info"].ToString();
                    return View(userDetails);
                }
                else
                {
                    if (TempData["message"] != null)
                        userModel.LoginErrorMessage = TempData["message"].ToString();
                    if (TempData["info"] != null)
                        userModel.ErrorMessage = TempData["info"].ToString();
                    userModel.Login = Session["UserLogin"].ToString();
                    return View(userModel);
                }
            }
                
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult ChangePassword(Users userModel)
        {
            Users userDetails = userRepository.Users.FirstOrDefault(x => x.ID_User == userModel.ID_User && x.Login == userModel.Login);
            if(userDetails != null && userModel.NewPassword != null && userModel.NewPassword != userDetails.Password)
            {
                userRepository.ChangePassword(userModel.ID_User, userModel.NewPassword);
                TempData["message"] = "Hasło zostało zmienione!";
                return RedirectToAction("Index");
            }
            else if(userDetails == null)
            {
                TempData["message"] = "Coś poszło nie tak.";
                return RedirectToAction("Index");
            }
            else if (userModel.NewPassword == null)
            {
                TempData["message"] = "To pole jest wymagane";
                return RedirectToAction("Index", userDetails);
            }
            else
            {
                TempData["message"] = "Twoje nowe hasło nie może być takie samo jak stare.";
                return RedirectToAction("Index", userDetails);
            }
        }
        [HttpPost]
        public ActionResult ChangeAddressData(Users userModel)
        {
            Users userDetails = userRepository.Users.FirstOrDefault(x => x.ID_User == userModel.ID_User);
            
            if (userDetails != null &&
                    userModel.Name == null &&
                    userModel.Surname == null &&
                    userModel.Adress == null &&
                    userModel.PostalCode == null &&
                    userModel.City == null &&
                    userModel.Phone == null
                    )
            {
                TempData["info"] = "Chociaż jedna wartość powinna zostać uzupełniona";
                return RedirectToAction("Index", userModel);
            }
            else if (userDetails != null)
            {
                TempData["info"] = "Pomyślnie dodano dane.";
                userRepository.AddAddressData(userModel);
                return RedirectToAction("Index", userModel);
            }
            else
            {
                TempData["info"] = "Coś poszło nie tak.";
                return RedirectToAction("Index", userModel);
            }
        }
    }
}