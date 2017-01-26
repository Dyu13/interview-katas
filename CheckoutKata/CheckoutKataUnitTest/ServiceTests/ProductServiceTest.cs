using CheckoutKata.Core.Models;
using CheckoutKata.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKataUnitTest.ServiceTests
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public void ShouldGettAllProducts()
        {
            // Arrange
            var productService = new ProductService();

            // Act
            var productList = productService.GetAllProducts();

            // Assert
            Assert.IsNotNull(productList);
        }

        [TestMethod]
        public void ShouldUpdateProductPrice()
        {
            // Arrange
            var productService = new ProductService();
            var productSku = "A";
            var productPrice = 22.48m;

            // Act
            productService.UpdateProductPrice(productSku, productPrice);

            // Assert
        }

        [TestMethod]
        public void ShouldUpdateProductSpecialPrice()
        {
            // Arrange
            var productService = new ProductService();
            var productSku = "A";
            var productSpecialQty = 3;
            var productSpecialPrice = 22.48m;

            // Act
            productService.UpdateProductSpecialPrice(productSku, productSpecialQty, productSpecialPrice);

            // Assert
        }

        [TestMethod]
        public void ShouldAddProduct()
        {
            // Arrange
            var productService = new ProductService();
            var product = new Product();

            // Act
            productService.AddProduct(product);

            // Assert
        }

        [TestMethod]
        public void ShouldRemoveProduct()
        {
            // Arrange
            var productService = new ProductService();
            var productSku = "A";

            // Act
            productService.RemoveProduct(productSku);

            // Assert
        }
    }
}
