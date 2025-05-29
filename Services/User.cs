using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DTO;
using Interfaces;
using SportShop.Models;

namespace SportShop.Services
{
  public class UserService : APIService, IService<User, UpdateUserDto, CreateUserDto>
  {
    protected async Task<User?> CheckIfUserExists(string login, string password)
    {
      var data = await GetData($"""SELECT * FROM "user" WHERE login = '{login}' AND password = '{password}'""");

      if (data.Rows.Count == 0)
      {
        return null;
      }

      User user = MapDataRow(data.Rows[0]);
      return user;
    }

    async public Task<User[]> GetAll()
    {
      var data = await GetData($"""SELECT * FROM "user";""");
      User[] users = MapData(data);
      return users;
    }

    async public Task<User?> GetOne(string id)
    {
      var data = await GetData($"""SELECT * FROM "user" WHERE id = '{id}'""");
      User user = MapDataRow(data.Rows[0]);
      return user;
    }

    async public Task<User> Create(CreateUserDto dto)
    {
      var data = await GetData($"""INSERT INTO "user" (login, password, type) VALUES('{dto.Login}', '{dto.Password}', '{dto.Type}') RETURNING *""");
      User user = MapDataRow(data.Rows[0]);
      return await GetOne(user.Id);
    }

    async public Task<User> Update(string id, UpdateUserDto dto)
    {
      var data = await GetData($"""UPDATE "user" SET type = '{dto.Type}' WHERE "id" = '{id}' RETURNING *""");
      User user = MapDataRow(data.Rows[0]);
      return await GetOne(user.Id);
    }

    async public void Delete(string id)
    {
      await GetData($"""DELETE FROM "user" WHERE "id" = '{id}' """);
    }

    public User[] MapData(DataTable data)
    {
      List<User> users = [];

      foreach (DataRow row in data.Rows)
      {
        User user = MapDataRow(row);
        users.Add(user);
      }

      return [.. users];
    }


    public User MapDataRow(DataRow apiRow)
    {
      return new User { Id = apiRow["id"].ToString(), Login = apiRow["login"].ToString(), Password = apiRow["password"].ToString(), Type = (UserType)Enum.Parse(typeof(UserType), apiRow["type"].ToString()) };
    }
  }
}