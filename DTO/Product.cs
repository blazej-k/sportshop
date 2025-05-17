using SportShop.Models;

namespace DTO
{
  public class UpdateProductDto
  {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
  }

  public class CreateProductDto
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
  }
}