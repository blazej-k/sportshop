using System.Collections.Generic;
using SportShop.Models;

namespace SportShop.Services
{
    public class ProductService
    {
        private List<Product> mockProducts = new List<Product>
        {
            new Product { Id = "1", Name = "Product 1", Price = 100 },
            new Product { Id = "2", Name = "Product 2", Price = 200 },
            new Product { Id = "3", Name = "Product 3", Price = 300 },
        };

        public List<Product> GetProducts()
        {
            return mockProducts;
        }

        public Product GetProductById(string id)
        {
            return mockProducts.Find(p => p.Id == id);
        }

    }
}