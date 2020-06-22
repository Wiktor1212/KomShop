using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomShop.Web.Entities
{
    [Table("GPU")]
    public class GPU
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
        public string Model { get; set; }
        public string Producent { get; set; }
        [DisplayName("Typ pamięci")]
        [Required]
        public string MemoryType { get; set; }
        [DisplayName("Ilość pamięci")]
        [Required]
        public int Memory { get; set; }
        [DisplayName("Cena")]
        [Required]
        public decimal Price { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool Promoted { get; set; }
        [DisplayName("Ilość")]
        [Required]
        public int Quantity { get; set; }
        [HiddenInput(DisplayValue = false)]
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}