using ProductsApp.DataAccess.Entities;

namespace ProductsApp.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Product CreateProduct(Product product)
        {
            var prod = this.databaseContext.Products.Add(product);
            this.databaseContext.SaveChanges();

            return prod.Entity;
        }

        public List<Product> GetAllProducts()
        {
            return this.databaseContext.Products.ToList();
        }

        public Product GetProductById(Guid id)
        {
            return this.databaseContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product UpdateProduct(Product product)
        {
            var prod = this.databaseContext.Products.Update(product);
            this.databaseContext.SaveChanges();

            return prod.Entity;
        }
    }
}
