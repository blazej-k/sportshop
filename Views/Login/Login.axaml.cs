using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SportShop.ViewModels;

namespace SportShop.Views
{
	public partial class LoginView : UserControl
	{
		public LoginView()
		{
			DataContext = new LoginViewModel();
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