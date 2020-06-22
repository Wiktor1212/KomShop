using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KomShop.Web.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int User_ID { get; set; }
        [DisplayName("Login: ")]
        [Required(ErrorMessage = "Wpisz login")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Wpisz hasło")]
        [DisplayName("Hasło: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Imię")]
        public string Name { get; set; }
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        [DisplayName("Kod pocztowy")]
        public string PostalCode { get; set; }
        [DisplayName("Miejscowość")]
        public string City { get; set; }
        [DisplayName("Telefon")]
        public int? Phone { get; set; }
        public bool? IsAdmin { get; set; }
        [NotMapped]
        public string LoginErrorMessage { get; set; }
        [NotMapped]
        [DisplayName("Potwierdź hasło: ")]
        [Compare("Password", ErrorMessage ="Hasła się nie zgadzają")]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        [DisplayName("Nowe hasło")]
        public string NewPassword { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage ="Musisz zaakceptować regulamin.")]
        [NotMapped]
        public bool Regulamin { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
        [ForeignKey("Order_ID")]
        public ICollection<Order> Orders { get; set; }
    }
}