using System;
using CheckoutKata.Core.Database;
using CheckoutKata.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            var csvRepository = csvRepositoryFactory.Create<CsvRepository<Product>>();

            // Assert
            Assert.IsNotNull(csvRepository);
        }

        [TestMethod]
        public void ShouldGetCsvRepositoryDataList()
        {
            // Arrange
            IRepository<Product> csvRepository = new CsvRepository<Product>();

            // Act
            var dataList = csvRepository.GetDataList();

            // Assert
            Assert.IsNotNull(dataList);
        }

        [TestMethod]
        public void ShouldGetCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new CsvRepository<Product>();

            // Act
            var data = csvRepository.GetData("A");

            // Assert
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void ShouldInsertCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new CsvRepository<Product>();

            // Act
            csvRepository.Insert(new Product {Sku = "X"});
            var data = csvRepository.GetData("X");

            // Assert
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void ShouldUpdateCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new CsvRepository<Product>();

            // Act
            csvRepository.Update(new Product { Sku = "X", UnitPrice = 20 });
            var data = csvRepository.GetData("X");

            // Assert
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void ShouldRemoveCsvRepositoryData()
        {
            // Arrange
            IRepository<Product> csvRepository = new CsvRepository<Product>();

            // Act
            csvRepository.Delete(new Product { Sku = "X" });
            var data = csvRepository.GetData("X");

            // Assert
            Assert.IsNull(data);
        }
    }
}
