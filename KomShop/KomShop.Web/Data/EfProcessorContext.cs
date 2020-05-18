using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;

namespace KomShop.Web.Data
{
    public class EfProcessorContext : IProcessorRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Processor> Processors
        {
            get
            {
                return context.Processors.Include(x => x.OrderDetails);
            }
        }
    }
}