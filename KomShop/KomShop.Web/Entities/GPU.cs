using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KomShop.Web.Entities
{
    [Table("GPU")]
    public class GPU
    {
        [Key]
        public int Product_ID { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Producent { get; set; }
        public string MemoryType { get; set; }
        public int Memory { get; set; }
        public decimal Price { get; set; }
        public bool Promoted { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public List<string> DetailsInfo = new List<string>
        {
            "Układ: ",
            "Typ pamięci: ",
            "Wielkość pamięci: "
        };
        [NotMapped]
        public List<string> Units = new List<string>
        {
            "",
            "",
            " GB"
        };
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
    public enum Vid
    {
        Radeon,
        nVidia
    }
}