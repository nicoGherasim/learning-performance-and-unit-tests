using ProductsApp.Application.Models;

namespace ProductsApp.Application.Services
{
    public interface IProductService
    {
        List<ProductResponseModel> GetAllProducts();
        ProductResponseModel GetProductById(Guid id);
        ProductResponseModel CreateProduct(CreateProductRequestModel model);
        public ProductResponseModel UpdateProduct(Guid id, UpdateProductRequestModel model);

    }
}
