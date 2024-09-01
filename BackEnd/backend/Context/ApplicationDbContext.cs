using Microsoft.EntityFrameworkCore;
using backend.ProductModule.Model;
namespace backend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
    }
}
