using System.Collections.Generic;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();

        void UpdateProductPrice(string productSku, decimal price);

        void UpdateProductSpecialPrice(string productSku, int qty, decimal price);

        void AddProduct(Product product);

        void RemoveProduct(string productSku);
    }
}