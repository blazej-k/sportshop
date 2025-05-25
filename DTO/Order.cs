using SportShop.Models;

namespace DTO
{
  public class UpdateOrderDto
  {
    public int? Quantity { get; set; }
    public double? Price { get; set; }
    public OrderStatus? Status { get; set; }
  }

  public class CreateOrderDto
  {
    public int Quantity { get; set; }
    public string UserId { get; set; }
    public string ProductId { get; set; }
  }
}