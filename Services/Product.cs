using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DTO;
using Interfaces;
using SportShop.Models;

namespace SportShop.Services
{
    public class ProductService : APIService, IService<Product, UpdateProductDto, CreateProductDto>
    {
        async public Task<Product[]> GetAll()
        {
            var data = await GetData($"""SELECT * FROM "product";""");
            Product[] products = MapData(data);
            return products;
        }

        async public Task<Product?> GetOne(string id)
        {
            var data = await GetData($"""SELECT * FROM "product" WHERE id = '{id}'""");
            Product product = MapDataRow(data.Rows[0]);
            return product;
        }

        async public Task<Product> Create(CreateProductDto dto)
        {
            var data = await GetData($"""INSERT INTO "product" (name, description, price) VALUES('{dto.Name}', '{dto.Description}', {dto.Price}) RETURNING *""");
            Product product = MapDataRow(data.Rows[0]);
            return await GetOne(product.Id);
        }

        async public Task<Product> Update(string id, UpdateProductDto dto)
        {
            var data = await GetData($"""UPDATE "product" SET name = '{dto.Name}', description = '{dto.Name}', price = {dto.Price}  WHERE "id" = '{id}' RETURNING *""");
            Product product = MapDataRow(data.Rows[0]);
            return await GetOne(product.Id);
        }

        async public void Delete(string id)
        {
            await GetData($"""DELETE FROM "product" WHERE "id" = '{id}' """);
        }

        public Product[] MapData(DataTable data)
        {
            List<Product> products = [];

            foreach (DataRow row in data.Rows)
            {
                Product product = MapDataRow(row);
                products.Add(product);
            }

            return [.. products];
        }

        public Product MapDataRow(DataRow apiRow)
        {
            return new Product { Id = apiRow["id"].ToString(), Name = apiRow["name"].ToString(), Description = apiRow["description"].ToString(), Price = double.Parse(apiRow["price"].ToString()) };
        }
    }
}