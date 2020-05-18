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
            kernel.Bind<IProcessorRepository>().To<EfProcessorContext>();
            kernel.Bind<IGPURepository>().To<EfGPUContext>();
            kernel.Bind<IUserRepository>().To<EfUserContext>();
            kernel.Bind<IProductRepository>().To<EfProductContext>();
            kernel.Bind<IOrderDetailsRepository>().To<EfOrderDetailsContext>();
            Mock<IPODRepository> mock = new Mock<IPODRepository>();
            mock.Setup(x => x.PoDs).Returns(new List<PoD>
            {
                new PoD{ Brand = "Asus", Model = "RTX 2080 SUPER", Description = "Zgarnij tę kartę w promocyjnej cenie już teraz!", DurationTime=3600, Price= 3190.00m},
                new PoD{ Brand = "MSI", Model = "Z390 GAMING PLUS", Description = "Teraz możesz poczuć prawdziwą moc w niskiej cenie!", DurationTime=3600, Price= 590.00m}
            });

            kernel.Bind<IPODRepository>().ToConstant(mock.Object);
        }
    }
}