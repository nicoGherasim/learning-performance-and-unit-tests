using ProductsApp.DataAccess.Entities;

namespace ProductsApp.DataAccess.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(Guid id);
        Product CreateProduct(Product product);
        Product UpdateProduct(Product product);

    }
}
