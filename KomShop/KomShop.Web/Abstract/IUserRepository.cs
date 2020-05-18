using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomShop.Web.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<Users> Users { get; }
        void Register(Users user);
        void ChangePassword(int id, string NewPassword);
        void AddAddressData(Users user);
    }
}
