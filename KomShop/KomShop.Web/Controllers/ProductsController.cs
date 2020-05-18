using Castle.Core.Internal;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KomShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IProcessorRepository ProcRepository;
        private IGPURepository GPURepository;
        private IProductRepository productRepository;
        private List<SubCategoriesModel> SubModel;
        private ShowItemModel ItemsModel = new ShowItemModel();
        private List<string> detailsInfo = new List<string>();
        private ProductDetailsViewModel productDetails = new ProductDetailsViewModel();

        public ProductsController(IProcessorRepository processorRepository,
                                  IGPURepository gpuRepository,
                                  IProductRepository prodRepository)
        {
            ProcRepository = processorRepository;
            GPURepository = gpuRepository;
            productRepository = prodRepository;
        }
      
        public ViewResult ProductDetails(string category, int product_Id)
        {
            switch(category)
            {
                case "Procesory":
                    Processor processor = new Processor();
                    productDetails = new ProductDetailsViewModel
                    {
                        Item = productRepository.TakeProcessors.FirstOrDefault(x => x.ProductID == product_Id),
                        properties = ProcRepository.Processors.FirstOrDefault(x => x.Product_ID == product_Id).GetType().GetProperties().ToList(),
                        DetailsInfo = processor.DetailsInfo,
                        Units = processor.Units
                    };
                    return View(productDetails);
                case "Karty graficzne":
                    productDetails = new ProductDetailsViewModel
                    {
                        Item = productRepository.TakeGPUs.FirstOrDefault(x => x.ProductID == product_Id),
                        properties = GPURepository.GPUs.FirstOrDefault(x => x.Product_ID == product_Id).GetType().GetProperties().ToList()
                    };
                    return View(productDetails);
                default:
                    return View();
            }
        }
        public ActionResult ShowProducts(string category)
        {
            Session["SortType"] = "none";
            Session["subcategory"] = "none";
            switch (category)
            {
                case "Procesory":
                    ViewBag.Section = "Podzespoły komputerowe";
                    ViewBag.Category = "Procesory";
                    ViewBag.Filter = "FiltrProcesory";
                    return View();
                case "Karty graficzne":
                    ViewBag.Section = "Podzespoły komputerowe";
                    ViewBag.Category = "Karty graficzne";
                    ViewBag.Filter = "FiltrGPU";
                    return View();
                default:
                    return View();
            }
        }
        public PartialViewResult FiltrProcesory(string subcategory = null, string socket = null, int? pricemin = null, int? pricemax = null, int? cores = null, int? cachemin = null, int? cachemax = null, string SortType = null)
        {
            subcategory = subcategory != null ? subcategory : Session["subcategory"].ToString();
            SortType = SortType != null ? SortType : Session["SortType"].ToString();
            IEnumerable<Processor> cpu = ProcRepository.Processors;
            if(subcategory != "none")
            {
                cpu = cpu.Where(p => p.Brand.Contains(subcategory));
                ViewBag.SubCategory = subcategory;
                Session["subcategory"] = subcategory;
            }
            if(socket != null)
            {
                cpu = cpu.Where(p => p.Socket.Contains(socket));
            }
            if(pricemin != null)
            {
                cpu = cpu.Where(p => p.Price >= pricemin);
            }
            if(pricemax != null)
            {
                cpu = cpu.Where(p => p.Price <= pricemax);
            }
            if(cores != null)
            {
                cpu = cpu.Where(p => p.Cores == cores);
            }
            if(cachemin != null)
            {
                cpu = cpu.Where(p => p.Cache >= cachemin);
            }
            if(cachemax != null)
            {
                cpu = cpu.Where(p => p.Cache <= cachemax);
            }
            Processor processor = new Processor();
            ItemsModel = new ShowItemModel
            {
                Product = productRepository.TakeProcessors.Where(p => cpu.Select(x => x.Product_ID).Contains(p.ProductID)).ToList(),
                DetailsInfo = processor.DetailsInfo,
                Units = processor.Units
            };
            ViewBag.Category = "Procesory";
            if (SortType != "none")
            {
                ItemsModel.Product = productRepository.Sort(ItemsModel.Product, SortType);
                Session["SortType"] = SortType;
            }
            return PartialView("ProductPartial", ItemsModel);
        }
        public PartialViewResult FiltrGPU(string subcategory = null, string producent = null, string model = null, string memorytype = null, int? memorysize = null, int? pricemin = null, int? pricemax = null, string SortType = null)
        {
            subcategory = subcategory != null ? subcategory : Session["subcategory"].ToString();
            SortType = SortType != null ? SortType : Session["SortType"].ToString();
            IEnumerable<GPU> gpu = GPURepository.GPUs;
            if(subcategory != "none")
            {
                gpu = gpu.Where(p => p.Brand.Contains(subcategory));
                ViewBag.SubCategory = subcategory;
                Session["subcategory"] = subcategory;
            }
            if(producent != null)
            {
                gpu = gpu.Where(p => p.Producent.Contains(producent));
            }
            if(model != null)
            {
                gpu = gpu.Where(p => p.Model.Contains(model));
            }
            if(memorytype != null)
            {
                gpu = gpu.Where(p => p.MemoryType.Contains(memorytype));
            }
            if(memorysize != null)
            {
                gpu = gpu.Where(p => p.Memory == memorysize);
            }
            if (pricemin != null)
            {
                gpu = gpu.Where(p => p.Price >= pricemin);
            }
            if (pricemax != null)
            {
                gpu = gpu.Where(p => p.Price <= pricemax);
            }
            GPU nowy = new GPU();
            ItemsModel = new ShowItemModel
            {
                Product = productRepository.TakeGPUs.Where(p => gpu.Select(x => x.Product_ID).Contains(p.ProductID)).ToList(),
                DetailsInfo = nowy.DetailsInfo,
                Units = nowy.Units
            };
            if(SortType != null)
            {
                ItemsModel.Product = productRepository.Sort(ItemsModel.Product, SortType);
                Session["SortType"] = SortType;
            }
            ViewBag.Category = "Karty graficzne";
            return PartialView("ProductPartial", ItemsModel);
        }
        public PartialViewResult Filtres(string category = null)
        {
            switch (category)
            {
                case "Procesory":
                    return PartialView("~/Views/Filters/ProcesoryFilter.cshtml", new CPU());
                case "Karty graficzne":
                    return PartialView("~/Views/Filters/GPUFilter.cshtml", new GPUModel());
                default:
                    return PartialView("~/Views/Filters/ProcesoryFilter.cshtml", new CPU());
            }
        }
        
        public PartialViewResult SubCategories(string category, string subcategory = null)
        {
            switch(category)
            {
                case "Procesory":
                    SubModel = new List<SubCategoriesModel>();
                    foreach(var item in (Proc[])Enum.GetValues(typeof(Proc)))
                    {
                        SubModel.Add(new SubCategoriesModel { ThingName = string.Format("Procesory {0}", item.ToString()), Thing = item.ToString() });
                    }
                    ViewBag.Category = "Procesory";
                    ViewBag.Section = "Podzespoły komputerowe";
                    ViewBag.SubCategory = subcategory ?? null;
                    ViewBag.Filtr = "FiltrProcesory";
                    return PartialView(SubModel);
                case "Karty graficzne":
                    SubModel = new List<SubCategoriesModel>();
                    foreach(var item in (Vid[])Enum.GetValues(typeof(Vid)))
                    {
                        SubModel.Add(new SubCategoriesModel { ThingName = string.Format("Karty {0}", item.ToString()), Thing = item.ToString() });
                    }
                    ViewBag.Category = "Karty graficzne";
                    ViewBag.Section = "Podzespoły komputerowe";
                    ViewBag.SubCategory = subcategory ?? null;
                    ViewBag.Filtr = "FiltrGPU";
                    return PartialView(SubModel);
                default:
                    return PartialView();
            }
        }
    }
}