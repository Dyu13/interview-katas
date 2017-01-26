using System.Collections.Generic;
using CheckoutKata.Core.Database;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Services
{
    public class ProductService : IProductService
    {
        #region Constructor

        private IRepository<Product> _repository;

        public ProductService()
        {
            IRepositoryFactory repositoryFactory = new CsvRepositoryFactory();
            _repository = repositoryFactory.Create<Product>();
        }

        #endregion Constructor

        #region GetAllProducts

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _repository.GetDataList();

            return products;
        }

        #endregion GetAllProducts

        #region UpdateProductPrice

        public void UpdateProductPrice(string productSku, decimal price)
        {
            var product = _repository.GetData(productSku);

            if(product == null || product.UnitPrice == price) return;

            product.UnitPrice = price;

            _repository.Update(product);
        }

        #endregion UpdateProductPrice

        #region UpdateProductSpecialPrice

        public void UpdateProductSpecialPrice(string productSku, int qty, decimal price)
        {
            var product = _repository.GetData(productSku);

            if (product == null || (product.SpecialQty == qty && product.SpecialPrice == price)) return;

            product.SpecialQty = qty;
            product.SpecialPrice = price;

            _repository.Update(product);
        }

        #endregion UpdateProductSpecialPrice

        #region AddProduct

        public void AddProduct(Product product)
        {
            _repository.Insert(product);
        }

        #endregion AddProduct

        #region RemoveProduct

        public void RemoveProduct(string productSku)
        {
            var product = _repository.GetData(productSku);

            if(product == null) return;

            _repository.Delete(product);
        }

        #endregion RemoveProduct
    }
}