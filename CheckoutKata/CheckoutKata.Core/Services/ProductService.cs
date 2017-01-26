using System.Collections.Generic;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public class ProductService : IProductService
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return new List<Product>();
        }

        public void UpdateProductPrice(string productSku, decimal price)
        {
            
        }

        public void UpdateProductSpecialPrice(string productSku, int qty, decimal price)
        {
            
        }

        public void AddProduct(Product product)
        {
            
        }

        public void RemoveProduct(string productSku)
        {
            
        }
    }
}