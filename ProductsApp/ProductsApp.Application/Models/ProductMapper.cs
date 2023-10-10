using ProductsApp.DataAccess.Entities;

namespace ProductsApp.Application.Models
{
    public static class ProductMapper
    {
        public static ProductResponseModel FromProduct(Product product)
        {
            return new ProductResponseModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                NumberOfPieces = product.NumberOfPieces
            };
        }
    }
}
