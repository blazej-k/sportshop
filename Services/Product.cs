using System.Collections.Generic;
using DTO;
using DynamicData;
using Interfaces;
using SportShop.Models;

namespace SportShop.Services
{
    public class ProductService : IService<Product, UpdateProductDto, CreateProductDto>
    {
        private List<Product> mockProducts = new List<Product>
        {
            new Product { Id = "1", Name = "Product 1", Price = 100 },
            new Product { Id = "2", Name = "Product 2", Price = 200 },
            new Product { Id = "3", Name = "Product 3", Price = 300 },
        };

        public Product[] GetAll()
        {
            return [.. mockProducts];
        }

        public Product? GetOne(string id)
        {
            return mockProducts.FindLast(user => user.Id == id);
        }

        public Product Create(CreateProductDto dto)
        {
            int count = mockProducts.Count + 1;
            mockProducts.Add(new Product { Id = $"{count}", Name = dto.Name, Price = dto.Price, Description = dto.Description });
            return GetOne($"{count}");
        }

        public Product Update(string id, UpdateProductDto dto)
        {
            Product currentProduct = mockProducts.Find(user => user.Id == id);
            mockProducts.Replace(currentProduct, new Product { Id = currentProduct.Id, Name = dto.Name ?? currentProduct.Name, Price = dto.Price ?? currentProduct.Price, Description = dto.Description ?? currentProduct.Description });
            return GetOne(currentProduct.Id);
        }

        public void Delete(string id)
        {
            mockProducts.RemoveAll(p => p.Id == id);
        }
    }
}