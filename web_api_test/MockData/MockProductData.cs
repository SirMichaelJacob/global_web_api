using global_web_api.Dtos;
using global_web_api.Models;
using Newtonsoft.Json;
using System.Text;

namespace web_api_test.MockData
{
    public class MockProductData
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>(){
                new Product{
                    Id = 1,
                    Name="Iphone 15",
                    Description="Apple Phone",
                    Price = "1500",
                    CreatedAt = DateTime.Now,
                    updatedAt  = DateTime.Now
                },
                new Product{
                    Id = 2,
                    Name="Dell Xps",
                    Description="Laptop",
                    Price = "960",
                    CreatedAt = DateTime.Now,
                    updatedAt  = DateTime.Now
                },
                new Product{
                    Id = 3,
                    Name="Hp Elite Book",
                    Description="Laptop",
                    Price = "890",
                    CreatedAt = DateTime.Now,
                    updatedAt  = DateTime.Now
                }
            };

            return products;

        }

        public static List<ProductDto> EmptyProducts()
        {
            var products = new List<ProductDto>() { };
            return products;
        }

        public static ProductDto AddProduct()
        {
            return new ProductDto
            {
                Name = "Test",
                Price = "240",
                Description = "New Product"
            };
        }

        public static int ProductId()
        {
            // Create an instance of Random
            Random random = new Random();

            // Generate a random integer within the specified range
            int randomNumber = random.Next(0, GetProducts().Count);

            return GetProducts()[randomNumber].Id;

        }

        public static async Task<HttpContent> GetProductsAsContent()
        {
            var allProducts = GetProducts();
            var allProductsDto = allProducts.Select(p => new ProductDto { Name = p.Name!, Price = p.Price, Description = p.Description });
            //Serialize
            var jsonString = JsonConvert.SerializeObject(allProductsDto);


            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }

        public static async Task<HttpContent> GetProductsWithPriceFrom(int Price)
        {
            var allProducts = GetProducts();
            var allProductsDto = allProducts.Where(p => int.Parse(p.Price) >= Price).ToList().Select(p => new ProductDto { Name = p.Name!, Price = p.Price, Description = p.Description });
            //Serialize
            var jsonString = JsonConvert.SerializeObject(allProductsDto);


            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }


    }
}
