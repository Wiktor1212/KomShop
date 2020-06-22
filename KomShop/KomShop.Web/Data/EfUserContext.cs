using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace KomShop.Web.Data
{
    public class EfUserContext : IUserRepository
    {
        public EfDbContext context = new EfDbContext();
        public EfUserContext(EfDbContext efDb)
        {
            context = efDb;
        }
        public IEnumerable<User> Users //Zwraca repozytorium użytkowników.
        {
            get
            {
                return context.Users.Include(x => x.Orders);
            }
        }
        public void Register(User user)    //Dodaje nowego użytkownika do bazy.
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public void ChangePassword(int id, string NewPassword)  //Zmienia hasło użytkownika.
        {
            context.Users.FirstOrDefault(x => x.User_ID == id).Password = NewPassword;
            context.Users.FirstOrDefault(x => x.User_ID == id).ConfirmPassword = NewPassword;
            context.SaveChanges();
        }
        public void AddAddressData(User user)  //Dodaje dane adresowe do użytkownika.
        {
            var dbEntry = context.Users.FirstOrDefault(x => x.User_ID == user.User_ID);
            dbEntry.ConfirmPassword = dbEntry.Password;
            dbEntry.Name = user.Name;
            dbEntry.Surname = user.Surname;
            dbEntry.Address = user.Address;
            dbEntry.PostalCode = user.PostalCode;
            dbEntry.City = user.City;
            dbEntry.Phone = user.Phone;
            dbEntry.Regulamin = true;
            context.SaveChanges();
        }
    }
}