using System.Collections.Generic;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public class ProductService : IProductService
    {
        public IEnumerable<Product> GetAllProducts()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProductPrice(string productSku, decimal price)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProductSpecialPrice(string productSku, int qty, decimal price)
        {
            throw new System.NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveProduct(string productSku)
        {
            throw new System.NotImplementedException();
        }
    }
}