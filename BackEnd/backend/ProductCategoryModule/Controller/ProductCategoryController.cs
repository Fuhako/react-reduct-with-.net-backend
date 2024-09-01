using backend.Context;
using backend.ProductCategoryModule.Model;
using backend.ProductCategoryModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductCategoryModule.Controller
{
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _ProductCategoryRepository;
        public ProductCategoryController(IProductCategoryRepository ProductCategoryRepository, ApplicationDbContext context) 
        {
            _ProductCategoryRepository = ProductCategoryRepository;
        }

        [Route("api/GetProductCategory")]
        [HttpGet]
        public IActionResult GetProductCategory()
        {
            var result = _ProductCategoryRepository.GetProductCategorys();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/GetProductCategoryById")]
        [HttpGet]
        public IActionResult GetProductCategoryById(int id)
        {
            var result = _ProductCategoryRepository.GetProductCategoryById(id);
            if(result == null)
            {
                return NotFound("ProductCategory not found!");
            }

            return Ok(result);
        }

        [Route("api/CreateProductCategory")]
        [HttpPost]
        public IActionResult CreateProductCategory(ProductCategory ProductCategory, string user)
        {
            if (ProductCategory == null)
            {
                return BadRequest("ProductCategory cannot be null");
            }

            var result = _ProductCategoryRepository.CreateProductCategory(ProductCategory, user);
            if (result == null)
            {
                return NotFound("Create ProductCategory Failed!");
            }

            return Ok(result);
        }

        [Route("api/UpdateProductCategoryById")]
        [HttpPut]
        public IActionResult UpdateProductCategoryById(ProductCategory ProductCategory, string user)
        {
            if (ProductCategory == null)
            {
                return BadRequest("ProductCategory cannot be null");
            }

            var result = _ProductCategoryRepository.UpdateProductCategoryById(ProductCategory, user);
            if (result == null)
            {
                return NotFound("Update ProductCategory Failed!");
            }

            return Ok(result);
        }

        [Route("api/DeleteProductCategoryById")]
        [HttpDelete]
        public IActionResult DeleteProductCategoryById(long id)
        {
            if (id == 0)
            {
                return BadRequest("Id ProductCategory cannot be null");
            }

            var result = _ProductCategoryRepository.DeleteProductCategoryById(id);
            if (result == null)
            {
                return NotFound("Delete ProductCategory Failed!");
            }

            return Ok(result);
        }
    }
}
