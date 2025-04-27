using System;
using Avalonia.Controls;
using SportShop.ViewModels;

namespace SportShop;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        DataContext = new MainWindowViewModel();
        InitializeComponent();
    }

    private void OnButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Console.WriteLine("Button clicked");
    }
}