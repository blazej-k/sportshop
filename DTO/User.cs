using SportShop.Models;

namespace DTO
{
  public class UpdateUserDto
  {
    public UserType? UserType { get; set; }
  }

  public class CreateUserDto
  {
    public UserType UserType { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
  }
}