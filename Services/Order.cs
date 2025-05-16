using System;
using System.Collections.Generic;
using SportShop.Models;

namespace SportShop.Services
{
  public class OrderService
  {
    private List<Order> mockOrders = new List<Order>
        {
            new Order { Id = "1", PublicId = "Z/0000001", User = new User { Id = "1", Login = "admin", Password = "admin", Type = UserType.ADMIN }, Product = new Product { Id = "1", Name = "Product 1", Price = 100 }, Quantity = 1, Price = 100, Date = DateTime.Now, Status = OrderStatus.IN_PROGRESS },
        };

    public List<Order> GetOrdersByUserId(string userId)
    {
      return mockOrders.FindAll(o => o.User.Id == userId);
    }

    public Order CreateOrder(Order order)
    {
      mockOrders.Add(order);
      return order;
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
  }
}