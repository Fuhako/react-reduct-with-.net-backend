using backend.Context;
using backend.ProductModule.Model;
using backend.ProductModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductModule.Controller
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository, ApplicationDbContext context) 
        {
            _productRepository = productRepository;
        }

        [Route("api/[controller]/GetProduct")]
        [HttpGet]
        public IActionResult GetProduct()
        {
            var result = _productRepository.GetProducts();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/[controller]/GetProductById")]
        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            var result = _productRepository.GetProductById(id);
            if(result == null)
            {
                return NotFound("Product not found!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/CreateProduct")]
        [HttpPost]
        public IActionResult CreateProduct(Product product, string user)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }

            var result = _productRepository.CreateProduct(product, user);
            if (result == null)
            {
                return NotFound("Create Product Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/UpdateProductById")]
        [HttpPut]
        public IActionResult UpdateProductById(Product product, string user)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }

            var result = _productRepository.UpdateProductById(product, user);
            if (result == null)
            {
                return NotFound("Update Product Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/DeleteProductById")]
        [HttpDelete]
        public IActionResult DeleteProductById(long id)
        {
            if (id == 0)
            {
                return BadRequest("Id Product cannot be null");
            }

            var result = _productRepository.DeleteProductById(id);
            if (result == null)
            {
                return NotFound("Delete Product Failed!");
            }

            return Ok(result);
        }
    }
}
