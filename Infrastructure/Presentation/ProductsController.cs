using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
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
        public async Task<IActionResult> GetALlProducts([FromQuery]ProductSpecificationsParamters SpecParams)
        {
            var products = await servicesManager.ProudectServices.GetAllProductsAsync(SpecParams);
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
        [HttpGet("brandes")]
        public async Task<IActionResult> GetAllbrands(int brandId)
        {
            var brand= await servicesManager.ProudectServices.GetAllBrandessAsync();
            if (brand is null) return NotFound(); //404
            return Ok(brand); //200
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllTypes(int typeId)
        {
            var products = await servicesManager.ProudectServices.GetAllTypesAsync();
            if (products is null) return NotFound(); //404
            return Ok(products); //200
        }
    }
}
