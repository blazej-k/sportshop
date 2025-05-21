using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DTO;
using SportShop.Models;
using SportShop.Services;

namespace SportShop.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private User _currentUser;
        private Product[] _products;
        private Product? _selectedProduct;
        private int _quantity = 1;
        private string _err = "";
        private Action<User> RedirectToDashboard;
        private readonly ProductService _productService = new ProductService();
        private readonly OrderService _orderService = new OrderService();

        public CheckoutViewModel(Action<User> redirectToDashboard, User currentUser)
        {
            _currentUser = currentUser;
            Products = _productService.GetAll();
            RedirectToDashboard = redirectToDashboard;
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

        public Product? SelectedProduct
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

        public string ErrorMessage
        {
            get => _err;
            set
            {
                if (_err != value)
                {
                    _err = value;
                    OnPropertyChanged();
                }
            }
        }

        private string ValidateCheckout()
        {
            if (SelectedProduct == null)
            {
                return "Please, provide valid product";
            }

            if (Quantity < 1)
            {
                return "Please, provide valid quantity";
            }

            return "";
        }


        public void OnSubmit()
        {
            string errorMessage = ValidateCheckout();
            bool isValid = string.IsNullOrEmpty(errorMessage);
            ErrorMessage = errorMessage;

            if (isValid)
            {
                _orderService.Create(new CreateOrderDto { ProductId = SelectedProduct.Id, Quantity = Quantity, UserId = _currentUser.Id });
                RedirectToDashboard.Invoke(_currentUser);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}