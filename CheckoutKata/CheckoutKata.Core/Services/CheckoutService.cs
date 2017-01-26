using System.Collections.Generic;
using CheckoutKata.Core.Database;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        #region Constructor

        private IRepository<Product> _repository;

        public CheckoutService()
        {
            IRepositoryFactory repositoryFactory = new CsvRepositoryFactory();
            _repository = repositoryFactory.Create<Product>();
        }

        #endregion Constructor

        #region Scan

        public Product Scan(string productSku)
        {
            var product = _repository.GetData(productSku);

            return product;
        }

        #endregion Scan

        #region GetTotalPrice

        public decimal GetTotalPrice(Dictionary<Product, int> productsQuantities)
        {
            var price = 0m;

            foreach (var product in productsQuantities.Keys)
            {
                if (product.SpecialQty > 0 && productsQuantities[product] >= product.SpecialQty)
                {
                    price = price + (productsQuantities[product] / product.SpecialQty) * product.SpecialPrice +
                            (productsQuantities[product] % product.SpecialQty) * product.UnitPrice;

                    continue;
                }

                price = price + product.UnitPrice * productsQuantities[product];
            }

            return price;
        }

        #endregion GetTotalPrice
    }
}