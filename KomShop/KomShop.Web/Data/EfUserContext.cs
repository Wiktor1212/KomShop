using KomShop.Web.Abstract;
using KomShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace KomShop.Web.Data
{
    public class EfUserContext : IUserRepository
    {
        private EfDbContext context = new EfDbContext();
        public IEnumerable<Users> Users
        {
            get
            {
                return context.Users.Include(x => x.Orders);
            }
        }
        public void Register(Users user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public void ChangePassword(int id, string NewPassword)
        {
            context.Users.FirstOrDefault(x => x.ID_User == id).Password = NewPassword;
            context.Users.FirstOrDefault(x => x.ID_User == id).ConfirmPassword = NewPassword;
            context.SaveChanges();
        }
        public void AddAddressData(Users user)
        {
            var dbEntry = context.Users.FirstOrDefault(x => x.ID_User == user.ID_User);
            dbEntry.ConfirmPassword = dbEntry.Password;
            dbEntry.Name = user.Name;
            dbEntry.Surname = user.Surname;
            dbEntry.Adress = user.Adress;
            dbEntry.PostalCode = user.PostalCode;
            dbEntry.City = user.City;
            dbEntry.Phone = user.Phone;
            context.SaveChanges();
        }
    }
}