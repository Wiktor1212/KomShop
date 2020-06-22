using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace KomShop.Web.Entities
{
    [Table("Delivery")]
    public class Delivery
    {
        [Key]
        public int Delivery_ID { get; set; }
        public int Order_ID { get; set; }
        [Required(ErrorMessage ="Wpisz swoje imię.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Wpisz swoje nazwisko.")]
        public string Surname { get; set; }
        [Required(ErrorMessage ="Wpisz swój adres.")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Wpisz swój kod pocztowy.")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage ="Wpisz miejscowość.")]
        public string City { get; set; }
        [Required(ErrorMessage ="Podaj swój numer telefonu.")]
        public int? Phone { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}