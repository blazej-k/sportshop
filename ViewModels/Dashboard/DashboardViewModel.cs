using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportShop.Models;
using SportShop.Services;

namespace SportShop.ViewModels
{
    public class DashboardViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Action RedirectToLogin;
        private User _currentUser;
        private Order[] _orders;
        private User[] _users;
        private bool _isAdmin;
        private UserService usersService = new UserService();
        private OrderService orderService = new OrderService();

        public DashboardViewModel(Action redirectToLogin, User currentUser)
        {
            RedirectToLogin = redirectToLogin;
            _currentUser = currentUser;
            Orders = orderService.GetOrdersByUserId(currentUser.Id);
            Users = usersService.GetAll();


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

        public void LogOut()
        {
            RedirectToLogin.Invoke();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}