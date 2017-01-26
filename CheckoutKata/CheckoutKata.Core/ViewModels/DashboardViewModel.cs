using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CheckoutKata.Core.Models;
using CheckoutKata.Core.Services;
using CheckoutKata.Core.ViewModels.NavigationParameters;
using MvvmCross.Core.ViewModels;

namespace CheckoutKata.Core.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        #region Constructor

        public DashboardViewModel(IProductService productService, ICheckoutService checkoutService)
        {
            _productService = productService;
            _checkoutService = checkoutService;
        }

        #endregion Constructor

        #region Initialization

        public void Init()
        {
            _productList = _productService.GetAllProducts().ToList().Select(p => p.Sku).ToList();

            FilteredProducts = new MvxObservableCollection<string>(_productList);

            _productsQuantitiesDictionary = new Dictionary<Product, int>();

            IsAnyProductSelected = false;
            IsNotNewProduct = true;
        }

        #endregion Initialization

        #region Private Properties

        private readonly IProductService _productService;
        private readonly ICheckoutService _checkoutService;
        private Dictionary<Product, int> _productsQuantitiesDictionary;
        private List<string> _productList;

        #endregion Private Properties

        #region Public Properties

        #region Admin

        #region PROPERTY: FilteredProducts

        private MvxObservableCollection<string> _filteredProducts;

        public MvxObservableCollection<string> FilteredProducts
        {
            get { return _filteredProducts; }
            set
            {
                if(_filteredProducts == value) return;

                _filteredProducts = value;

                RaisePropertyChanged(() => FilteredProducts);

                IsNewProduct = _filteredProducts.Count == 0;
            }
        }

        #endregion PROPERTY: FilteredProducts

        #region PROPERTY: SelectedProduct

        private string _selectedProduct;

        public string SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct == value) return;

                _selectedProduct = value;

                RaisePropertyChanged(() => SelectedProduct);

                if (SelectedProduct != null)
                {
                    IsAnyProductSelected = true;
                    return;
                }

                IsAnyProductSelected = false;
            }
        }

        #endregion PROPERTY: SelectedProduct

        #region PROPERTY: FilterText

        private string _filterText;

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText == value) return;

                _filterText = value;

                RaisePropertyChanged(() => FilterText);

                var filteredList = _productList.Where(p => p.Contains(FilterText));
                FilteredProducts = new MvxObservableCollection<string>(filteredList);

                IsAnyProductSelected = false;
            }
        }

        #endregion PROPERTY: FilterText

        #region PROPERTY: IsAnyProductSelected

        private bool _isAnyProductSelected;

        public bool IsAnyProductSelected
        {
            get { return _isAnyProductSelected; }
            set
            {
                if (_isAnyProductSelected == value) return;

                _isAnyProductSelected = value;

                RaisePropertyChanged(() => IsAnyProductSelected);
            }
        }

        #endregion PROPERTY: IsAnyProductSelected

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

        #endregion Admin

        #region User

        #region PROPERTY: ProductSku

        private string _productSku;

        public string ProductSku
        {
            get { return _productSku; }
            set
            {
                if (_productSku == value) return;

                _productSku = value;

                RaisePropertyChanged(() => ProductSku);
            }
        }

        #endregion PROPERTY: ProductSku

        #region PROPERTY: TotalPrice

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice == value) return;

                _totalPrice = value;

                RaisePropertyChanged(() => TotalPrice);
            }
        }

        #endregion PROPERTY: TotalPrice

        #region PROPERTY: ProductSymmaryList

        private MvxObservableCollection<ProductSummary> _productSymmaryList;

        public MvxObservableCollection<ProductSummary> ProductSymmaryList
        {
            get { return _productSymmaryList; }
            set
            {
                if (_productSymmaryList == value) return;

                _productSymmaryList = value;

                RaisePropertyChanged(() => ProductSymmaryList);
            }
        }

        #endregion PROPERTY: FilteredProducts

        #endregion User

        #endregion Public Properties

        #region Commands

        #region Admin

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
            if (SelectedProduct == null)
            {
                // TODO: log and show popup message

                return;
            }

            var productDetailsNavigationParameter = new ProductDetailsNavigationParameter
            {
                ProductSku = SelectedProduct,
                IsForNewProduct = false
            };

            ShowViewModel<ProductDetailsViewModel>(productDetailsNavigationParameter);
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
            var productDetailsNavigationParameter = new ProductDetailsNavigationParameter
            {
                ProductSku = SelectedProduct,
                IsForNewProduct = true
            };

            ShowViewModel<ProductDetailsViewModel>(productDetailsNavigationParameter);
        }

        #endregion COMMAND: Insert

        #region COMMAND: RemoveProduct

        private IMvxCommand _removeProductCommand;

        public ICommand RemoveProductCommand
        {
            get
            {
                _removeProductCommand = _removeProductCommand ?? new MvxCommand(RemoveProduct);
                return _removeProductCommand;
            }
        }

        private void RemoveProduct()
        {
            if (SelectedProduct == null)
            {
                // TODO: log and show popup message

                return;
            }

            _productService.RemoveProduct(SelectedProduct);

            _productList.Remove(SelectedProduct);
            FilteredProducts = new MvxObservableCollection<string>(_productList);
        }

        #endregion COMMAND: RemoveProduct

        #endregion Admin

        #region User

        #region COMMAND: Scan

        private IMvxCommand _scanCommand;

        public ICommand ScanCommand
        {
            get
            {
                _scanCommand = _scanCommand ?? new MvxCommand(Scan);
                return _scanCommand;
            }
        }

        private void Scan()
        {
            var product = _checkoutService.Scan(ProductSku);

            if (product == null)
            {
                // TODO: log and popup message
                
                return;
            }

            var isProductAlreadyAdded = _productsQuantitiesDictionary.Keys.Any(p => p.Sku == product.Sku);

            if (isProductAlreadyAdded)
            {
                _productsQuantitiesDictionary[product]++;

                ProductSymmaryList.FirstOrDefault(p => p.Sku == product.Sku).Qty++;

                TotalPrice = _checkoutService.GetTotalPrice(_productsQuantitiesDictionary);

                return;
            }

            _productsQuantitiesDictionary.Add(product, 1);

            ProductSymmaryList.Add(new ProductSummary
            {
                Sku = product.Sku,
                Qty = 1
            });

            TotalPrice = _checkoutService.GetTotalPrice(_productsQuantitiesDictionary);
        }

        #endregion COMMAND: Scan

        #region COMMAND: Pay

        private IMvxCommand _payCommand;

        public ICommand PayCommand
        {
            get
            {
                _payCommand = _payCommand ?? new MvxCommand(Pay);
                return _payCommand;
            }
        }

        private void Pay()
        {
            ProductSku = string.Empty;
            ProductSymmaryList = new MvxObservableCollection<ProductSummary>();
            TotalPrice = 0;
            _productsQuantitiesDictionary = new Dictionary<Product, int>();
        }

        #endregion COMMAND: Pay

        #endregion User

        #endregion Commands
    }
}