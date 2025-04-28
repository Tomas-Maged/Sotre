using AutoMapper;
using Domian.Contercts;
using Domian.Exceptions;
using Domian.Models;
using Services.Abstractions;
using Services.Specifications;
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

        public async Task<PaginationResponse<ProudectResultDto>> GetAllProductsAsync(ProductSpecificationsParamters SpecParams)
        {
            var Spec =new ProductWithBrandsAndTypesSpecifications(SpecParams);


            var specCount = new ProductWithCountSpecifications(SpecParams);
            var countResult = await unitOfWork.GenericRepository<Proudect, int>().CountAsync(specCount);

            var Proudect = await unitOfWork.GenericRepository<Proudect,int>().GetAllAsync(Spec);
            var Result = mapper.Map<IEnumerable<ProudectResultDto>>(Proudect);
            return new PaginationResponse<ProudectResultDto>(SpecParams.PageIndex,SpecParams.PageSize,0,Result);
        }
        public async Task<ProudectResultDto> GetProductByIdAsync(int id)
        {
            var Spec = new ProductWithBrandsAndTypesSpecifications(id);
            var Proudect = await unitOfWork.GenericRepository<Proudect,int>().GetAsync(Spec);
            if (Proudect is null) throw new ProductNotFoundExceptions(id);
            
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
