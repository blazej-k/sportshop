using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportShop.Models;

namespace SportShop.ViewModels
{
    public class DashboardViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Action _redirectToLogin;
        private readonly User _currentUser;

        public DashboardViewModel(Action redirectToLogin, User currentUser)
        {
            _redirectToLogin = redirectToLogin;
            _currentUser = currentUser;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}