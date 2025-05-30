using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DTO;
using Interfaces;
using SportShop.Models;

namespace SportShop.Services
{
  public class OrderService : APIService, IService<Order, UpdateOrderDto, CreateOrderDto>
  {
    public async Task<Order[]> GetOrdersByUserId(string userId)
    {
      var data = await GetData($"""SELECT o.id, o."publicId", o.quantity, o.date, o.status, u.id as "userId", u.login as "userLogin", u.password as "userPassword", u.type as "userType", p.id as "productId", p.name as "productName", p.description as "productDescription", p.price as "productPrice" from "order" o LEFT JOIN "user" u ON u."id" = o."userId" LEFT JOIN "product" p ON p."id" = o."productId" WHERE o."userId" = '{userId}' ORDER BY "publicId";""");
      Order[] orders = MapData(data);
      return orders;
    }

    async public Task<Order[]> GetAll()
    {
      var data = await GetData($"""SELECT o.id, o."publicId", o.quantity, o.date, o.status, u.id as "userId", u.login as "userLogin", u.password as "userPassword", u.type as "userType", p.id as "productId", p.name as "productName", p.description as "productDescription", p.price as "productPrice" from "order" o LEFT JOIN "user" u ON u."id" = o."userId" LEFT JOIN "product" p ON p."id" = o."productId" ORDER BY "publicId";""");
      Order[] orders = MapData(data);
      return orders;
    }

    async public Task<Order?> GetOne(string id)
    {
      var data = await GetData($"""SELECT o.id, o."publicId", o.quantity, o.date, o.status, u.id as "userId", u.login as "userLogin", u.password as "userPassword", u.type as "userType", p.id as "productId", p.name as "productName", p.description as "productDescription", p.price as "productPrice" from "order" o LEFT JOIN "user" u ON u."id" = o."userId" LEFT JOIN "product" p ON p."id" = o."productId" WHERE o."id" = '{id}';""");
      Order order = MapDataRow(data.Rows[0]);
      return order;
    }

    async public Task<Order> Create(CreateOrderDto dto)
    {
      long unixTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
      string publicId = $"Z/{unixTimestampMs}";

      var data = await GetData($"""INSERT INTO "order" ("publicId", quantity, "productId", "userId") VALUES('{publicId}', {dto.Quantity}, '{dto.ProductId}', '{dto.UserId}') RETURNING *""");
      string orderId = data.Rows[0]["id"].ToString();
      return await GetOne(orderId);
    }

    async public Task<Order> Update(string id, UpdateOrderDto dto)
    {
      var data = await GetData($"""UPDATE "order" SET quantity = {dto.Quantity}, status = '{dto.Status}'  WHERE "id" = '{id}' RETURNING *""");
      string orderId = data.Rows[0]["id"].ToString();
      return await GetOne(orderId);
    }

    async public void Delete(string id)
    {
      await GetData($"""DELETE FROM "order" WHERE "id" = '{id}' """);
    }

    public Order MapDataRow(DataRow dataRow)
    {
      int orderQuantity = int.Parse(dataRow["quantity"].ToString());
      double productPrice = double.Parse(dataRow["productPrice"].ToString());
      double orderPrice = orderQuantity * productPrice;

      return new Order { Id = dataRow["id"].ToString(), PublicId = dataRow["publicId"].ToString(), Quantity = int.Parse(dataRow["quantity"].ToString()), Price = orderPrice, Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), dataRow["status"].ToString()), Date = DateTime.Parse(dataRow["date"].ToString()), User = new User { Id = dataRow["userId"].ToString(), Login = dataRow["userLogin"].ToString(), Password = dataRow["userPassword"].ToString(), Type = (UserType)Enum.Parse(typeof(UserType), dataRow["userType"].ToString()) }, Product = new Product { Id = dataRow["productId"].ToString(), Name = dataRow["productName"].ToString(), Price = double.Parse(dataRow["productPrice"].ToString()), Description = dataRow["productDescription"].ToString() } };
    }

    public Order[] MapData(DataTable data)
    {
      List<Order> orders = [];

      foreach (DataRow row in data.Rows)
      {
        Order order = MapDataRow(row);
        orders.Add(order);
      }

      return [.. orders];
    }
  }
}