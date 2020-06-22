using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KomShop.Web.Data
{
    public class EfGPUContext : IGPURepository
    {
        public EfDbContext context = new EfDbContext();
        private ProductContext productContext = new ProductContext();
        public IQueryable<Object> GetRepo()
        {
            return context.GPUs;
        }   //Zwraca DbSet kart graficznych.
        public List<string> GetDetailsInfo()
        {
            return new List<string>
        {
            "Układ",
            "Typ pamięci",
            "Wielkość pamięci"
        };
        }   //Wybrane szczegóły

        public List<string> GetDetailsUnits()
        {
            return new List<string>
            {
                "",
                "",
                " GB"
            };
        }   //Jednostki szczegółów.

        public IEnumerable<GPU> GPUs    //Zwraca repozytorium kart graficznych.
        {
            get
            {
                return context.GPUs.Include(x => x.OrderDetails);
            }
        }
        public List<FiltersProperties> GetFilterProperties()    //Ustala filtry dla kategorii.
        {
            List<FiltersProperties> properties = new List<FiltersProperties>();
            properties.Add(new FiltersProperties
            {
                Name = "Producent",
                PropertyName = "Producent",
                Values = context.GPUs.OrderByDescending(x => x.Producent).Select(x => new SelectListItem { Text = x.Producent }).Distinct().ToList()
            });
            properties.Add(new FiltersProperties
            {
                Name = "Układ",
                PropertyName = "Model",
                Values = context.GPUs.OrderByDescending(x => x.Model).Select(x => new SelectListItem { Text = x.Model }).Distinct().ToList()
            });
            properties.Add(new FiltersProperties
            {
                Name = "Typ pamięci",
                PropertyName = "MemoryType",
                Values = context.GPUs.OrderByDescending(x => x.MemoryType).Select(x => new SelectListItem { Text = x.MemoryType }).Distinct().ToList()
            });
            properties.Add(new FiltersProperties
            {
                Name = "Ilość pamięci",
                PropertyName = "Memory",
                Values = context.GPUs.OrderByDescending(x => x.MemoryType).Select(x => new SelectListItem { Text = x.Memory.ToString() + " GB", Value = x.Memory.ToString() }).Distinct().ToList()
            });
            return properties;  //Zwraca listę filtrów.
        }

        public List<SelectListItem> GetSubcategories()  //Generuje podkategorie.
        {
            List<SelectListItem> subcategories = new List<SelectListItem>(); //Nowa lista podkategorii
            foreach (var name in GPUs.Select(x => x.Brand).Distinct())   //Dla każdej firmy
            {
                subcategories.Add(new SelectListItem { Text = "Karty " + name, Value = name }); //Dodaj nową podkategorię.
            }
            return subcategories; //Zwraca listę podkategorii
        }
        public void AddProduct(GPU product)
        {
            product.Product_ID = productContext.items.Count() != 0 ? productContext.items.OrderByDescending(x => x.ProductID).Select(x => x.ProductID).FirstOrDefault() + 1 : 1;

            context.GPUs.Add(product);
            context.SaveChanges();
        }   //Dodaje produkt do bazy danych.
        public void EditProduct(GPU product)
        {
            var EfDbEntry = context.GPUs.FirstOrDefault(x => x.Product_ID == product.Product_ID);
            foreach (var property in EfDbEntry.GetType().GetProperties())
            {
                property.SetValue(EfDbEntry, product.GetType().GetProperty(property.Name).GetValue(product));
            }

            context.SaveChanges();
        }   //Edytuje produkt w bazie danych.
        public void RemoveProduct(int id)
        {
            context.GPUs.Remove(context.GPUs.FirstOrDefault(x => x.Product_ID == id));
            context.SaveChanges();
        }   //Usuwa produkt z bazy danych.
    }
}