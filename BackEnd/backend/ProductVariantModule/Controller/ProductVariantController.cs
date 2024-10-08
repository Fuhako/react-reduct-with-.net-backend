﻿using backend.Context;
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

        [Route("api/[controller]/GetProductVariant")]
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

        [Route("api/[controller]/GetProductVariantById")]
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

        [Route("api/[controller]/CreateProductVariant")]
        [HttpPost]
        public IActionResult CreateProductVariant(ProductVariant ProductVariant)
        {
            if (ProductVariant == null)
            {
                return BadRequest("ProductVariant cannot be null");
            }

            var result = _ProductVariantRepository.CreateProductVariant(ProductVariant, ProductVariant.created_user);
            if (result == null)
            {
                return NotFound("Create ProductVariant Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/UpdateProductVariantById")]
        [HttpPut]
        public IActionResult UpdateProductVariantById(ProductVariant ProductVariant)
        {
            if (ProductVariant == null)
            {
                return BadRequest("ProductVariant cannot be null");
            }

            var result = _ProductVariantRepository.UpdateProductVariantById(ProductVariant, ProductVariant.created_user);
            if (result == null)
            {
                return NotFound("Update ProductVariant Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/DeleteProductVariantById")]
        [HttpDelete]
        public IActionResult DeleteProductVariantById([FromQuery] long id)
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
