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
    }
}
