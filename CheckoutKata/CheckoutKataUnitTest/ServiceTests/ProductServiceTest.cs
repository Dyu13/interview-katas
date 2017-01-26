using System.Linq;
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
            var expectedProductSku = "A";

            // Act
            var productList = productService.GetAllProducts().ToList();

            // Assert
            Assert.IsNotNull(productList);
            Assert.AreEqual(expectedProductSku, productList[0].Sku);
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
            var products = productService.GetAllProducts().ToList();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(productPrice, products[0].UnitPrice);
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
            var products = productService.GetAllProducts().ToList();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(productSpecialQty, products[0].SpecialQty);
            Assert.AreEqual(productSpecialPrice, products[0].SpecialPrice);
        }

        [TestMethod]
        public void ShouldAddProduct()
        {
            // Arrange
            var productService = new ProductService();
            var product = new Product {Sku = "X"};

            // Act
            productService.AddProduct(product);
            var products = productService.GetAllProducts().ToList();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(product.Sku, products.Last().Sku);
        }

        [TestMethod]
        public void ShouldRemoveProduct()
        {
            // Arrange
            var productService = new ProductService();
            var productSku = "A";
            var expectedProductsCount = 3;

            // Act
            productService.RemoveProduct(productSku);
            var products = productService.GetAllProducts().ToList();

            // Assert
            Assert.AreEqual(expectedProductsCount, products.Count);
        }
    }
}
