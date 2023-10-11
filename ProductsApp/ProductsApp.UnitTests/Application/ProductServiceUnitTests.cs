using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ProductsApp.Application.Exceptions;
using ProductsApp.Application.Models;
using ProductsApp.Application.Services;
using ProductsApp.DataAccess.Entities;
using ProductsApp.DataAccess.Repositories;

namespace ProductsApp.UnitTests.Application
{
    public class ProductServiceUnitTests
    {
        private ProductService productService;
        private IProductRepository productRepository;

        [SetUp]
        public void Setup()
        {
            productRepository = Substitute.For<IProductRepository>();
            productService = new ProductService(productRepository);
        }

        [Test]
        public void Should_CreateAProduct_When_ValidInformationIsProvidedForAProduct()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new CreateProductRequestModel
            {
                Name = "name",
                Price = 1,
                NumberOfPieces = 1,
            };
            productRepository.CreateProduct(Arg.Any<Product>()).Returns(
                new Product
                {
                    Id = id,
                    Name = "name",
                    Price = 1,
                    NumberOfPieces = 1
                });

            //Act
            productService.CreateProduct(product);

            //Assert
            //productRepository.Received(1).CreateProduct(Arg.Any<Product>());
            productRepository.Received(1).CreateProduct(Arg.Is<Product>(p => p.Name == product.Name
            && p.Price == product.Price
            && p.NumberOfPieces == product.NumberOfPieces));
        }

        [Test]
        public void Should_ThrowException_When_NullNameIsProvidedForAProduct()
        {
            //Arrange
            var product = new CreateProductRequestModel
            {
                Name = null,
                Price = 1,
                NumberOfPieces = 1,
            };

            //Act
            //Assert
            var exception = Assert.Throws<InputValidationException>(() => productService.CreateProduct(product));
            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty."));
        }

        [Test]
        public void Should_ThrowException_When_EmptyNameIsProvidedForAProduct()
        {
            //Arrange
            var product = new CreateProductRequestModel
            {
                Name = "",
                Price = 1,
                NumberOfPieces = 1,
            };

            //Act
            //Assert
            var exception = Assert.Throws<InputValidationException>(() => productService.CreateProduct(product));
            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty."));
        }

        [Test]
        public void Should_ThrowException_When_TooShortNameIsProvidedForAProduct()
        {
            //Arrange
            var product = new CreateProductRequestModel
            {
                Name = "na",
                Price = 1,
                NumberOfPieces = 1,
            };

            //Act
            //Assert
            var exception = Assert.Throws<InputValidationException>(() => productService.CreateProduct(product));
            Assert.That(exception.Message, Is.EqualTo("Name should have length greater than 3."));
        }

