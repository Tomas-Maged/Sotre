using AutoMapper;
using Domian.Contercts;
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
    public class ProudectServices(IUnitOfWork unitOfWork, IMapper mapper) : IProudectServices
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<IEnumerable<ProudectResultDto>> GetAllProductsAsync()
        {
          var Proudect = await unitOfWork.GenericRepository<Proudect,int>().GetAllAsync();
            var Result = mapper.Map<IEnumerable<ProudectResultDto>>(Proudect);
            return Result;
        }
        public async Task<ProudectResultDto> GetProductByIdAsync(int id)
        {
          var Proudect = await unitOfWork.GenericRepository<Proudect,int>().GetAsync(id);
            if (Proudect is null) return null;
            
            var Result = mapper.Map<ProudectResultDto>(Proudect);
            return Result;

        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandessAsync()
        {
            var Brands = await unitOfWork.GenericRepository<ProudectBrand, int>().GetAllAsync();
            var Result =  mapper.Map<IEnumerable<BrandResultDto>>(Brands);
            return Result;
        }


        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GenericRepository<ProudectType, int>().GetAllAsync();
            var Result = mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return Result;
        }

    }
}
