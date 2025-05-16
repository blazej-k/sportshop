using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SportShop.Models;
using SportShop.ViewModels;

namespace SportShop.Views
{
	public partial class LoginView : UserControl
	{
		public LoginView(Action<User> redirectToDashboard)
		{
			DataContext = new LoginViewModel(redirectToDashboard);
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void OnButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			var viewModel = DataContext as LoginViewModel;
			viewModel.OnLogin();
		}
	}
}