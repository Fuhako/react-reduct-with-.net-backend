using backend.Context;
using backend.ProductCategoryModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductCategoryModule.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductCategoryRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        public List<ProductCategory> GetProductCategorys()
        {
            return _context.ProductCategory.OrderBy(p => p.id).ToList();
        }
        
        public ProductCategory GetProductCategoryById(long id)
        {
            return _context.ProductCategory.Where(a => a.id == id).FirstOrDefault();
        }

        public string CreateProductCategory(ProductCategory ProductCategory, string user)
        {
            try
            {
                ProductCategory.active = true;
                ProductCategory.created_date = DateTime.Now;
                ProductCategory.created_user = user;

                _context.ProductCategory.Add(ProductCategory);
                _context.SaveChanges(); 

                return "Insert ProductCategory Success!";
            }
            catch (Exception ex)
            {
                return $"Insert ProductCategory Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateProductCategoryById(ProductCategory ProductCategory, string user)
        {
            try
            {
                // Fetch the existing ProductCategory from the database
                var existingProductCategory = GetProductCategoryById(ProductCategory.id);
                if (existingProductCategory == null)
                {
                    // ProductCategory with the given ID does not exist
                    return "ProductCategory doesnt exists!";
                }

                // Update properties
                existingProductCategory.name = ProductCategory.name;
                existingProductCategory.active = ProductCategory.active;
                existingProductCategory.updated_user = user;
                existingProductCategory.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.ProductCategory.Update(existingProductCategory);
                _context.SaveChanges();
                return "Update ProductCategory Success!";
            }
            catch (Exception ex)
            {
                return $"Update ProductCategory Failed with Error :{ex.InnerException}"; // Failure
            }

        }

        public string DeleteProductCategoryById(long id)
        {
            try
            {

                // Fetch the existing ProductCategory from the database
                var existingProductCategory = GetProductCategoryById(id);
                if (existingProductCategory == null)
                {
                    // ProductCategory with the given ID does not exist
                    return "ProductCategory doesnt exists!";
                }

                // Mark the entity for deletion
                _context.ProductCategory.Remove(existingProductCategory);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete ProductCategory Success!";
            }
            catch (Exception ex)
            {
                return $"Delete ProductCategory Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
