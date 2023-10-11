using FluentAssertions;
using NUnit.Framework;
using ProductsApp.Application.Models;
using ProductsApp.DataAccess.Entities;

namespace ProductsApp.UnitTests.Application
{
    public class ProductMapperUnitTests
    {

        [Test]
        public void Should_MapAllProductAttributes_WhenMappingIsNeeded()
        {
            //Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Price = 10,
                NumberOfPieces = 10
            };

            //Act
            var mappedProduct = ProductMapper.FromProduct(product);

            //Assert
            //Assert.AreEqual(mappedProduct.Id, product.Id);
            product.Id.Should().Be(mappedProduct.Id);
            product.Name.Should().Be(mappedProduct.Name);
            product.Price.Should().Be(mappedProduct.Price);
            product.NumberOfPieces.Should().Be(mappedProduct.NumberOfPieces);
        }
    }
}
