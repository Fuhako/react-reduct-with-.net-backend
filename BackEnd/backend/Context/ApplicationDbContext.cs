using Microsoft.EntityFrameworkCore;
using backend.ProductModule.Model;
using backend.ProductCategoryModule.Model;
using backend.ProductVariantModule.Model;
using backend.UserModule.Model;
using backend.MenuModule.Model;
using backend.MenuAccessModule.Model;
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
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuAccess> MenuAccess { get; set; }


    }
}
