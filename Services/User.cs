using System.Collections.Generic;
using System.Linq;
using SportShop.Models;

namespace SportShop.Services
{
  public class UserService
  {
    private List<User> mockUsers = new List<User>
        {
            new User { Id = "1", Login = "admin", Password = "admin", Type = UserType.Admin },
            new User { Id = "2", Login = "customer", Password = "customer", Type = UserType.Customer },
        };

    public bool CheckIfUserExists(string login, string password)
    {
      return mockUsers.Exists(user => user.Login == login && user.Password == password);
    }
  }
}