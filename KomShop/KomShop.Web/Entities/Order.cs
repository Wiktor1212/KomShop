using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KomShop.Web.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }
        public int User_ID { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int Delivery_ID { get; set; }
        public string DeliveryType { get; set; }
        [ForeignKey("ID_User")]
        public User User { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public Delivery Delivery { get; set; }
    }
}