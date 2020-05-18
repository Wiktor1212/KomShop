using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KomShop.Web.Entities
{
    public class OrderDetails
    {
        [Key]
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        [ForeignKey(nameof(Product_ID))]
        public Processor Processor { get; set; }
        [ForeignKey(nameof(Product_ID))]
        public GPU GPU { get; set; }
        [ForeignKey(nameof(Order_ID))]
        public Orders Order { get; set; }
    }
}