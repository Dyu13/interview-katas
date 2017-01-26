using System;
using System.Linq;
using System.Windows.Input;
using CheckoutKata.Core.Models;
using CheckoutKata.Core.Services;
using CheckoutKata.Core.ViewModels.NavigationParameters;
using MvvmCross.Core.ViewModels;

namespace CheckoutKata.Core.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        #region Constructor

        public ProductDetailsViewModel(IProductService productService)
        {
            _productService = productService;
        }

        #endregion Constructor

        #region Initialization

        public void Init(ProductDetailsNavigationParameter productDetailsNavigationParameter)
        {
            IsNotNewProduct = true;
            IsNewProduct = productDetailsNavigationParameter.IsForNewProduct;

            if (IsNewProduct)
            {
                Product = new Product
                {
                    Sku = productDetailsNavigationParameter.ProductSku,
                    UnitPrice = 0,
                    SpecialQty = -1,
                    SpecialPrice = -1
                };
            }
            else
            {
                Product =
                    _productService.GetAllProducts()
                        .ToList()
                        .FirstOrDefault(p => p.Sku == productDetailsNavigationParameter.ProductSku);
            }

            _productPrice = Product.UnitPrice;
            UnitPrice = Convert.ToDouble(Product.UnitPrice);
            _productSpecialQty = Product.SpecialQty;
            _productSpecialPrice = Product.SpecialPrice;
            SpecialPrice = Convert.ToDouble(Product.SpecialPrice);
        }

        #endregion Initialization

        #region Private Properties

        private readonly IProductService _productService;
        private decimal _productPrice;
        private int _productSpecialQty;
        private decimal _productSpecialPrice;

        #endregion Private Properties

        #region Public Properties

        #region PROPERTY: IsNewProduct

        private bool _isNewProduct;

        public bool IsNewProduct
        {
            get { return _isNewProduct; }
            set
            {
                if (_isNewProduct == value) return;

                _isNewProduct = value;

                RaisePropertyChanged(() => IsNewProduct);

                IsNotNewProduct = !IsNewProduct;
            }
        }

        #endregion PROPERTY: IsNewProduct

        #region PROPERTY: IsNotNewProduct
        // TODO: use Converter for invert the Visibility
        private bool _isNotNewProduct;

        public bool IsNotNewProduct
        {
            get { return _isNotNewProduct; }
            set
            {
                if (_isNotNewProduct == value) return;

                _isNotNewProduct = value;

                RaisePropertyChanged(() => IsNotNewProduct);
            }
        }

        #endregion PROPERTY: IsUpdateAvailable

        #region PROPERTY: UnitPrice
        //TODO: use converter instead
        private double _unitPrice;

        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice == value) return;

                _unitPrice = value;

                RaisePropertyChanged(() => UnitPrice);

                Product.UnitPrice = Convert.ToDecimal(UnitPrice);
            }
        }

        #endregion PROPERTY: UnitPrice

        #region PROPERTY: SpecialPrice
        //TODO: use converter instead
        private double _specialPrice;

        public double SpecialPrice
        {
            get { return _specialPrice; }
            set
            {
                if (_specialPrice == value) return;

                _specialPrice = value;

                RaisePropertyChanged(() => SpecialPrice);

                Product.SpecialPrice = Convert.ToDecimal(SpecialPrice);
            }
        }

        #endregion PROPERTY: SpecialPrice

        #region PROPERTY: Product

        private Product _product;

        public Product Product
        {
            get { return _product; }
            set
            {
                if (_product == value) return;

                _product = value;

                RaisePropertyChanged(() => Product);
            }
        }

        #endregion PROPERTY: Product

        #endregion Public Properties

        #region Commands

        #region COMMAND: UpdateProduct

        private IMvxCommand _updateProductCommand;

        public ICommand UpdateProductCommand
        {
            get
            {
                _updateProductCommand = _updateProductCommand ?? new MvxCommand(UpdateProduct);
                return _updateProductCommand;
            }
        }

        private void UpdateProduct()
        {
            if (_productPrice != Product.UnitPrice)
            {
                _productService.UpdateProductPrice(Product.Sku, Product.UnitPrice);
            }

            if (_productSpecialQty != Product.SpecialQty ||
                _productSpecialPrice != Product.SpecialPrice)
            {
                _productService.UpdateProductSpecialPrice(Product.Sku, Product.SpecialQty, Product.SpecialPrice);
            }

            Close(this);
        }

        #endregion COMMAND: UpdateProduct

        #region COMMAND: InsertProduct

        private IMvxCommand _insertProductCommand;

        public ICommand InsertProductCommand
        {
            get
            {
                _insertProductCommand = _insertProductCommand ?? new MvxCommand(InsertProduct);
                return _insertProductCommand;
            }
        }

        private void InsertProduct()
        {
            _productService.AddProduct(Product);

            Close(this);

            // TODO: use messenger to notify the Dashboard that the list has to be taken again from database to have the new product
        }

        #endregion COMMAND: Insert

        #endregion Commands
    }
}