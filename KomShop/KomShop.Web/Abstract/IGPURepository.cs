using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KomShop.Web.Abstract
{
    public interface IGPURepository
    {
        IEnumerable<GPU> GPUs { get; }  //Zwraca repozytorium kart graficznych.
        List<FiltersProperties> GetFilterProperties();  //Zwraca filtry.
        List<SelectListItem> GetSubcategories();    //Zwraca podkategorie.
        void AddProduct(GPU product);   //Dodaje produkt do bazy danych.
        void EditProduct(GPU product);   //Zastępuje stary produkt nowym, zedytowanym.
        void RemoveProduct(int id); //Usuwa produkt z bazy.
    }
}
