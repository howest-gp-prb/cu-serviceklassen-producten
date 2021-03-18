using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Prb.Serviceklassen.Core
{
    public class ProductService
    {
        public List<Product> Products;
        public decimal StockValueEuro
        {
            get
            {
                decimal stockValue = 0m;
                foreach(Product product in Products)
                {
                    stockValue += product.PriceEuro * product.Stock;
                }
                return stockValue;
            }
        }
        public decimal StockValueDollar
        {
            get
            {
                decimal stockValue = 0m;
                foreach (Product product in Products)
                {
                    stockValue += product.PriceDollar * product.Stock;
                }
                return stockValue;
            }
        }
        public ProductService()
        {
            Products = new List<Product>();
            DoSeeding();
            Sort();
        }
        private void DoSeeding()
        {
            Products = new List<Product>();
            Products.Add(new Product("A001", "TWIX", PackingUnits.Dozen, 512, 15.45M));
            Products.Add(new Product("A002", "MARS", PackingUnits.Dozen, 488, 16.15M));
            Products.Add(new Product("A003", "BOUNTY", PackingUnits.Dozen, 915, 16M));
            Products.Add(new Product("A004", "SNICKER", PackingUnits.Dozen, 17, 15.45M));
            Products.Add(new Product("A005", "MILKY WAY", PackingUnits.Dozen, 88, 9.99M));
            Products.Add(new Product("B001", "COCA COLA", PackingUnits.Gross, 97, 33.14M));
            Products.Add(new Product("B002", "COCA COLA LIGHT", PackingUnits.Gross, 115, 33.14M));
            Products.Add(new Product("B003", "ICE TEA", PackingUnits.Gross, 9, 29.99M));
        }
        private void Sort()
        {
            Products = Products.OrderBy(p => p.Code).ToList();
        }
        public void AddProduct(Product product)
        {
            Products.Add(product);
            Sort();
        }
        public void DeleteProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}
