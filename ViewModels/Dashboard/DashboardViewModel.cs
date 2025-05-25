using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
            RedirectToCheckout = redirectToCheckout;
            SetDashboardDataFromApi();
        }

        private async Task SetDashboardDataFromApi()
        {
            Orders = await _orderService.GetOrdersByUserId(_currentUser.Id);

            if (IsAdmin)
            {
                Users = await _usersService.GetAll();
            }
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

        public static Func<OrderStatus, string> StatusMapper { get; } = status =>
        {
            switch (status)
            {
                case OrderStatus.COMPLETED:
                    return "Zakończone";
                case OrderStatus.IN_PROGRESS:
                    return "W trakcie realizacji";
                default:
                    return "Nieznany";
            }
        };

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

        public async Task OnOrderRemove(string id)
        {
            _orderService.Delete(id);
            Orders = await _orderService.GetAll();
        }

        public async Task OnUserRemove(string id)
        {
            _usersService.Delete(id);
            Users = await _usersService.GetAll();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}