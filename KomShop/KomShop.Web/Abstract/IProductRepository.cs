using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> items { get; } //Zwraca listę wszystich produktów.
        IEnumerable<Product> TakeProcessors { get; }    //Zwraca listę procesorów jako klasa Product.
        IEnumerable<Product> TakeGPUs { get; }  //Zwraca listę kart graficznych jako klasa Product.
        List<Product> Sort(List<Product> repo, string SortType); //Sortuje listę produktów.
        void SellProduct(OrderDetails details); //Odejmuje odpowiednią ilość produktu z magazynu.
        ProductDetailsViewModel GetProductDetails(int id);    //Zwraca szczegóły produktu.
        void AddPromotion(int productId);   //Dodaje produkt do listy promowanych.
        void RemovePromotion(int productId);    //Usuwa produkt z listy promowanych.
        List<Product> Filtr(FiltersModel filters, string category); //Zwraca przefiltrowaną listę produktów dla danej kategorii.
        List<string> GetDetailsInfo(string category);   //Pobiera nazwy szczególów dla kategorii.
        List<string> GetDetailsUnits(string category);  //Pobiera odpowiednie jednostki szczegółów dla kategorii.
        FiltersAndSubcategoriesModel GetFiltersAndSubcategories(string category);   //Zwraca model zawierający filtry i podkategorie dla wybranej kategorii.
        void RemoveProduct(string category, int productId);   //Usuwa wybrany produkt z danej kategorii.
        Object GetNewProduct(string category);    //Generuje nową klasę dla kategorii.
        Object GetProduct(int productId); //Zwraca produkt z jego właściwą klasą po jego id.
        void AddProduct(string category, FormCollection formCollection, HttpPostedFileBase image);   //Dodaje nowy produkt do bazy.
        void EditProduct(string category, FormCollection formCollection, HttpPostedFileBase image); //Edytuje wybrany produkt.
    }
}
