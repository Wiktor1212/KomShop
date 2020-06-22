using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KomShop.Web.Entities;
using KomShop.Web.Models;

namespace KomShop.Web.Abstract
{
    public interface IProcessorRepository
    {
        IEnumerable<CPU> Processors { get; }  //Zwraca repozytorium procesorów.
        void AddProduct(CPU product); //Dodaje produkt do bazy danych.
        List<FiltersProperties> GetFilterProperties();  //Zwraca listę filtrów.
        List<SelectListItem> GetSubcategories();    //Zwraca listę podkategorii.
        void EditProduct(CPU product);    //Zastępuje stary produkt nowym, zedytowanym.
        void RemoveProduct(int id); //Usuwa produkt z bazy.
    }
}
