namespace SportShop.Models
{
  public enum OrderStatus
  {
    IN_PROGRESS,
    COMPLETED,
  }

  public class Order
  {
    public required string Id { get; set; }
    public required string PublicId { get; set; }
    public required User? User { get; set; }
    public required Product? Product { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public System.DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
  }
}