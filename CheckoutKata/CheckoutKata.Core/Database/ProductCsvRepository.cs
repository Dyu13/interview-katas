using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CheckoutKata.Core.Models;
using PCLStorage;

namespace CheckoutKata.Core.Database
{
    public class ProductCsvRepository<T> : IRepository<T> where T : class, new()
    {
        #region Constructor

        private IFile _file;

        public ProductCsvRepository()
        {
            var client = new HttpClient();
            var dataContent = client.GetStringAsync("https://raw.githubusercontent.com/Dyu13/interview-katas/develop/CheckoutKata/CheckoutKata.Core/Content/Products.csv").Result;

            // TODO: change to a static folder in order to always use the same DB for Admin And User
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = rootFolder.CreateFolderAsync("Content",
                CreationCollisionOption.OpenIfExists).Result;
            _file = folder.CreateFileAsync("Products.csv",
                CreationCollisionOption.OpenIfExists).Result;

            var fileText = _file.ReadAllTextAsync().Result;
            if (fileText == string.Empty)
            {
                _file.WriteAllTextAsync(dataContent).Wait();
            }
        }

        #endregion Constructor

        #region GetDataList

        public IEnumerable<T> GetDataList()
        {
            var content = _file.ReadAllTextAsync().Result;
            var productsText = content.Split('\n').Skip(1);

            var items = (from p in productsText
                where p != string.Empty
                select p.Split(',')
                into properties
                select new Product
                {
                    Sku = properties[0],
                    UnitPrice = Convert.ToDecimal(properties[1]),
                    SpecialQty = Convert.ToInt32(properties[2]),
                    SpecialPrice = Convert.ToDecimal(properties[3])
                }).ToList();

            return items as List<T>;
        }

        #endregion GetDataList

        #region GetData

        public T GetData(string name)
        {
            var products = GetDataList() as List<Product>;

            var product = products.FirstOrDefault(p => p.Sku == name);

            return product as T;
        }

        #endregion GetData

        #region Insert

        public void Insert(T entry)
        {
            var product = entry as Product;
            if(product == null) return;

            var productDetails = product.Sku + "," + product.UnitPrice + "," + product.SpecialQty + "," +
                                    product.SpecialPrice;

            var content = _file.ReadAllTextAsync().Result;
            content = content + productDetails;

            _file.WriteAllTextAsync(content).Wait();
        }

        #endregion Insert

        #region Update

        public void Update(T entry)
        {
            var product = entry as Product;
            var productList = GetDataList() as List<Product>;

            if(product == null || productList == null) return; // TODO: log it

            var productIndex = productList.FindIndex(p => p.Sku == product.Sku);

            try
            {
                productList[productIndex] = product;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                // TODO: log exception
            }
            catch (Exception exception)
            {
                // TODO: log exception
            }

            var content = productList.Select(p => p.Sku + "," + p.UnitPrice + "," + p.SpecialQty + "," + p.SpecialPrice + "\n")
                .Aggregate("SKU,UnitPrice,SpecialQty,SpecialPrice\n", (current, productDetails) => current + productDetails);

            _file.WriteAllTextAsync(content).Wait();
        }

        #endregion Update

        #region Delete

        public void Delete(T entry)
        {
            var product = entry as Product;
            var productList = GetDataList() as List<Product>;

            if (product == null || productList == null) return; // TODO: log it

            var productIndex = productList.FindIndex(p => p.Sku == product.Sku);

            try
            {
                productList.RemoveAt(productIndex);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                // TODO: log exception
            }
            catch (Exception exception)
            {
                // TODO: log exception
            }

            var content = productList.Select(p => p.Sku + "," + p.UnitPrice + "," + p.SpecialQty + "," + p.SpecialPrice + "\n")
                .Aggregate("SKU,UnitPrice,SpecialQty,SpecialPrice\n", (current, productDetails) => current + productDetails);

            _file.WriteAllTextAsync(content).Wait();
        }

        #endregion Delete
    }
}