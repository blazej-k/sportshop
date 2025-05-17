using System.Collections.Generic;
using DTO;
using DynamicData;
using Interfaces;
using SportShop.Models;

namespace SportShop.Services
{
  public class UserService : IService<User, UpdateUserDto, CreateUserDto>
  {
    private List<User> mockUsers = new List<User>
        {
            new User { Id = "1", Login = "admin", Password = "admin", Type = UserType.ADMIN },
            new User { Id = "2", Login = "customer", Password = "customer", Type = UserType.CUSTOMER },
        };

    protected User? CheckIfUserExists(string login, string password)
    {
      return mockUsers.Find(user => user.Login == login && user.Password == password);
    }

    public User[] GetAll()
    {
      return [.. mockUsers];
    }

    public User? GetOne(string id)
    {
      return mockUsers.FindLast(user => user.Id == id);
    }

    public User Create(CreateUserDto dto)
    {
      int count = mockUsers.Count + 1;
      mockUsers.Add(new User { Id = $"{count}", Login = dto.Login, Password = dto.Password, Type = dto.UserType });
      return GetOne($"{count}");
    }

    public User Update(string id, UpdateUserDto dto)
    {
      User currentUser = mockUsers.Find(user => user.Id == id);
      mockUsers.Replace(currentUser, new User { Id = currentUser.Id, Login = currentUser.Login, Password = currentUser.Password, Type = dto.UserType ?? currentUser.Type });
      return GetOne(currentUser.Id);
    }
  }
}