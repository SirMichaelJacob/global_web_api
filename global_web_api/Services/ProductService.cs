using global_web_api.Context;
using global_web_api.Dtos;
using global_web_api.Interfaces;
using global_web_api.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace global_web_api.Services
{
    public class ProductService : IGeneric<ProductDto>
    {
        private readonly MyDbContext _dbContext;
        public ProductService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Business Logic to Add New product
        /// Returns HttpStatus Code
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Add(ProductDto productDto)
        {
            var newProduct = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
            };



            await Task.Run(async () =>
            {
                var res = await _dbContext.Products.AddAsync(newProduct);
                await _dbContext.SaveChangesAsync();
            });

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            return response;

        }

        /// <summary>
        /// Business Logic to Delete product
        /// Returns HttpStatus Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<HttpResponseMessage> Delete(int id)
        {

            var response = new HttpResponseMessage();
            Product product = await Task.Run(async () => await _dbContext.Products.FindAsync(id));


            if (product is null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new Exception(response.StatusCode.ToString())
                {

                };
            }
            else
            {
                //Implement Multi-threading
                await Task.Run(async () =>
                {
                    _dbContext.Products.Remove(product);
                    await _dbContext.SaveChangesAsync();
                });

                response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }



        }

        /// <summary>
        /// Business Logic to Fetch all products
        /// Returns String Content
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> GetAll()
        {
            await SimulateDelay();

            var allProducts = _dbContext.Products.ToList();
            var allProductsDto = allProducts.Select(p => new ProductDto { Name = p.Name!, Price = p.Price, Description = p.Description });
            // Serialize the list of ProductDto objects
            var json = JsonConvert.SerializeObject(allProductsDto);

            return new StringContent(json, Encoding.UTF8, "application/json");

        }

        /// <summary>
        /// Business Logic to Get product using Id
        /// Returns a Product Dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductDto> GetItem(int id)
        {

            try
            {
                var prod = new Product();
                await Task.Run(async () =>
                {
                    prod = await _dbContext.Products.FindAsync(id);

                });
                ProductDto prodDto = new ProductDto { Name = prod.Name!, Description = prod.Description, Price = prod.Price };
                return prodDto;

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Business Logic to Update a product
        /// Returns HttpStatus Code
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<HttpResponseMessage> Update(int id, ProductDto obj)
        {

            var response = new HttpResponseMessage();
            var prod = await Task.Run(async () => await _dbContext.Products.FindAsync(id));
            if (prod is null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new Exception(response.StatusCode.ToString())
                {

                };
            }
            await Task.Run(async () =>
            {
                prod.Name = obj.Name;
                prod.Price = obj.Price;
                prod.Description = obj.Description;

                await _dbContext.SaveChangesAsync();
            });
            response = new HttpResponseMessage(HttpStatusCode.OK);
            return response;
        }

        private Task SimulateDelay()
        {
            // Simulate delay (e.g., database operation, network call)
            return Task.Delay(1000);
        }
    }
}
