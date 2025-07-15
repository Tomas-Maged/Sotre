using Shared.ORderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<OrderResultDto> GetOrderByIDAsnyc(Guid Id);
        Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail);
       Task<OrderResultDto> CreateOrderAsync(OrderRquestDTO orderRequest, string userEmail);
        Task<IEnumerable<DeliveryMethodDTO>> GetAllDeliveryMethod();



    }
}
