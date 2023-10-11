using ProductsApp.Application.Exceptions;
using ProductsApp.Application.Models;
using ProductsApp.DataAccess.Entities;
using ProductsApp.DataAccess.Repositories;

namespace ProductsApp.Application.Services
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductResponseModel CreateProduct(CreateProductRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new InputValidationException("Name cannot be null or empty.");
            }

            if (model.Name.Length < 3)
            {
                throw new InputValidationException("Name should have length greater than 3.");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                NumberOfPieces = model.NumberOfPieces
            };

            var createdProduct = this.productRepository.CreateProduct(product);

            return ProductMapper.FromProduct(createdProduct);
        }

        public ProductResponseModel GetProductById(Guid id)
        {
            var product = this.productRepository.GetProductById(id);

            if (product == null)
            {
                throw new NotFoundException($"The Product with id {id} does not exist!");
            }

            return ProductMapper.FromProduct(product);
        }

        public List<ProductResponseModel> GetAllProducts()
        {
            var products = this.productRepository.GetAllProducts();

            return products.Select(product =>
            ProductMapper.FromProduct(product)).ToList();
        }

        public ProductResponseModel UpdateProduct(Guid id, UpdateProductRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new InputValidationException("Name cannot be null or empty.");
            }

            if (model.Name.Length < 3)
            {
                throw new InputValidationException("Name should have length greater than 3.");
            }

            if (model.Price <= 0)
            {
                throw new InputValidationException("Price should be greater than 0!");
            }

            var product = this.productRepository.GetProductById(id);

            if (product == null)
            {
                throw new NotFoundException($"The Product with id {id} does not exist!");
            }

            var newNumberOfPieces = product.NumberOfPieces - model.NumberOfPieces;
            if (newNumberOfPieces < 0)
            {
                throw new InputValidationException("You are trying to remove too many pieces!");
            }

            product.Name = model.Name;
            product.Price = model.Price;
            product.NumberOfPieces = newNumberOfPieces;

            var updatedProduct = this.productRepository.UpdateProduct(product);

            return ProductMapper.FromProduct(updatedProduct);
        }
    }
}
