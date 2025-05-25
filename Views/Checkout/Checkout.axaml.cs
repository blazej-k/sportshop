using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SportShop.Models;
using SportShop.ViewModels;

namespace SportShop.Views
{
  public partial class CheckoutView : UserControl
  {
    public CheckoutView(Action<User> redirectToDashboard, User currentUser)
    {
      InitializeComponent();
      DataContext = new CheckoutViewModel(redirectToDashboard, currentUser);
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }

    private void OnSubmit(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
      var viewModel = DataContext as CheckoutViewModel;
      viewModel.OnSubmit();
    }

    private void OnCancel(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
      var viewModel = DataContext as CheckoutViewModel;
      viewModel.OnCancel();
    }
  }
}