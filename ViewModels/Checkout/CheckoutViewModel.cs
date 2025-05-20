using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportShop.Models;
using SportShop.Services;

namespace SportShop.ViewModels
{
    public class CheckoutViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private User _currentUser;
        private Product[] _products;
        private Product _selectedProduct;
        private int _quantity;
        private ProductService productService = new ProductService();

        public CheckoutViewModel(Action<User> redirectToDashboard, User currentUser)
        {
            _currentUser = currentUser;
            Products = productService.GetAll();
        }

        public Product[] Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged();
                }
            }
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}