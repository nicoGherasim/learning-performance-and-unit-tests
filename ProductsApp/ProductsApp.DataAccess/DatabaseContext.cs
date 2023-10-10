using Microsoft.EntityFrameworkCore;
using ProductsApp.DataAccess.Entities;
using System.Collections.Generic;

namespace ProductsApp.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}
