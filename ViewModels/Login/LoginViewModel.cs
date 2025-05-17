using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportShop.Models;
using SportShop.Services;

namespace SportShop.ViewModels
{
    public class LoginViewModel : UserService, INotifyPropertyChanged
    {
        private string _username = "";
        private string _password = "";
        private string _errorMessage = "";
        private readonly Action<User> RedirectToDashboard;

        public LoginViewModel(Action<User> redirectToDashboard)
        {
            RedirectToDashboard = redirectToDashboard;
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string ValidateLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return "Username and password are required";
            }

            return "";
        }

        public void OnLogin()
        {
            string errorMessage = ValidateLogin();
            bool isValid = string.IsNullOrEmpty(errorMessage);

            ErrorMessage = errorMessage;

            if (!isValid)
            {
                return;
            }

            User? user = CheckIfUserExists(_username, _password);

            if (user is null)
            {
                ErrorMessage = "Username or password are incorrect";
                return;
            }

            RedirectToDashboard.Invoke(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}