using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportShop.Services;
using SportShop.Views;

namespace SportShop.ViewModels
{
    public class MainWindowViewModel : UserService, INotifyPropertyChanged
    {
        private object _currentView;

        public MainWindowViewModel()
        {
            CurrentView = new LoginView();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}