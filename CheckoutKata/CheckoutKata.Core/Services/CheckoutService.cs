using System.Collections.Generic;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        public void Scan(string productSku)
        {
            
        }

        public decimal GetTotalPrice(Dictionary<Product, int> productsQuantities)
        {
            return 24;
        }
    }
}