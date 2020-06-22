using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;

namespace KomShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;   //Wszystkie produkty.
        public HomeController(IProductRepository product)
        {
            productRepository = product;
        }
        public ViewResult Index()
        {
            HomeModel model = new HomeModel //Tworzenie nowego modelu dla strony głównej.
            {
                PromotedThings = GetPromoted(),         //Promowane przedmioty.
                Bestsellers = GetBestsellers(),         //Najlepiej sprzedawane przedmioty wg. ilości zamówień.
                Latest = GetLatest()                    //5 ostatnio przeglądanych przedmiotów.
            };
            return View(model);
        }
        
        public IEnumerable<Product> GetPromoted()   //Wybiera promowane przedmioty.
        {
            return productRepository.items.Where(x => x.Promoted == true); //Wyszukuje z repozytorium przedmioty z wartością promoted równej true.
        }
        public IEnumerable<Product> GetBestsellers()    //Wybiera najlepiej sprzedające się przedmioty.
        {
            return productRepository.items.OrderByDescending(x => x.SoldPieces).Take(5);  //Sortuje repozytorium malejąco po ilości zamówień na dany przedmioti wybiera pierwsze 5.
        }
        public IEnumerable<Product> GetLatest() //Wyszukuje ostatnio przeglądane przedmioty po ich ID zapisanych w danych sesji.
        {
            List<Product> items = new List<Product>();  //Nowa lista produktów
            if(Session["Latest"] != null)   //Jeśli dane sesji istnieją
            {
                foreach(int id in (List<int?>)Session["Latest"])    //Dla każdego id zapisanego w danych sesji
                {
                    items.Add(productRepository.items.FirstOrDefault(x => x.ProductID == id));  //Dodaj produkt o konkretnym id
                }   
            }

            return Enumerable.Reverse(items).ToList();  //Zwraca odwróconą listę produktów, aby ostatnio przeglądana rzecz była pierwsza
        }
    }
}