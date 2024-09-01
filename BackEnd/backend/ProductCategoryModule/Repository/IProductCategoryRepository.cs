using backend.ProductCategoryModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductCategoryModule.Repository
{
    public interface IProductCategoryRepository
    {
        List<ProductCategory> GetProductCategorys();
        ProductCategory GetProductCategoryById(long id);
        string CreateProductCategory(ProductCategory ProductCategory, string user);
        string UpdateProductCategoryById(ProductCategory ProductCategory, string user);
        string DeleteProductCategoryById(long id);
    }
}
