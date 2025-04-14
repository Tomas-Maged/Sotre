using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetALlProducts()
        {
            var products = await servicesManager.ProudectServices.GetAllProductsAsync();
            if (products is null) return BadRequest(); //400
            return Ok(products); //200
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await servicesManager.ProudectServices.GetProductByIdAsync(id);
            if (product is null) return NotFound(); //404
            return Ok(product); //200
        }
        [HttpGet]
        [Route("GetProductByBrandId")]
        public async Task<IActionResult> GetProductByBrandId(int brandId)
        {
            var products = await servicesManager.ProudectServices.GetProductByIdAsync(brandId);
            if (products is null) return NotFound(); //404
            return Ok(products); //200
        }
        [HttpGet]
        [Route("GetProductByTypeId")]
        public async Task<IActionResult> GetProductByTypeId(int typeId)
        {
            var products = await servicesManager.ProudectServices.GetProductByIdAsync(typeId);
            if (products is null) return NotFound(); //404
            return Ok(products); //200
        }
    }
}
