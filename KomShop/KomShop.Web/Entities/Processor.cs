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
    [Table("Processor")]
    public class Processor
    {
        [Key]
        public int Product_ID { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Socket { get; set; }
        public decimal Price { get; set; }
        public bool Promoted { get; set; }
        public int Threads { get; set; }
        public int Quantity { get; set; }
        public double Clock { get; set; }
        public int Cores { get; set; }
        public int Cache { get; set; }
        
        [NotMapped]
        public List<string> DetailsInfo = new List<string>
        {
            "Taktowanie",
            "Liczba rdzeni",
            "Pamięć cache"
        };
        [NotMapped]
        public List<string> Units = new List<string>
        {
            " GHz",
            " rdzeni",
            " MB"
        };
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
public enum Proc
    {
        Intel,
        AMD
    }
}