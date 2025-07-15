using AutoMapper;
using Domian.Contercts;
using Domian.Exceptions;
using Domian.Models;
using Domian.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared.ORderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper mapper
        ,IBasketRepository basketRepository
        ,IUnitOfWork unitOfWork
        ) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRquestDTO orderRequest, string userEmail)
        {
            //Address 
          var adress =  mapper.Map<Address>(orderRequest.ShippToAddress);
            //Basket
            var basket = await basketRepository.GetBaskedAsync(orderRequest.BasketId);
            if (basket == null) throw new BasketNotFoundException(orderRequest.BasketId);

            var OrderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var proudect = await unitOfWork.GenericRepository<Proudect,int>().GetAsync(item.Id);
                if (proudect == null) throw new ProductNotFoundExceptions(item.Id);
                var orderitem = new OrderItem(new proudectInOrderItem(proudect.Id,proudect.Name,proudect.PictureUrl),proudect.Price,item.Quantity);
                OrderItems.Add(orderitem);
            }
            //Get Delivery Method
            var deliveryMethod = await unitOfWork.GenericRepository<DeliveryMethod, int>().GetAsync(orderRequest.DeliveryMethodId);
            if (deliveryMethod == null) throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            var subTotal = OrderItems.Sum(x => x.Price * x.Quantity);

            var order = new order(userEmail,adress,OrderItems,deliveryMethod,subTotal,"");
           await unitOfWork.GenericRepository<order, Guid>().AddAsync(order);
           var count = await unitOfWork.SaveChangesAsync();
            if (count == 0) throw new OrderCreateBadRequestException();
          var result = mapper.Map<OrderResultDto>(order);
            return result;
        }
         
        public async Task<IEnumerable<DeliveryMethodDTO>> GetAllDeliveryMethod()
        {
           var DeliveryMethod = await unitOfWork.GenericRepository<DeliveryMethod,int>().GetAllAsync();
          var Result =  mapper.Map<IEnumerable<DeliveryMethodDTO>>(DeliveryMethod);
            return Result;
        }

        public async Task<OrderResultDto> GetOrderByIDAsnyc(Guid Id)
        {
            var Spc = new OrderSpecifications(Id);
           var order = await unitOfWork.GenericRepository<order, Guid>().GetAsync(Spc);
            if (order == null) throw new OrderNotFoundException(Id);
            //Map to DTO
            var Result = mapper.Map<OrderResultDto>(order);
            return Result;

        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail)
        {
            var Spc = new OrderSpecifications(userEmail);
            var orders = await unitOfWork.GenericRepository<order, Guid>().GetAllAsync(Spc);
            //Map to DTO
            var Result = mapper.Map<IEnumerable<OrderResultDto>>(orders);
            return Result;
        }
    }
}
