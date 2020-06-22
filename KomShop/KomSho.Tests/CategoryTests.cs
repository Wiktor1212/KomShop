using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KomShop.Web.Controllers;
using KomShop.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KomShop.Tests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void Can_show_categories_correctly()
        {
            //przygotowanie
            var target = new CategoryController();
            target.CatDetails.Podzespoly = new List<string> 
            {
                "Procesory",
                "Karty graficzne"
            };
            //działanie
            var result = (List<CategoryModel>)((ViewResult)target.ShowCategories("Podzespoły komputerowe")).ViewData.Model;

            //asercje
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].CategoryName, "Procesory");
            Assert.AreEqual(result[1].CategoryName, "Karty graficzne");
        }
        [TestMethod]
        public void Can_create_history_of_navigation()
        {
            //przygotowanie
            var target = new CategoryController();

            //działanie
            var result = (NavigationHistoryModel)target.NavigationHistory("Peryferia", "Monitory").Model;

            //asercje
            Assert.AreEqual(result.Section, "Peryferia");
            Assert.AreEqual(result.Category, "Monitory");
        }
    }
}
