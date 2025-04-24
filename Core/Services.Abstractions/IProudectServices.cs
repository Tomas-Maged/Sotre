using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProudectServices
    {
        //GetAllProductsAsync
        //Task<IEnumerable<ProudectResultDto>> GetAllProductsAsync(int? BrandId, int? TypeId, string? Sort, int PageIndex = 1, int PageSize = 5)
        Task<PaginationResponse<ProudectResultDto>> GetAllProductsAsync(ProductSpecificationsParamters SpecParams)
            
            ;
        //GetProductByIdAsync
        Task<ProudectResultDto> GetProductByIdAsync(int id);
        //GetBrandByIdAsync
        Task<IEnumerable<BrandResultDto>> GetAllBrandessAsync();
        //GetTypeByIdAsync
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
