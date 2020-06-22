using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using Moq;
using Ninject.Activation;

namespace KomShop.Web.Data
{
    public class EfCPUContext : IProcessorRepository
    {
        public EfDbContext context = new EfDbContext();
        
        public ProductContext productContext = new ProductContext();

        public IQueryable<Object> GetRepo() //Zwraca DbSet procesorów.
        {
            return context.CPUs;
        }
        public List<string> GetDetailsInfo()
        {
            return new List<string>
        {
            "Taktowanie",
            "Liczba rdzeni",
            "Pamięć cache"
        };
        }   //Wybrane szczegóły

        public List<string> GetDetailsUnits()
        {
            return new List<string>
            {
                " GHz",
                " rdzeni",
                " MB"
            };
        }   //Jednostki szczegółów.
        public IEnumerable<CPU> Processors    //Zwraca repozytorium procesorów
        {
            get
            {
                return context.CPUs.Include(x => x.OrderDetails);
            }
        }
        public List<FiltersProperties> GetFilterProperties()    //Ustala filtry dla kategorii.
        {
            List<FiltersProperties> properties = new List<FiltersProperties>();
            properties.Add(new FiltersProperties
            {
                Name = "Socket",
                PropertyName = "Socket",
                Values = context.CPUs.Select(x => new SelectListItem { Text = x.Socket }).Distinct().ToList()
            });
            properties.Add(new FiltersProperties
            {
                Name = "Rdzenie",
                PropertyName = "Cores",
                Values = context.CPUs.Select(x => new SelectListItem { Text = x.Cores.ToString()}).Distinct().ToList()
            });
            properties.Add(new FiltersProperties
            {
                Name = "Cache",
                PropertyName = "Cache",
                Values = context.CPUs.Select(x => new SelectListItem { Text = x.Cache.ToString() + "MB", Value = x.Cache.ToString() }).Distinct().ToList()
            });
            return properties;  //Zwraca listę filtrów.
        }
        
        public List<SelectListItem> GetSubcategories()  //Generuje podkategorie.
        {
            List<SelectListItem> subcategories = new List<SelectListItem>(); //Nowa lista podkategorii
            foreach (var name in Processors.Select(x => x.Brand).Distinct())   //Dla każdej firmy
            {
                subcategories.Add(new SelectListItem { Text = "Procesory " + name, Value = name }); //Dodaj nową podkategorię.
            }
            return subcategories; //Zwraca listę podkategorii
        }
        public void AddProduct(CPU product)
        {
            product.Product_ID = productContext.items.Count() != 0 ? productContext.items.OrderByDescending(x => x.ProductID).Select(x => x.ProductID).FirstOrDefault() + 1 : 1;
            context.CPUs.Add(product);
            context.SaveChanges();
        }   //Dodaje produkt do bazy danych.
        public void EditProduct(CPU product)
        {
            var EfDbEntry = context.CPUs.FirstOrDefault(x => x.Product_ID == product.Product_ID);
            foreach(var property in EfDbEntry.GetType().GetProperties())
            {
                property.SetValue(EfDbEntry, product.GetType().GetProperty(property.Name).GetValue(product));
            }
            context.SaveChanges();
        }   //Edytuje produkt w bazie danych.
        public void RemoveProduct(int id)
        {
            context.CPUs.Remove(context.CPUs.FirstOrDefault(x => x.Product_ID == id));
            context.SaveChanges();
        }   //Usuwa produkt z bazy danych.
    }
}