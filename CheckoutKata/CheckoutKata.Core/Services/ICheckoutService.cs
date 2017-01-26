using System.Collections.Generic;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public interface ICheckoutService
    {
        void Scan(string productSku);

        decimal GetTotalPrice(Dictionary<Product, int> productsQuantities);
    }
}