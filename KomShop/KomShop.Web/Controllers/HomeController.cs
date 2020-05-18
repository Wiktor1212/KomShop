using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Models;

namespace KomShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProcessorRepository ProcRepository;
        private IGPURepository GPURepository;
        private IPODRepository PODRepository;
        public HomeController(IProcessorRepository processorRepository,                                  IGPURepository gpuRepository,
                              IPODRepository podRepository)
        {
            ProcRepository = processorRepository;
            GPURepository = gpuRepository;
            PODRepository = podRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            HomeModel model = new HomeModel
            {
                PromotedThings = GetPromoted(),
                PoDs = PODRepository.PoDs
            };
            return View(model);
        }
        
        public IEnumerable<PromotedThings> GetPromoted()
        {
            IEnumerable<Processor> processors = ProcRepository.Processors.Where(x => x.Promoted == true);
            IEnumerable<GPU> gpus = GPURepository.GPUs.Where(x => x.Promoted == true);

            List<PromotedThings> model = new List<PromotedThings>();

            foreach (var item in processors)
            {
                model.Add(new PromotedThings { Brand = item.Brand, Model = item.Model, Price = item.Price });
            }
            foreach (var item in gpus)
            {
                model.Add(new PromotedThings { Brand = item.Producent, Model = item.Model, Price = item.Price });
            }

            return model;
        }
    }
}