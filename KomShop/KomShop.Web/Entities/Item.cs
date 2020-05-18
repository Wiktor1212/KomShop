using System.Collections.Generic;

namespace KomShop.Web.Entities
{
    public class Item
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string FDetail { get; set; }
        public string SDetail { get; set; }
        public string TDetail { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}