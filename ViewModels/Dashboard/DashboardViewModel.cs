using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DTO;
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
        private bool _isOrdersLoading = false;
        private bool _isUsersLoading = false;
        private bool _showOrdersNoOrdersMessage = false;

        public DashboardViewModel(Action redirectToLogin, Action<User> redirectToCheckout, User currentUser)
        {
            RedirectToLogin = redirectToLogin;
            _currentUser = currentUser;
            RedirectToCheckout = redirectToCheckout;
            UpdateIsAdmin();
            SetDashboardDataFromApi();
        }

        private async void SetDashboardDataFromApi()
        {
            IsOrdersLoading = true;
            IsUsersLoading = true;

            if (IsAdmin)
            {
                Users = await _usersService.GetAll();
                Orders = await _orderService.GetAll();
            }
            else
            {
                Orders = await _orderService.GetOrdersByUserId(_currentUser.Id);
            }

            IsOrdersLoading = false;
            IsUsersLoading = false;
            ShowNoOrdersMessage = Orders.Length == 0;
        }

        public Order[] Orders
        {
            get => _orders;
            set
            {
                if (_orders != value)
                {
                    _orders = value;
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

        public bool IsOrdersLoading
        {
            get => _isOrdersLoading;
            private set
            {
                if (_isOrdersLoading != value)
                {
                    _isOrdersLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsUsersLoading
        {
            get => _isUsersLoading;
            private set
            {
                if (_isUsersLoading != value)
                {
                    _isUsersLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowNoOrdersMessage
        {
            get => _showOrdersNoOrdersMessage;
            private set
            {
                if (_showOrdersNoOrdersMessage != value)
                {
                    _showOrdersNoOrdersMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OrdersTitle => IsAdmin ? "All orders" : "My orders";

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
            Orders = IsAdmin ? await _orderService.GetAll() : await _orderService.GetOrdersByUserId(_currentUser.Id);
        }

        public async Task OnUserRemove(string id)
        {
            _usersService.Delete(id);
            Users = await _usersService.GetAll();
        }

        public async Task OnToggleStatus(Order order)
        {
            Order updatedOrder = await _orderService.Update(order.Id, new UpdateOrderDto { Status = order.Status == OrderStatus.IN_PROGRESS ? OrderStatus.COMPLETED : OrderStatus.IN_PROGRESS, Quantity = order.Quantity });
            Orders = Orders.Select(o => o.Id == updatedOrder.Id ? updatedOrder : o).ToArray();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}