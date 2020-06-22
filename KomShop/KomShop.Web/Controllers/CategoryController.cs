using KomShop.Web.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;

namespace KomShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private List<CategoryModel> categoryModels = new List<CategoryModel>(); //Model dla kategorii.
        public CategoryDetails CatDetails = new CategoryDetails();   //Klasa z kategoriami.
        public ActionResult ShowCategories(string section)      //Kategorie dla danego działu.
        {
            switch (section)    //W zależności od działu
            {
                /*case "LiT":

                    break;
                case "Telefony i GPS":

                    break;
                case "Komputery":

                    break;*/
                case "Podzespoły komputerowe":
                    for(int i = 0; i< CatDetails.Podzespoly.Count(); i++) //Dla każdej kategorii w dziale.
                    {
                        categoryModels.Add(new CategoryModel        //Dodaj do modelu.
                        {
                            CategoryName = CatDetails.Podzespoly[i],   //Nazwa kategorii.
                            clas = CatDetails.PodzIcons[i].ToString()  //Ikona kategorii.
                        });
                    }
                    ViewBag.Section = section;  //Dane ViewBag działu.
                    return View("Categories", categoryModels);  //Wygenerowanie widoku z przekazaniem modelu.

                case "Urządzenia peryferyjne":
                    for (int i = 0; i < CatDetails.Peryferia.Count(); i++)//Dla każdej kategorii w dziale.
                    {
                        categoryModels.Add(new CategoryModel    //Dodaj do modelu.
                        {
                            CategoryName = CatDetails.Peryferia[i], //Nazwa kategorii.
                            clas = CatDetails.PerIcons[i].ToString() //Ikona kategorii.
                        });
                    }
                    ViewBag.Section = section;  //Dane ViewBag działu.
                    return View("Categories", categoryModels);  //Wygenerowanie widoku z przekazaniem modelu.
                /*case "Strefa Gracza":

                    break;
                case "Foto, TV i audio":

                    break;
                case "Oprogramowanie":

                    break;
                case "Akcesoria":

                    break;*/
                default:
                    return RedirectToAction("Index", "Home");   //Przekierowanie do strony główniej.
            }
        }
        public PartialViewResult NavigationHistory(string Section, string Category = null, string SubCategory = null) //Historia nawigacji
        {
            NavigationHistoryModel model = new NavigationHistoryModel   //Tworzenie nowego modelu nawigacji poprzez przypisanie zmiennych.
            {
                Section = Section,
                Category = Category ?? null,
                SubCategory = SubCategory ?? null
            };
            return PartialView(model);  //Wygenetowanie widoku częściowego z przekazaniem modelu.
        }
        public PartialViewResult CatMenu(string section)   //Poboczne menu nawigacji.
        {
            List<string> Categories = new List<string>();    //Model dla częściowego widoku z działami i kategoriami.
            switch (section)
            {
                case "Podzespoły komputerowe":      //Lista kategorii dla działu Podzespoly.

                    Categories = CatDetails.Podzespoly;  //Przypisz listę nazw kategorii.
                    ViewBag.Section = section;  //Przypisz do ViewBag nazwę działu.
                    return PartialView(Categories);  //Wygeneruj widok i przekarz model.

                case "Urządzenia peryferyjne":      //Lista kategorii dla działu Peryferia.
                    Categories = CatDetails.Peryferia; //Przypisz listę nazw kategorii.
                    ViewBag.Section = section;  //Przypisz do ViewBag nazwę działu.
                    return PartialView(Categories);  //Wygeneruj widok i przekarz model.

                default:
                    return PartialView();   //Zwróć pusty widok.
            }
        }
    }
}