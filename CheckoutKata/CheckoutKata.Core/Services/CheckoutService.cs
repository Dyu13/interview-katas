using System.Collections.Generic;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        public void Scan(string productSku)
        {
            throw new System.NotImplementedException();
        }

        public decimal GetTotalPrice(Dictionary<Product, int> productsQuantities)
        {
            throw new System.NotImplementedException();
        }
    }
}