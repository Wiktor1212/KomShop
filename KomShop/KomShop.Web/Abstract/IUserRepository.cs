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
        IEnumerable<User> Users { get; }   //Zwraca repozytorium użytkowników.
        void Register(User user);  //Dodaje nowego użytkownika.
        void ChangePassword(int id, string NewPassword);    //Zmienia hasło użytkownika.
        void AddAddressData(User user);    //Dodaje dane adresowe.
    }
}
