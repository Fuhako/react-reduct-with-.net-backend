using backend.ProductVariantModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductVariantModule.Repository
{
    public interface IProductVariantRepository
    {
        List<ProductVariant> GetProductVariant();
        ProductVariant GetProductVariantById(long id);
        string CreateProductVariant(ProductVariant ProductVariant, string user);
        string UpdateProductVariantById(ProductVariant ProductVariant, string user);
        string DeleteProductVariantById(long id);
    }
}
