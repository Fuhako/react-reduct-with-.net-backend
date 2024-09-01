using Microsoft.EntityFrameworkCore;
using backend.ProductModule.Model;
using backend.ProductCategoryModule.Model;
using backend.ProductVariantModule.Model;
using backend.UserModule.Model;
namespace backend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductVariant> ProductVariant { get; set; }
        public DbSet<User> User { get; set; }



    }
}
