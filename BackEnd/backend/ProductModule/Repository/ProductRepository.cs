using backend.Context;
using backend.ProductModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductModule.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        public List<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.id).ToList();
        }
        
        public Product GetProductById(long id)
        {
            return _context.Products.Where(a => a.id == id).FirstOrDefault();
        }

        public string CreateProduct(Product product, string user)
        {
            try
            {
                product.active = true;
                product.created_date = DateTime.Now;
                product.created_user = user;

                _context.Products.Add(product);
                _context.SaveChanges(); 

                return "Create Product Success!";
            }
            catch (Exception ex)
            {
                return $"Create Product Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateProductById(Product product, string user)
        {
            try
            {
                // Fetch the existing product from the database
                var existingProduct = GetProductById(product.id);
                if (existingProduct == null)
                {
                    // Product with the given ID does not exist
                    return "Product doesnt exists!";
                }

                // Update properties
                existingProduct.plu = product.plu;
                existingProduct.product_category_id = product.product_category_id;
                existingProduct.active = product.active;
                existingProduct.updated_user = user;
                existingProduct.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.Products.Update(existingProduct);
                _context.SaveChanges();
                return "Update Product Success!";
            }
            catch (Exception ex)
            {
                return $"Update Product Failed with Error :{ex.InnerException}"; // Failure
            }

        }

        public string DeleteProductById(long id)
        {
            try
            {

                // Fetch the existing product from the database
                var existingProduct = GetProductById(id);
                if (existingProduct == null)
                {
                    // Product with the given ID does not exist
                    return "Product doesnt exists!";
                }

                // Mark the entity for deletion
                _context.Products.Remove(existingProduct);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete Product Success!";
            }
            catch (Exception ex)
            {
                return $"Delete Product Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
