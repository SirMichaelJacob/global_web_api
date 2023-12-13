using global_web_api.Dtos;
using global_web_api.Interfaces;
using Newtonsoft.Json;

namespace global_web_api.Extensions
{/// <summary>
/// Product Extension class
/// </summary>
    public static class ProductsExtension
    {
        /// <summary>
        /// Product Extension method. Fetches Produts with prices greater than or equal to specified 'price'
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static async Task<List<ProductDto>> GetProductsByPrice(this IGeneric<ProductDto> productService, int price)
        {
            var allProductsString = await productService.GetAll();
            string jsonString;
            using (var streamReader = new StreamReader(await allProductsString.ReadAsStreamAsync()))
            {
                jsonString = await streamReader.ReadToEndAsync();
            }

            var allProducts = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);

            var result = allProducts!.Where(p => int.Parse(p.Price) >= price).ToList();
            return result;
        }
    }
}
