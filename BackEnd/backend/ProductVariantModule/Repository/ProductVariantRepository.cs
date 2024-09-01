using backend.Context;
using backend.ProductVariantModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductVariantModule.Repository
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductVariantRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        public List<ProductVariant> GetProductVariant()
        {
            return _context.ProductVariant.OrderBy(p => p.id).ToList();
        }
        
        public ProductVariant GetProductVariantById(long id)
        {
            return _context.ProductVariant.Where(a => a.id == id).FirstOrDefault();
        }

        public string CreateProductVariant(ProductVariant ProductVariant, string user)
        {
            try
            {
                ProductVariant.active = true;
                ProductVariant.created_date = DateTime.Now;
                ProductVariant.created_user = user;

                _context.ProductVariant.Add(ProductVariant);
                _context.SaveChanges(); 

                return "Insert ProductVariant Success!";
            }
            catch (Exception ex)
            {
                return $"Insert ProductVariant Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateProductVariantById(ProductVariant ProductVariant, string user)
        {
            try
            {
                // Fetch the existing ProductVariant from the database
                var existingProductVariant = GetProductVariantById(ProductVariant.id);
                if (existingProductVariant == null)
                {
                    // ProductVariant with the given ID does not exist
                    return "ProductVariant doesnt exists!";
                }

                // Update properties
                existingProductVariant.name = ProductVariant.name;
                existingProductVariant.price = ProductVariant.price;
                existingProductVariant.qty = ProductVariant.qty;
                existingProductVariant.code = ProductVariant.code;
                existingProductVariant.active = ProductVariant.active;
                existingProductVariant.updated_user = user;
                existingProductVariant.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.ProductVariant.Update(existingProductVariant);
                _context.SaveChanges();
                return "Update ProductVariant Success!";
            }
            catch (Exception ex)
            {
                return $"Update ProductVariant Failed with Error :{ex.InnerException}"; // Failure
            }

        }

        public string DeleteProductVariantById(long id)
        {
            try
            {

                // Fetch the existing ProductVariant from the database
                var existingProductVariant = GetProductVariantById(id);
                if (existingProductVariant == null)
                {
                    // ProductVariant with the given ID does not exist
                    return "ProductVariant doesnt exists!";
                }

                // Mark the entity for deletion
                _context.ProductVariant.Remove(existingProductVariant);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete ProductVariant Success!";
            }
            catch (Exception ex)
            {
                return $"Delete ProductVariant Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
