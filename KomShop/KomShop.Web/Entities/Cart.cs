using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Reflection;
using System.Web;
using Castle.Components.DictionaryAdapter.Xml;
using KomShop.Web.Abstract;
using KomShop.Web.Entities;

namespace KomShop.Web.Entities
{
    public class Cart
    {
        private CartLine lineCollection = new CartLine();

        public void AddItem(int productId, int quantity, decimal price, string title=null)
        {
            Item product = lineCollection.items.Where(p => p.ProductID == productId).FirstOrDefault();
            if(product == null)
            {
                lineCollection.items.Add(new Item { ProductID = productId, Title = title, Price = price, Quantity = quantity });
            }
            else
            {
                lineCollection.items
                .Where(p => p.ProductID == productId)
                .FirstOrDefault()
                .Quantity += quantity;
            }
        }
        public void RemoveItem(int productId)
        {   
            lineCollection.items.RemoveAll(p => p.ProductID == productId);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.items.Sum(p => p.Price * p.Quantity);
        }
        public void Clear()
        {
            lineCollection = new CartLine();
        }
        public CartLine Lines
        {
            get { return lineCollection; }
        }
    }
    public class CartLine
    {
        public List<Item> items = new List<Item>();
    }
}