        [Test]
        public void Should_ReturnAProduct_When_GetProductByIdIsCalledWithValidId()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = "name",
                Price = 1,
                NumberOfPieces = 1
            };
            productRepository.GetProductById(id).Returns(product);

            //Act
            var productById = productService.GetProductById(id);

            //Assert
            productById.Id.Equals(product.Id);
            productById.Name.Equals(product.Name);
            productById.Price.Equals(product.Price);
            productById.NumberOfPieces.Equals(product.NumberOfPieces);
        }

        [Test]
        public void Should_ThrowException_When_GetProductByIdIsCalledWithInvalidId()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = "name",
                Price = 1,
                NumberOfPieces = 1
            };
            productRepository.GetProductById(id).ReturnsNull();

            //Act
            //Assert
            var exception = Assert.Throws<NotFoundException>(() => productService.GetProductById(id));
            Assert.That(exception.Message, Is.EqualTo($"The Product with id {id} does not exist!"));
        }

        [Test]
        public void Should_ReturnAListOfProduct_When_GetAllProductsIsCalled()
        {
            //Arrange
            var products = new List<Product>(){
                new Product
            {
                Id = Guid.NewGuid(),
                Name = "name1",
                Price = 1,
                NumberOfPieces = 1
            },

                new Product
            {
                Id = Guid.NewGuid(),
                Name = "name2",
                Price = 2,
                NumberOfPieces = 2
            }};
            productRepository.GetAllProducts().Returns(products);

            //Act
            var allProducts = productService.GetAllProducts();

            //Assert
            productRepository.Received(1).GetAllProducts();
            allProducts.Count.Equals(2);
        }


        [Test]
        public void Should_ReturnAnEmptyList_When_NoProductsInTheDatabase()
        {
            //Arrange
            productRepository.GetAllProducts().Returns(new List<Product>());

            //Act
            var allProducts = productService.GetAllProducts();

            //Assert
            productRepository.Received(1).GetAllProducts();
            allProducts.Count.Equals(0);
        }

        [Test]
        public void Should_UpdateAProduct_When_ValidDataIsProvidedForAnUpdate()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new UpdateProductRequestModel
            {
                Name = "name",
                Price = 1,
                NumberOfPieces = 1
            };
            productRepository.GetProductById(id).Returns(
                new Product
                {
                    Id = id,
                    Name = "oldName",
                    Price = 2,
                    NumberOfPieces = 2
                });
            productRepository.UpdateProduct(Arg.Any<Product>()).Returns(
                new Product());

            //Act
            productService.UpdateProduct(id, product);

            //Assert
            productRepository.Received(1).UpdateProduct(Arg.Is<Product>(p => p.Name == product.Name
            && p.Price == product.Price
            && p.NumberOfPieces == product.NumberOfPieces));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Should_ThrowException_When_UpdateProductIsCalledWithNullOrEmptyName(string name)
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new UpdateProductRequestModel
            {
                Name = name,
                Price = 1,
                NumberOfPieces = 1
            };

            //Act
            //Assert
            var exception = Assert.Throws<InputValidationException>(() => productService.UpdateProduct(id, product));
            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty."));
        }

        [TestCase("n")]
        [TestCase("na")]
        public void Should_ThrowException_When_UpdateProductIsCalledWithTooShortName(string name)
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new UpdateProductRequestModel
            {
                Name = name,
                Price = 1,
                NumberOfPieces = 1
            };

            //Act
            //Assert
            var exception = Assert.Throws<InputValidationException>(() => productService.UpdateProduct(id, product));
            Assert.That(exception.Message, Is.EqualTo("Name should have length greater than 3."));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-20000)]
        public void Should_ThrowException_When_UpdateProductIsCalledWithPriceLowerOrEqualToZero(int price)
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new UpdateProductRequestModel
            {
                Name = "name",
                Price = price,
                NumberOfPieces = 1
            };

            //Act
            //Assert
            var exception = Assert.Throws<InputValidationException>(() => productService.UpdateProduct(id, product));
            Assert.That(exception.Message, Is.EqualTo("Price should be greater than 0!"));
        }

        [Test]
        public void Should_ThrowException_When_TryingToUpdateAProductThatDoesNotExist()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new UpdateProductRequestModel
            {
                Name = "name",
                Price = 1,
                NumberOfPieces = 1
            };
            productRepository.GetProductById(id).ReturnsNull();

            //Act
            //Assert
            var exception = Assert.Throws<NotFoundException>(() => productService.UpdateProduct(id, product));
            Assert.That(exception.Message, Is.EqualTo($"The Product with id {id} does not exist!"));
        }

        [Test]
        public void Should_ThrowException_When_UpdateProductIsCalledWithTooHighNumberOfPieces()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = new UpdateProductRequestModel
            {
                Name = "name",
                Price = 1,
                NumberOfPieces = 10
            };
            productRepository.GetProductById(id).Returns(
                new Product
                {
                    Id = id,
                    Name = "name",
                    Price = 1,
                    NumberOfPieces = 1
                });

            //Act
            //Assert
            var exception = Assert.Throws<InputValidationException>(() => productService.UpdateProduct(id, product));
            Assert.That(exception.Message, Is.EqualTo("You are trying to remove too many pieces!"));
        }
    }
}
