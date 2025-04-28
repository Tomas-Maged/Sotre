using AutoMapper;
using Domian.Contercts;
using Domian.Exceptions;
using Domian.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Basketservice(IBasketRepository basketRepository , IMapper mapper) : IBasketservice
    {
        public async Task<BasketDto> GetbasketAsync(string id)
        {
           var basket =  await basketRepository.GetBaskedAsync(id);
            if (basket is null ) throw new BasketNotFoundException(id);
            var Result = mapper.Map<BasketDto>(basket);
            return Result;
        }

        public async Task<BasketDto> UpdatebasketAsync(BasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);
         basket = await basketRepository.UpdateBaskedAsync(basket);
            if (basket is null) throw new basketcreateOrUpdateBadRequestException();
           var result =  mapper.Map<BasketDto>(basket);
            return result;
        }
        public async Task<bool> DeletbasketAsync(string id)
        {
            var flag = await basketRepository.DeleteBasketAsync(id);
            if (flag ==  false) throw new BasketDeleteBadRequestException();
            return flag;
        }

    }
}
