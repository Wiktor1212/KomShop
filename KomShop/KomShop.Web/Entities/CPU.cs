using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace KomShop.Web.Entities
{
    [Table("CPU")]
    public class CPU
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Product_ID { get; set; }
        [DisplayName("Tytuł")]
        [Required]
        public string Title { get; set; }
        [DisplayName("Marka")]
        [Required]
        public string Brand { get; set; }
        [DisplayName("Model")]
        [Required]
        public string Model { get; set; }
        [Required]
        public string Socket { get; set; }
        [DisplayName("Cena")]
        [Required]
        public decimal Price { get; set; }
        [HiddenInput(DisplayValue =false)]
        public bool Promoted { get; set; }
        [DisplayName("Liczba wątków")]
        [Required]
        public int Threads { get; set; }
        [DisplayName("Ilość")]
        [Required]
        public int Quantity { get; set; }
        [DisplayName("Taktowanie")]
        [Required]
        public double Clock { get; set; }
        [DisplayName("Rdzenie")]
        [Required]
        public int Cores { get; set; }
        [DisplayName("Pamięć cache")]
        [Required]
        public int Cache { get; set; }
        [HiddenInput(DisplayValue = false)]
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}