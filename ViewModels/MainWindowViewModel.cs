
namespace SportShop.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    public string UserName { get; set; } = "John Doe";
    public string Greeting => "Hello world";
  }
}