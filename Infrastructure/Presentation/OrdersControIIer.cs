using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.ORderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRquestDTO rquest)
        {
           var Email = User.FindFirstValue(ClaimTypes.Email);
           var Result = await servicesManager.OrderService.CreateOrderAsync(rquest,Email);
            return Ok (Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await servicesManager.OrderService.GetOrdersByUserEmailAsync(Email);
            return Ok(Result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(Guid Id)
        {
            var Result = await servicesManager.OrderService.GetOrderByIDAsnyc(Id);
            return Ok(Result);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await servicesManager.OrderService.GetOrdersByUserEmailAsync(Email);
            return Ok(Result);
        }
    }
}
