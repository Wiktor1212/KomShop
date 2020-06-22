using System.Collections.Generic;
using System.Linq;
using KomShop.Web.Abstract;
using KomShop.Web.Data;

namespace KomShop.Web.Entities
{
    public class Cart
    {
        private List<Product> lineCollection = new List<Product>();
        public IProductRepository productRepository = new ProductContext();

        public void AddItem(int productId, int quantity)    //Dodaje przedmiot do koszyka.
        {
            Product product = productRepository.items.FirstOrDefault(x => x.ProductID == productId); //Wyszukuje produktu w repozytorium.
            Product id = lineCollection.FirstOrDefault(x => x.ProductID == productId);
            if (lineCollection.FirstOrDefault(x => x.ProductID == productId) == null) //Jeżeli nie wyszukało produktu.
            {
                lineCollection.Add(new Product { ProductID = productId, Title = product.Title, Price = product.Price, Quantity = quantity, Category = product.Category });  //Dodaje nowy produkt do koszyka.
            }
            else    //Jeżeli wyszukało produkt.
            {
                lineCollection
                .Where(p => p.ProductID == productId)
                .FirstOrDefault()
                .Quantity += quantity;  //Zwiększa ilość produktu.
            }
        }
        public void RemoveItem(int productId)   //Usuwa produkt.
        {   
            lineCollection.RemoveAll(p => p.ProductID == productId);
        }
        public decimal ComputeTotalValue()  //Oblicza łączną wartość zamówienia.
        {
            return lineCollection.Sum(p => p.Price * p.Quantity);
        }
        public void ChangeQuantity(int product_Id, int quantity)    //Zmienia ilość przedmiotu.
        {
            lineCollection.FirstOrDefault(x => x.ProductID == product_Id).Quantity = quantity;
        }
        public void Clear() //Czyści koszyk.
        {
            lineCollection = new List<Product>();
        }
        public List<Product> Products   //Zwraca produkty w koszyku.
        {
            get { return lineCollection; }
        }
    }
}