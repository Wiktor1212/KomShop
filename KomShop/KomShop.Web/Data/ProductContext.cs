using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace KomShop.Web.Data
{
    public class ProductContext : IProductRepository
    {
        public EfDbContext context = new EfDbContext();

        public IEnumerable<Product> items   //Zwraca repozytorium wszystkich produktów.
        {
            get
            {
                List<Product> cpu = TakeProcessors.ToList();
                List<Product> gpu = TakeGPUs.ToList();
                List<Product> products = new List<Product>();
                products.AddRange(cpu);
                products.AddRange(gpu);
                return products;
            }
        }
        public IEnumerable<Product> TakeProcessors  //Zwraca repozutorium procesorów jako lista klas Product.
        {
            get
            {
                return (from x in context.CPUs
                        select new Product
                        {
                            ProductID = x.Product_ID,
                            Title = x.Title,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            FDetail = SqlFunctions.StringConvert(x.Clock, 5, 1),
                            SDetail = SqlFunctions.StringConvert((double)x.Cores),
                            TDetail = SqlFunctions.StringConvert((decimal)x.Cache),
                            OrderDetails = x.OrderDetails,
                            Promoted = x.Promoted,
                            SoldPieces = x.OrderDetails.Count() == 0 ? 0 : x.OrderDetails.Sum(y => y.Quantity),
                            Category = "Procesory"
                        }).ToList();
            }
        }
        public IEnumerable<Product> TakeGPUs    //Zwraca repozutorium procesorów jako lista klas Product.
        {
            get
            {
                return (from x in context.GPUs
                        select new Product
                        {
                            ProductID = x.Product_ID,
                            Title = x.Title,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            FDetail = x.Model,
                            SDetail = x.MemoryType,
                            TDetail = SqlFunctions.StringConvert((decimal)x.Memory),
                            OrderDetails = x.OrderDetails,
                            Promoted = x.Promoted,
                            SoldPieces = x.OrderDetails.Count() == 0 ? 0 : x.OrderDetails.Sum(y => y.Quantity),
                            Category = "Karty graficzne"
                        }).ToList();
            }
        }
        public List<Product> Sort(List<Product> repo, string SortType)   //Sortuje listę produktów.
        {
            switch (SortType)
            {
                case "Cena - malejąco":
                    repo = repo.OrderByDescending(x => x.Price).ToList();
                    break;
                case "Cena - rosnąco":
                    repo = repo.OrderBy(x => x.Price).ToList();
                    break;
                case "Nazwa A-Z":
                    repo = repo.OrderBy(x => x.Title).ToList();
                    break;
                case "Nazwa Z-A":
                    repo = repo.OrderByDescending(x => x.Title).ToList();
                    break;
                case "Popularność - największa":
                    repo = repo.OrderByDescending(x => x.SoldPieces).ToList();
                    break;
                case "Popularność - najmniejsza":
                    repo = repo.OrderBy(x => x.SoldPieces).ToList();
                    break;
                default: break;
            }
            return repo;
        }
        public void SellProduct(OrderDetails details)   //Zmniejsza ilość danego produktu w magazynie
        {
            var productDetails = GetProduct(details.Product_ID);
            productDetails.GetType().GetProperty("Quantity").SetValue(productDetails, (int)productDetails.GetType().GetProperty("Quantity").GetValue(productDetails) - details.Quantity);
            context.SaveChanges();
        }
        public ProductDetailsViewModel GetProductDetails(int id)  //Generuje szczegóły produktu
        {
            var viewModel = new ProductDetailsViewModel();
            var productDetails = items.FirstOrDefault(x => x.ProductID == id);
            switch(productDetails.Category)
            {
                case "Procesory":
                    var processor = (CPU)GetProduct(id);
                    viewModel.Title = processor.Title;
                    viewModel.Price = processor.Price;

                    viewModel.Details.Add(new ProductDetails { DetailName = "Producent", Value = processor.Brand, Units = "" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Socket: ", Value = processor.Socket, Units = "" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Rdzenie: ", Value = processor.Cores.ToString(), Units = " rdzeni" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Wątki: ", Value = processor.Threads.ToString(), Units = " wątków" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Taktowanie: ", Value = processor.Clock.ToString(), Units = " GHz" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Pamięć: ", Value = processor.Cache.ToString(), Units = " MB" });
                    break;
                case "Karty graficzne":
                    var gpu = (GPU)GetProduct(id);
                    viewModel.Title = gpu.Title;
                    viewModel.Price = gpu.Price;

                    viewModel.Details.Add(new ProductDetails { DetailName = "Producent: ", Value = gpu.Producent, Units = "" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Układ: ", Value = gpu.Model, Units = "" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Typ pamięci: ", Value = gpu.MemoryType, Units = "" });
                    viewModel.Details.Add(new ProductDetails { DetailName = "Pamięć: ", Value = gpu.Memory.ToString(), Units = " GB" });
                    break;
                default: break;
            }
            viewModel.Product_ID = id;
            return viewModel;
        }
        public Object GetProduct(int productId) //Zwraca produkt z jego właściwą klasą po jego id.
        {
            var productDetails = items.FirstOrDefault(x => x.ProductID == productId);

            var query = $"{"Product_ID"} == {productId}";

            return GetDbSet(productDetails.Category).Where(query).FirstOrDefault();
        }
        public Object GetEfContext(string category)
        {
            switch (category)
            {
                case "Procesory":
                    return new EfCPUContext();
                case "Karty graficzne":
                    return new EfGPUContext();
                default: return new EfOrderContext();
            }
        }   //Zwraca EfContext dla danej kategorii.
        public IQueryable<Object> GetDbSet(string category) //Zwraca DbSet odpowiedniej kategorii.
        {
            var EfContext = GetEfContext(category);
            return (IQueryable<Object>)EfContext.GetType().GetMethod("GetRepo").Invoke(EfContext, null);
        }
        public void AddPromotion(int productId) //Dodaje produkt do promowanych.
        {
            var productDetails = GetProduct(productId);
            productDetails.GetType().GetProperty("Promoted").SetValue(productDetails, true);
            context.SaveChanges();
        }
        public void RemovePromotion(int productId)  //Usuwa produkt z promowanych.
        {
            var productDetails = GetProduct(productId);
            productDetails.GetType().GetProperty("Promoted").SetValue(productDetails, false);
            context.SaveChanges();
        }
        public List<Product> Filtr(FiltersModel filters, string category)    //Filtruje i zwraca listę produktów.
        {
            string subcategory = (string)HttpContext.Current.Session["subcategory"] ?? null;    //Sprawdza czy podkategoria nie została wybrana.
            IEnumerable<Object> products = GetDbSet(category);  //Pobiera repozytorium

            if (subcategory != null) //Jeśli podkategoria została wybrana
            {
                products = products.Where($"Brand == {subcategory}");
            }
            if (filters.PriceMin != 0)   //Jeśli cena min. została ustalona
            {
                products = products.Where($"Price >= {filters.PriceMin}");
            }
            if (filters.PriceMax != 0)   //Jeśli cena max została ustalona.
            {
                products = products.Where($"Price <= {filters.PriceMax}");
            }
            if (filters.Filters != null)    //Jeśli wybrano dodatkowe filtry.
            {
                List<int> a = filters.Filters.Where<FiltersProperties>(x => x.Value != null).Select<FiltersProperties, int>(x => filters.Filters.IndexOf(x)).ToList();    //Pobiera indexy filtrów, które zostały wybrane.
                foreach (int i in a)
                {
                    products = products.Where($"{filters.Filters[i].PropertyName} == {filters.Filters[i].Value}");  //Filtruje repozytorium
                }
            }

            var Id = (IEnumerable<int>)products.Select($"Product_ID");      //Pobiera ID przefiltrowanych produktów.
            return Sort(items.Where(x => Id.Contains(x.ProductID)).ToList(), HttpContext.Current.Session["sort"]?.ToString());   //Zwraca posortowane repozytorium produktów.
        }
        public List<string> GetDetailsInfo(string category)
        {
            var repo = GetEfContext(category);
            return (List<string>)repo.GetType().GetMethod("GetDetailsInfo").Invoke(repo, null);
        }   //Zwraca listę nazw wybranych szczegółów dla wybranej kategorii.
        public List<string> GetDetailsUnits(string category)
        {
            var EfContext = GetEfContext(category);
            return (List<string>)EfContext.GetType().GetMethod("GetDetailsUnits").Invoke(EfContext, null);
        }   //Zwraca jednostki szczegółów dla wybranej kategorii
        public FiltersAndSubcategoriesModel GetFiltersAndSubcategories(string category) //Zwraca model zawierający filtry i podkategorie dla wybranej kategorii.
        {
            var EfContext = GetEfContext(category);

            return new FiltersAndSubcategoriesModel
            {
                Filters = new FiltersModel {Filters = (List<FiltersProperties>)EfContext.GetType().GetMethod("GetFilterProperties").Invoke(EfContext, null) },
                Subcategories = (List<SelectListItem>)EfContext.GetType().GetMethod("GetSubcategories").Invoke(EfContext, null)
            };
        }
        public void RemoveProduct(string category, int productId)   //Usuwa wybrany produkt z danej kategorii.
        {
            var EfContext = GetEfContext(category);
            EfContext.GetType().GetMethod("RemoveProduct").Invoke(EfContext, new object[] { productId});
        }
        public Object GetNewProduct(string category)    //Generuje nową klasę dla kategorii.
        {
            var type = GetDbSet(category).GetType().GetGenericArguments().Single();
            return Activator.CreateInstance(type);
        }
        public void AddProduct(string category, FormCollection formCollection, HttpPostedFileBase image)   //Dodaje nowy produkt do bazy.
        {
            var product = GetNewProduct(category);
            var EfContext = GetEfContext(category);
            foreach(var property in product.GetType().GetProperties())
            {
                if(property.Name != "ImageData" && property.Name != "ImageMimeType")
                {
                    var NewValue = Convert.ChangeType(formCollection[property.Name], property.PropertyType);
                    property.SetValue(product, NewValue);
                }
                else 
                {
                    if (property.Name == "ImageData")
                    {
                        property.SetValue(product, new byte[image.ContentLength]);
                    }
                    if(property.Name == "ImageMimeType")
                    {
                        property.SetValue(product, image.ContentType);
                    }
                    if(product.GetType().GetProperty("ImageData").GetValue(product) != null && product.GetType().GetProperty("ImageMimeType").GetValue(product) != null)
                    {
                        image.InputStream.Read((byte[])product.GetType().GetProperty("ImageData").GetValue(product), 0, image.ContentLength);
                    }
                }
            }
            EfContext.GetType().GetMethod("AddProduct").Invoke(EfContext, new object[] { product });
        }
        public void EditProduct(string category, FormCollection formCollection, HttpPostedFileBase image) //Edytuje wybrany produkt.
        {
            var product = GetNewProduct(category);
            var EfContext = GetEfContext(category);
            foreach (var property in product.GetType().GetProperties())
            {
                if (property.Name != "ImageData" && property.Name != "ImageMimeType")
                {
                    var NewValue = Convert.ChangeType(formCollection[property.Name], property.PropertyType);
                    property.SetValue(product, NewValue);
                }
                else
                {
                    if (property.Name == "ImageData")
                    {
                        property.SetValue(product, new byte[image.ContentLength]);
                    }
                    if (property.Name == "ImageMimeType")
                    {
                        property.SetValue(product, image.ContentType);
                    }
                    if (product.GetType().GetProperty("ImageData").GetValue(product) != null && product.GetType().GetProperty("ImageMimeType").GetValue(product) != null)
                    {
                        image.InputStream.Read((byte[])product.GetType().GetProperty("ImageData").GetValue(product), 0, image.ContentLength);
                    }
                }
            }
            EfContext.GetType().GetMethod("EditProduct").Invoke(EfContext, new object[] { product });
        }
    }
}