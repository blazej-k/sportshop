using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportShop.Models;
using SportShop.Services;
using SportShop.Views;

namespace SportShop.ViewModels
{
    public class MainWindowViewModel : UserService, INotifyPropertyChanged
    {
        private object _currentView;

        public MainWindowViewModel()
        {
            CurrentView = new DashboardView(RedirectToLogin, RedirectToDashboard, new User { Id = "1", Login = "admin", Password = "admin", Type = UserType.ADMIN });
            // CurrentView = new LoginView(redirectToDashboard: RedirectToDashboard);
            // CurrentView = new CheckoutView(redirectToDashboard: RedirectToDashboard, new User { Id = "1", Login = "admin", Password = "admin", Type = UserType.ADMIN });
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void RedirectToDashboard(User currentUser)
        {
            CurrentView = new DashboardView(RedirectToLogin, RedirectToCheckout, currentUser);
        }

        public void RedirectToLogin()
        {
            CurrentView = new LoginView(RedirectToDashboard);
        }

        public void RedirectToCheckout(User currentUser)
        {
            CurrentView = new CheckoutView(RedirectToDashboard, currentUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}