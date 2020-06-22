using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using KomShop.Web.Data;
using KomShop.Web.Models;
using System.Web.UI.WebControls;

namespace KomShop.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        public void AddBindings()
        {
            //Tu umieść powiązania
            kernel.Bind<IProcessorRepository>().To<EfCPUContext>();
            kernel.Bind<IGPURepository>().To<EfGPUContext>();
            kernel.Bind<IUserRepository>().To<EfUserContext>();
            kernel.Bind<IProductRepository>().To<ProductContext>();
            kernel.Bind<IOrderDetailsRepository>().To<EfOrderDetailsContext>();
            kernel.Bind<IOrdersRepository>().To<EfOrderContext>();
            kernel.Bind<IDeliveryRepository>().To<EfDeliveryContext>();
        }
    }
}