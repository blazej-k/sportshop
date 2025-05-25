using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportShop.Models;
using SportShop.Services;

namespace SportShop.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Action RedirectToLogin;
        private readonly Action<User> RedirectToCheckout;
        private User _currentUser;
        private Order[] _orders;
        private User[] _users;
        private bool _isAdmin;
        private readonly UserService _usersService = new UserService();
        private readonly OrderService _orderService = new OrderService();

        public DashboardViewModel(Action redirectToLogin, Action<User> redirectToCheckout, User currentUser)
        {
            RedirectToLogin = redirectToLogin;
            _currentUser = currentUser;
            Orders = _orderService.GetOrdersByUserId(currentUser.Id);
            Users = _usersService.GetAll();
            RedirectToCheckout = redirectToCheckout;
        }

        public Order[] Orders
        {
            get => _orders;
            set
            {
                if (_orders != value)
                {
                    _orders = value;
                    UpdateIsAdmin();
                    OnPropertyChanged();
                }
            }
        }

        public User[] Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            private set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged();
                }
            }
        }

        private void UpdateIsAdmin()
        {
            IsAdmin = _currentUser?.Type == UserType.ADMIN;
        }

        public void SignOut()
        {
            RedirectToLogin.Invoke();
        }

        public void OnCheckout()
        {
            RedirectToCheckout.Invoke(_currentUser);
        }

        public void OnRemove(string id)
        {
            _orderService.Delete(id);
            Orders = _orderService.GetAll();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}