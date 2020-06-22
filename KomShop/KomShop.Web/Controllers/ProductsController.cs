using KomShop.Web.Abstract;
using KomShop.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KomShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IProcessorRepository ProcRepository;    //repozytorium procesorów.
        private IGPURepository GPURepository;   //Repozytorium kart graficznych.
        private IProductRepository productRepository;   //Repozytorium produktów.
        private ProductDetailsViewModel productDetails { get; set; }    //Szczegóły produktu.

        public ProductsController(IProcessorRepository processorRepository,
                                  IGPURepository gpuRepository,
                                  IProductRepository prodRepository)
        {
            ProcRepository = processorRepository;
            GPURepository = gpuRepository;
            productRepository = prodRepository;
        }
      
        public ViewResult ProductDetails(int product_Id)   //Wyświetl stronę ze szczegółami produktu.
        {
            AddLatest(product_Id);  //Dodaj jako ostatnio przeglądany produkt.
            productDetails = productRepository.GetProductDetails(product_Id);    //Przypisz szczegóły zamówienia wywołując akcję.
            return View(productDetails);    //Wygeneruj widok z przekazaniem modelu.
        }
        public FileContentResult GetImage(int productId)    //Wyświetla zdjęcie produktu.
        {
            object product = productRepository.GetProduct(productId);
            if (product.GetType().GetProperty("ImageData").GetValue(product) != null)
                return File((byte[])product.GetType().GetProperty("ImageData").GetValue(product), (string)product.GetType().GetProperty("ImageMimeType").GetValue(product));
            else
                return null;
        }
        public void AddLatest(int product_Id)   //Dodaj ostatnio przeglądany produkt.
        {
            List<int?> latest = new List<int?>();   //Nowa lista z ID produktów.
            if (Session["Latest"] != null)  //Jeżeli dane sesji istnieją.
            {
                latest = (List<int?>)Session["Latest"]; //Przypisz dane z danych sesji.
                latest.Remove(latest.FirstOrDefault(x => x == product_Id)); //Usuwa przedmiot jeśli był w liście.
            }
            if (latest.Count() == 5)    //Jeżeli w liście jest 5 produktów.
            {
                latest.RemoveAt(0); //Usuwa ostatni przeglądany produkt.
                latest.Add(product_Id); //Dodaje nowy produkt do listy.
            }
            else    //Jeżeli w liście nie ma 5 produktów.
            {
                latest.Add(product_Id); //Dodaje nowy produkt do listy.
            }
            Session["Latest"] = latest; //Zapisuje listę.
        }
        public ViewResult ShowProducts(string category, string section)   //Generuje stronę dla kategorii.
        {
            Session["sort"] = null;       //Usuwa dane sesji.
            Session["subcategory"] = null;  //dla typu sortowania i podkategorii.

            ViewBag.Section = section;      //Ustalanie danych Viewbag
            ViewBag.Category = category;    //dla dalszego działania.

            return View();
        }
        public void SignSubcategory(string subcategory)    //Przypisuje podkategorię.
        {
            Session["subcategory"] = subcategory;   //Przypisuje podkategorię do danych sesji.
        }
        public void SignSorting(string sorting) //Przypisuje wybrany typ sortowania.
        {
            Session["sort"] = sorting;
        }
        public PartialViewResult Filtr(string category, FiltersModel filters = null)    //Filtruje i wyświetla listę produktów.
        {
            ShowItemModel model = new ShowItemModel();  //Nowy model widoku.
            filters = filters ?? new FiltersModel();    //Użyte filtry.

            model.Product = productRepository.Filtr(filters, category); //Pobiera repozytorium produktów dla odpowiedniej kategorii i je filtruje.
            model.DetailsInfo = productRepository.GetDetailsInfo(category); //Pobiera nazwy wybranych szczegółów dla danej kategorii.
            model.Units = productRepository.GetDetailsUnits(category);  //Pobiera jednostki do szczegółów.

            return PartialView("ProductPartial", model);    //Wygenerowanie widoku z przekazaniem modelu.
        }
        public PartialViewResult FiltresAndSubcategories(string category)    //Generuje częściowy widok z dostępnymi filtrami i podkategoriami.
        {
            return PartialView("_PartialFilters", productRepository.GetFiltersAndSubcategories(category));   //Wygenerowanie częściowego widoku z przekazaniem modelu.
        }
        public JsonResult GetTitles(string term)    //Zwraca tytuły produktów
        {
            List<string> titles = productRepository.items.Where(x => x.Title.ToLower().Contains(term.ToLower())).Select(y => y.Title).ToList();

            return Json(titles, JsonRequestBehavior.AllowGet);
        }
        public RedirectToRouteResult SearchProduct(string searchTerm)
        {
            var id = productRepository.items.FirstOrDefault(x => x.Title == searchTerm).ProductID;
            return RedirectToAction("ProductDetails", new { product_Id = id });
        }
    }
}