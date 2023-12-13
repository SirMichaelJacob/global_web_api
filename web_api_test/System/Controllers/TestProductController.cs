using FluentAssertions;
using global_web_api.Controllers;
using global_web_api.Dtos;
using global_web_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using web_api_test.MockData;

namespace web_api_test.System.Controllers
{
    public class TestProductController
    {
        /// <summary>
        /// Test Method for GetAll in Products Controller
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetProducts_ShouldReturnStatusCode200()
        {
            //Arrange
            var _productService = new Mock<IGeneric<ProductDto>>();
            _productService.Setup(x => x.GetAll()).Returns(MockProductData.GetProductsAsContent());

            var sut = new ProductController(_productService.Object);

            //Act
            var result = await sut.GetProducts();

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]

        public async Task AddProduct_ShouldAddNewProductOnce()
        {
            //Arrange
            var _productService = new Mock<IGeneric<ProductDto>>();
            var newProduct = MockProductData.AddProduct();
            var sut = new ProductController(_productService.Object);
            //Act
            var result = await sut.AddProduct(newProduct);

            //Assert
            _productService.Verify(x => x.Add(newProduct), Times.Exactly(1));
        }

        [Fact]
        public async Task GetItem_ReturnProduct()
        {
            //Arrange
            var _productService = new Mock<IGeneric<ProductDto>>();
            var prodId = MockProductData.ProductId();
            var sut = new ProductController(_productService.Object);

            //Act
            var result = await sut.GetProduct(prodId);


            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }


        /// <summary>
        /// Test Method for product controller Update Product method
        /// </summary>
        /// <returns></returns>
        [Fact]

        public async Task UpdateProduct_ReturnOKResponseType()
        {
            //Arrange
            var _productService = new Mock<IGeneric<ProductDto>>();
            var prodId = MockProductData.ProductId();
            var data = MockProductData.AddProduct();

            var prod = _productService.Setup(x => x.Update(prodId, data));

            var sut = new ProductController(_productService.Object);

            //Act
            var result = await sut.Update(prodId, MockProductData.AddProduct());


            //Assert
            Assert.NotNull(prod);
            result.GetType().Should().Be(typeof(OkObjectResult));
        }


        /// <summary>
        /// TestMethod for product controller Delete Product Method
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteProduct_ReturnResponseType()
        {
            //Arrange
            var _productService = new Mock<IGeneric<ProductDto>>();

            var prodId = MockProductData.ProductId();


            //Act
            var sut = new ProductController(_productService.Object);
            var result = await sut.Delete(prodId);

            //Assert
            Assert.NotNull(result);
            result.GetType().Should().Be(typeof(OkObjectResult));

        }


        /// <summary>
        /// Test Method for GetProductByPrice
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task GetProductByPrice_ReturnStatusCode200()
        {
            //Arrange
            var _productService = new Mock<IGeneric<ProductDto>>();

            var price = 1000;

            var res = _productService.Setup(x => x.GetAll()).Returns(MockProductData.GetProductsWithPriceFrom(price));

            //Act
            var sut = new ProductController(_productService.Object);

            var result = await sut.GetProductsAbove(price);

            //Assert
            res.Should().NotBeNull();
            result.GetType().Should().Be(typeof(List<ProductDto>));
        }
    }
}
