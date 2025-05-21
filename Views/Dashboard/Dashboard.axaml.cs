using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SportShop.Models;
using SportShop.ViewModels;

namespace SportShop.Views
{
  public partial class DashboardView : UserControl
  {
    public DashboardView(Action redirectToLogin, Action<User> redirectToCheckout, User currentUser)
    {
      InitializeComponent();
      DataContext = new DashboardViewModel(redirectToLogin, redirectToCheckout, currentUser);
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }

    private void OnSignOut(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
      var viewModel = DataContext as DashboardViewModel;
      viewModel.SignOut();
    }

    private void OnCheckout(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
      var viewModel = DataContext as DashboardViewModel;
      viewModel.OnCheckout();
    }
  }
}