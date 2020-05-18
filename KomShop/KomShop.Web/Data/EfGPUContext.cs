using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KomShop.Web.Data
{
    public class EfGPUContext : IGPURepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<GPU> GPUs
        {
            get
            {
                return context.GPUs.Include(x => x.OrderDetails);
            }
        }
    }
}