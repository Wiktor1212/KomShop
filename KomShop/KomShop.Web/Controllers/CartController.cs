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
        private IProductRepository productRepository; //Repozytorium produktów.

        public CartController(IProductRepository repository)
        {
            productRepository = repository;
        }
        public ViewResult Index(string returnURL)   //Wygenerowanie strony głównej.
        {
            return View(returnURL);
        }
        public PartialViewResult Details()  //Widok częściowy z produktami w koszyku.
        {
            return PartialView(GetCart());  //Wygenerowanie widoku częściowego z przekazaniem koszyka.
        }
        public RedirectToRouteResult AddItem(int ProductID, string returnURL, int quantity = 1)   //Dodanie produktu do koszyka.
        {
            GetCart().AddItem(ProductID, quantity);     //Wywołuje akcję dodającą dany produkt do koszyka.
            return RedirectToAction("Index","Cart");    //Przekierowanie do akcji Index.
        }
        public RedirectToRouteResult ChangeQuantity(int product_Id, int quantity)   //Zmienia ilość przedmiotów.
        {
            GetCart().ChangeQuantity(product_Id, quantity); //Wywołuje akcję zmieniającą ilość danego przedmiotu.
            return RedirectToAction("Details"); //Aktualizuje widok częściowy.
        }
        public RedirectToRouteResult RemoveItem(int id) //Usuwa dany przedmiot z koszyka.
        {
            GetCart().RemoveItem(id);   //Wywołuje akcję usuwającą produkt z koszyka.
            return RedirectToAction("Details"); //Aktualizuje widok częściowy.
        }
        public RedirectToRouteResult CheckForQuantities()   //Sprawdza czy można dokończyć zamówienie.
        {
            bool CanBuy()   //Sprawdza czy w magazynie jest odpowiednia ilość danych produktów.
            {
                int count = 0; //Zlicza ilość przedmiotów nie do kupienia.
                foreach(Product product in GetCart().Products)  //Dla każdego produktu w koszyku.
                {
                    Product item = productRepository.items.FirstOrDefault(x => x.ProductID == product.ProductID);   //Przypisuje produkt z repozytorium.
                    if (product.Quantity > item.Quantity)   //Jeżeli użytkownik chce kupić więcej produktów niż jest w magazynie.
                    {
                        count++;    //Dodaje 1 do ilości niemożliwych produktów do kupienia.
                        if(item.Quantity >= 1)  //Jeżeli w magazynie jest co najmniej jeden produkt.
                            TempData[product.ProductID.ToString()] = "Przykro nam ale obecnie mamy w sklepie tylko " + item.Quantity + " szt. tego przedmiotu"; //Feedback.
                        else    //Jeżeli produktu nie ma już w magazynie.
                            TempData[product.ProductID.ToString()] = "Niestety, nie mamy już tego produktu";    //Feedback.
                    }
                }
                return count == 0 ? true : false;   //Jeżeli użytkownik może kupić wszystko zwraca prawdę, jeżeli nie, zwraca fałsz.
            }
            if (CanBuy())   //Jeżeli użytkownik może kupić
                return RedirectToAction("FinalizeOrder", "Orders"); //Przejście do podsumowania.
            else
                return RedirectToAction("Index");   //Wygenerowanie widoku koszyka z informacjami o produktach.
        }
        private Cart GetCart()  //Przypisanie koszyka.
        {
            Cart cart = (Cart)Session["Cart"];  //Odnalezienie koszyka w danych sesji.
            if(cart == null)    //Jeżeli nie odnaleziono koszyka.
            {
                cart = new Cart();  //Stwórz nowy koszyk.
                Session["Cart"] = cart; //Przypisz nowy koszyk.
            }
            return cart;    //Zwróć koszyk.
        }
    }
}