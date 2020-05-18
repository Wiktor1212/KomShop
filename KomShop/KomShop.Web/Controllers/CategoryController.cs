using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private List<string> MenuCategories;
        private CatMenuModel MenuModel;
        private List<CategoryModel> categoryModels;
        private Icons icons = new Icons();
        public ActionResult ShowCategories(string section)      //Kafelki z kategoriami
        {
            switch (section)
            {
                /*case "LiT":

                    break;
                case "Telefony i GPS":

                    break;
                case "Komputery":

                    break;*/
                case "Podzespoły komputerowe":
                    categoryModels = new List<CategoryModel>();
                    for(int i = 0; i< icons.PodzIcons.Count(); i++)
                    {
                        categoryModels.Add(new CategoryModel
                        {
                            CategoryName = icons.Podzespoly[i].Replace("_", " "),
                            clas = icons.PodzIcons[i].ToString()
                        }); ;
                    }

                    ViewBag.Section = section;
                    return View("Categories", categoryModels);
                case "Urządzenia peryferyjne":
                    categoryModels = new List<CategoryModel>();
                    for (int i = 0; i < icons.PodzIcons.Count(); i++)
                    {
                        categoryModels.Add(new CategoryModel
                        {
                            CategoryName = icons.Peryferia[i].Replace("_", " "),
                            clas = icons.PerIcons[i].ToString()
                        }); ;
                    }
                    ViewBag.Section = section;
                    return View("Categories", categoryModels);
                /*case "Strefa Gracza":

                    break;
                case "Foto, TV i audio":

                    break;
                case "Oprogramowanie":

                    break;
                case "Akcesoria":

                    break;*/
                default:
                    return View("Index");
            }
        }
        public PartialViewResult NavigationHistory(string Section, string Category = null, string SubCategory = null) //Historia nawigacji
        {
            NavigationHistoryModel model = new NavigationHistoryModel
            {
                Section = Section,
                Category = Category ?? null,
                SubCategory = SubCategory ?? null
            };
            return PartialView(model);
        }
        public PartialViewResult CartMenu(string section)   //Poboczne menu nawigacji
        {
            switch (section)
            {
                case "Podzespoły komputerowe":      //Lista kategorii Podzespoly

                    MenuCategories = new List<string>();
                    foreach (var item in icons.Podzespoly)
                    {
                        MenuCategories.Add(item.ToString().Replace("_", " "));
                    }

                    MenuModel = new CatMenuModel
                    {
                        Categories = MenuCategories
                    };

                    ViewBag.Section = section;
                    return PartialView(MenuModel);
                case "Urządzenia peryferyjne":      //Lista kategorii Peryferia
                    MenuCategories = new List<string>();
                    foreach (var item in icons.Peryferia)
                    {
                        MenuCategories.Add(item.ToString().Replace("_", " "));
                    }

                    MenuModel = new CatMenuModel
                    {
                        Categories = MenuCategories
                    };
                    ViewBag.Section = section;
                    return PartialView(MenuModel);
                default:
                    return PartialView();
            }
        }
    }
}