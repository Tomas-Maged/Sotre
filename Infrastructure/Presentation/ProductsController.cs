using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorsModeLs;
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
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status200OK , Type = typeof(PaginationResponse<ProudectResultDto>))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status500InternalServerError , Type = typeof(ErrorDetaiLs))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status400BadRequest , Type = typeof(ErrorDetaiLs))]
        public async Task<IActionResult> GetALlProducts([FromQuery]ProductSpecificationsParamters SpecParams)
        {
            var products = await servicesManager.ProudectServices.GetAllProductsAsync(SpecParams);
            return Ok(products); //200
        }

        [HttpGet("{id}")]

        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status200OK, Type = typeof(ProudectResultDto))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetaiLs))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetaiLs))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status404NotFound, Type = typeof(ErrorDetaiLs))]
        public async Task<ActionResult<ProudectResultDto>> GetProductById(int id)
        {
            var product = await servicesManager.ProudectServices.GetProductByIdAsync(id);

            return Ok(product); //200
        }
        [HttpGet("brandes")]

        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetaiLs))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetaiLs))]
        public async Task<ActionResult<BrandResultDto>> GetAllbrands()
        {
            var brand= await servicesManager.ProudectServices.GetAllBrandessAsync();
            if (brand is null) return NotFound(); //404
            return Ok(brand); //200
        }
        [HttpGet("Types")]

        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetaiLs))]
        [ProducesResponseType<PaginationResponse<ProudectResultDto>>(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetaiLs))]
        public async Task<ActionResult<TypeResultDto>> GetAllTypes()
        {
            var products = await servicesManager.ProudectServices.GetAllTypesAsync();
            if (products is null) return NotFound(); //404
            return Ok(products); //200
        }
    }
}
