using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KomShop.Web.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string FDetail { get; set; }
        public string SDetail { get; set; }
        public string TDetail { get; set; }
        public bool Promoted { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        [NotMapped]
        public int SoldPieces { get; set; }
        [NotMapped]
        public string Category { get; set; }
    }
}