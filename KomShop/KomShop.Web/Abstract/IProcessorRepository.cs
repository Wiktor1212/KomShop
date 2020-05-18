using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KomShop.Web.Entities;
namespace KomShop.Web.Abstract
{
    public interface IProcessorRepository
    {
        IEnumerable<Processor> Processors { get; }
    }
}
