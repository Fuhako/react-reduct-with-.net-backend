using backend.ProductModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductModule.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(long id);
        string CreateProduct(Product product, string user);
        string UpdateProductById(Product product, string user);
        string DeleteProductById(long id);
    }
}
