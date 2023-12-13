using global_web_api.Dtos;
using global_web_api.Extensions;
using global_web_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace global_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IGeneric<ProductDto> _productService;
        public ProductController(IGeneric<ProductDto> productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Endpoint to fetch products
        /// </summary>
        /// <returns></returns>
        [HttpGet("/products")]
        public async Task<IActionResult> GetProducts()
        {

            var allProducts = await _productService.GetAll();

            string jsonString;
            using (var streamReader = new StreamReader(await allProducts.ReadAsStreamAsync()))
            {
                jsonString = await streamReader.ReadToEndAsync();
            }
            // DeSerialize the jsonString to ProductDto objects
            var allProductsDto = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);

            return Ok(allProductsDto);
        }

        /// <summary>
        /// Endpoint to fetch  specific Product using product Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productService.GetItem(id);

            return Ok(result);
        }

        /// <summary>
        /// End point for creating new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("/products")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
        {

            var result = await _productService.Add(product);
            return Ok("Product successfully Added");

        }

        /// <summary>
        /// Endpoint to Update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPut("/products/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            var result = await _productService.Update(id, productDto);
            return Ok(result);
        }

        /// <summary>
        /// End point to delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/products/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            return Ok("Delete Sucessfull!");

        }

        /// <summary>
        /// Endpoint to Get Products >= price
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpGet("/get-products-by-price")]
        public async Task<List<ProductDto>> GetProductsAbove(int price)
        {
            var result = await _productService.GetProductsByPrice(price);
            return result;
        }

    }



}

