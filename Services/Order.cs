using System;
using System.Collections.Generic;
using DTO;
using DynamicData;
using Interfaces;
using SportShop.Models;

namespace SportShop.Services
{
  public class OrderService : IService<Order, UpdateOrderDto, CreateOrderDto>
  {
    private List<Order> mockOrders = new List<Order>
    {
        new Order { Id = "1", PublicId = "Z/0000001", User = new User { Id = "1", Login = "admin", Password = "admin", Type = UserType.ADMIN }, Product = new Product { Id = "1", Name = "Product 1", Price = 100 }, Quantity = 1, Price = 100, Date = DateTime.Now, Status = OrderStatus.IN_PROGRESS },
    };

    public Order[] GetOrdersByUserId(string userId)
    {
      return mockOrders.FindAll(o => o.User.Id == userId).ToArray();
    }

    public Order? UpdateOrderStatus(string orderId, OrderStatus status)
    {
      var orderToUpdate = mockOrders.Find(o => o.Id == orderId);

      if (orderToUpdate != null)
      {
        orderToUpdate.Status = status;
        return orderToUpdate;
      }

      return null;
    }

    public Order[] GetAll()
    {
      return [.. mockOrders];
    }

    public Order? GetOne(string id)
    {
      return mockOrders.FindLast(user => user.Id == id);
    }

    public Order Create(CreateOrderDto dto)
    {
      int count = mockOrders.Count + 1;
      mockOrders.Add(new Order { Id = $"{count}", Quantity = dto.Quantity, Status = OrderStatus.IN_PROGRESS, PublicId = $"{count}", Date = DateTime.Now, User = new User { Id = "1", Login = "admin", Password = "admin", Type = UserType.ADMIN }, Product = new Product { Id = "1", Name = "Product 1", Price = 100 }, });
      return GetOne(count.ToString());
    }

    public Order Update(string id, UpdateOrderDto dto)
    {
      Order currentOrder = mockOrders.Find(order => order.Id == id);
      mockOrders.Replace(currentOrder, new Order { Id = currentOrder.Id, Quantity = dto.Quantity ?? currentOrder.Quantity, Status = dto.OrderStatus ?? currentOrder.Status, PublicId = currentOrder.PublicId, Date = currentOrder.Date, User = currentOrder.User, Product = currentOrder.Product, });
      return GetOne(currentOrder.Id);
    }

    public void Delete(string id)
    {
      mockOrders.RemoveAll(o => o.Id == id);
    }
  }
}