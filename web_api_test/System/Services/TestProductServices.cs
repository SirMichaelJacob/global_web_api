using FluentAssertions;
using global_web_api.Context;
using global_web_api.Dtos;
using global_web_api.Models;
using global_web_api.Services;
using Microsoft.EntityFrameworkCore;
using web_api_test.MockData;

namespace web_api_test.System.Services
{
    public class TestProductServices : IDisposable
    {
        private readonly MyDbContext _dbContext;
        public TestProductServices()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _dbContext = new MyDbContext(options);
            _dbContext.Database.EnsureCreated();
        }



        [Fact]

        public async Task Add_CheckLengthOfNewProductList()
        {
            var newProd = new Product
            {
                Name = MockProductData.AddProduct().Name,
                Price = MockProductData.AddProduct().Price,
                Description = MockProductData.AddProduct().Description
            };
            //Arrange
            await _dbContext.Products.AddRangeAsync(MockProductData.GetProducts());
            await _dbContext.SaveChangesAsync();
            var allProds = await _dbContext.Products.ToListAsync();
            await _dbContext.Products.AddAsync(newProd);
            await _dbContext.SaveChangesAsync();

            //allProds = await _dbContext.Products.ToListAsync();

            var sut = new ProductService(_dbContext);
            //Act
            var result = sut.Add(MockProductData.AddProduct());
            //Assert
            int expected = MockProductData.GetProducts().Count + 1;
            allProds = await _dbContext.Products.ToListAsync();
            allProds.Count.Should().Be(expected);

        }


        [Fact]
        public async Task GetItem_CheckTypeOfReturnProduct()
        {
            //Arrange
            await _dbContext.Products.AddRangeAsync(MockProductData.GetProducts());
            await _dbContext.SaveChangesAsync();
            var prods = await _dbContext.Products.ToListAsync();

            int prodId = MockProductData.ProductId();

            var prod = await _dbContext.Products.FindAsync(prodId);

            var sut = new ProductService(_dbContext);

            //Act
            var result = sut.GetItem(MockProductData.ProductId());
            //Assert
            prod.GetType().Should().Be(typeof(Product));
        }

        [Fact]
        public async Task DeleteItem_CompareProductListLengthWithOriginal()
        {
            //Arrange
            await _dbContext.Products.AddRangeAsync(MockProductData.GetProducts());
            await _dbContext.SaveChangesAsync();
            var prods = await _dbContext.Products.ToListAsync();

            int prodId = MockProductData.ProductId();

            var prod = await _dbContext.Products.FindAsync(prodId);

            _dbContext.Products.Remove(prod);
            await _dbContext.SaveChangesAsync();

            var sut = new ProductService(_dbContext);

            prods = await _dbContext.Products.ToListAsync();
            //Act
            var result = sut.Delete(MockProductData.ProductId());
            //Assert
            prods.Count.Should().BeLessThan(MockProductData.GetProducts().Count);
        }

        [Fact]
        public async Task GetAll_CheckLengthOfReturnedProductList()
        {
            ///Arrange
            await _dbContext.Products.AddRangeAsync(MockProductData.GetProducts());
            await _dbContext.SaveChangesAsync();
            var allProducts = await _dbContext.Products.ToListAsync();


            var allProDto = allProducts.Select(p => new ProductDto { Name = p.Name!, Price = p.Price, Description = p.Description });

            var systemUT = new ProductService(_dbContext);

            ///Act
            var result = systemUT.GetAll();

            ///Assert
            allProDto.ToList().Count.Equals(MockProductData.GetProducts().Count);


        }

        [Fact]
        public async Task Update_ReturnNameOfProduct()
        {
            //Arrange
            await _dbContext.Products.AddRangeAsync(MockProductData.GetProducts());
            await _dbContext.SaveChangesAsync();
            var allProducts = await _dbContext.Products.ToListAsync();
            var pId = MockProductData.ProductId();
            var productToEdit = await _dbContext.Products.FindAsync(pId);

            var newProductDto = new ProductDto
            {
                Name = "New Name",
                Description = "New Desc",
                Price = "909"
            };
            productToEdit!.Name = newProductDto.Name;

            await _dbContext.SaveChangesAsync();

            var sut = new ProductService(_dbContext);
            //Act
            var result = sut.Update(pId, newProductDto);


            //Assert
            var newProduct = await _dbContext.Products.FindAsync(pId);
            var edittedProduct = new Product();

            foreach (Product item in MockProductData.GetProducts())
            {
                if (item.Id == pId)
                {
                    edittedProduct = item;
                    break;
                }
            }

            newProduct!.Name.Should().NotBeSameAs(edittedProduct.Name);
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
