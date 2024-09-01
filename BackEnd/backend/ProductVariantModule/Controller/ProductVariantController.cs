using backend.Context;
using backend.ProductVariantModule.Model;
using backend.ProductVariantModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.ProductVariantModule.Controller
{
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantRepository _ProductVariantRepository;
        public ProductVariantController(IProductVariantRepository ProductVariantRepository, ApplicationDbContext context) 
        {
            _ProductVariantRepository = ProductVariantRepository;
        }

        [Route("api/GetProductVariant")]
        [HttpGet]
        public IActionResult GetProductVariant()
        {
            var result = _ProductVariantRepository.GetProductVariant();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/GetProductVariantById")]
        [HttpGet]
        public IActionResult GetProductVariantById(int id)
        {
            var result = _ProductVariantRepository.GetProductVariantById(id);
            if(result == null)
            {
                return NotFound("ProductVariant not found!");
            }

            return Ok(result);
        }

        [Route("api/CreateProductVariant")]
        [HttpPost]
        public IActionResult CreateProductVariant(ProductVariant ProductVariant, string user)
        {
            if (ProductVariant == null)
            {
                return BadRequest("ProductVariant cannot be null");
            }

            var result = _ProductVariantRepository.CreateProductVariant(ProductVariant, user);
            if (result == null)
            {
                return NotFound("Create ProductVariant Failed!");
            }

            return Ok(result);
        }

        [Route("api/UpdateProductVariantById")]
        [HttpPut]
        public IActionResult UpdateProductVariantById(ProductVariant ProductVariant, string user)
        {
            if (ProductVariant == null)
            {
                return BadRequest("ProductVariant cannot be null");
            }

            var result = _ProductVariantRepository.UpdateProductVariantById(ProductVariant, user);
            if (result == null)
            {
                return NotFound("Update ProductVariant Failed!");
            }

            return Ok(result);
        }

        [Route("api/DeleteProductVariantById")]
        [HttpDelete]
        public IActionResult DeleteProductVariantById(long id)
        {
            if (id == 0)
            {
                return BadRequest("Id ProductVariant cannot be null");
            }

            var result = _ProductVariantRepository.DeleteProductVariantById(id);
            if (result == null)
            {
                return NotFound("Delete ProductVariant Failed!");
            }

            return Ok(result);
        }
    }
}
