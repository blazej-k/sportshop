using SportShop.Models;

namespace DTO
{
  public class UpdateUserDto
  {
    public UserType? Type { get; set; }
  }

  public class CreateUserDto
  {
    public UserType Type { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
  }
}