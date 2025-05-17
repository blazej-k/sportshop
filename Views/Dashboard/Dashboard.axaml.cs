using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SportShop.Models;
using SportShop.ViewModels;

namespace SportShop.Views
{
  public partial class DashboardView : UserControl
  {
    public DashboardView(Action redirectToLogin, User currentUser)
    {
      InitializeComponent();
      DataContext = new DashboardViewModel(redirectToLogin, currentUser);
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}