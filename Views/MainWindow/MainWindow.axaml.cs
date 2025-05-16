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
}