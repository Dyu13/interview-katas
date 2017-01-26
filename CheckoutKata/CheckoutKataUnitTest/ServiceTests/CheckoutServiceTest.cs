using System.Collections.Generic;
using CheckoutKata.Core.Models;
using CheckoutKata.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKataUnitTest.ServiceTests
{
    [TestClass]
    public class CheckoutServiceTest
    {
        [TestMethod]
        public void ShouldScanProduct()
        {
            // Arrange
            var checkoutService = new CheckoutService();
            const string productSku = "A";

            // Act
            checkoutService.Scan(productSku);

            // Assert
        }

        [TestMethod]
        public void ShouldGetTotalPrice()
        {
            // Arrange
            var checkoutService = new CheckoutService();
            var productsQuantitiesDictionary = new Dictionary<Product, int>
            {
                {new Product
                {
                    Sku = "A",
                    UnitPrice = 5,
                    SpecialQty = 3,
                    SpecialPrice = 10
                }, 3 },
                {new Product
                {
                    Sku = "B",
                    UnitPrice = 7,
                    SpecialQty = -1,
                    SpecialPrice = -1
                }, 2 }
            };
            const int expectedTotalPrice = 24;

            // Act
            var totalPrice = checkoutService.GetTotalPrice(productsQuantitiesDictionary);

            // Assert
            Assert.AreEqual(expectedTotalPrice, totalPrice);
        }
    }
}
