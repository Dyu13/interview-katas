using System.Linq;
using CheckoutKata.Core.Database;
using CheckoutKata.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKataUnitTest.DatabaseTests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void ShouldGetCsvRepository()
        {
            // Arrange
            IRepositoryFactory csvRepositoryFactory = new CsvRepositoryFactory();

            // Act
            var csvRepository = csvRepositoryFactory.Create<ProductCsvRepository<Product>>();

            // Assert
            Assert.IsNotNull(csvRepository);
        }

        [TestMethod]
        public void ShouldGetCsvRepositoryDataList()
        {
            // Arrange
            IRepository<Product> csvRepository = new ProductCsvRepository<Product>();
            const string productSku = "A";

            // Act
            var productList = csvRepository.GetDataList().ToList();
            
            // Assert
            Assert.IsNotNull(productList);
            Assert.IsTrue(productList[0].Sku == productSku);
        }

        [TestMethod]
        public void ShouldGetCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new ProductCsvRepository<Product>();
            const string productSku = "A";

            // Act
            var product = csvRepository.GetData(productSku);

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Sku == productSku);
        }

        [TestMethod]
        public void ShouldInsertCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new ProductCsvRepository<Product>();
            const string productSku = "X";

            // Act
            csvRepository.Insert(new Product {Sku = productSku });
            var product = csvRepository.GetData(productSku);

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Sku == productSku);
        }

        [TestMethod]
        public void ShouldUpdateCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new ProductCsvRepository<Product>();
            const string productSku = "A";
            const decimal productUnitPrice = 22.48m;

            // Act
            csvRepository.Update(new Product { Sku = productSku, UnitPrice = productUnitPrice });
            var product = csvRepository.GetData(productSku);

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.UnitPrice == productUnitPrice);
        }

        [TestMethod]
        public void ShouldRemoveCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new ProductCsvRepository<Product>();
            const string productSku = "A";

            // Act
            csvRepository.Delete(new Product { Sku = productSku });
            var product = csvRepository.GetData(productSku);

            // Assert
            Assert.IsNull(product);
        }
    }
}
