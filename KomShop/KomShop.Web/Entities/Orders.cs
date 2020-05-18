using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KomShop.Web.Entities
{
    public class Orders
    {
        [Key]
        public int Order_ID { get; set; }
        public int ID_User { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        [ForeignKey("ID_User")]
        public Users User { get; set; }
    }
}