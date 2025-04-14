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
        Task<IEnumerable<ProudectResultDto>> GetAllProductsAsync();
        //GetProductByIdAsync
        Task<ProudectResultDto> GetProductByIdAsync(int id);
        //GetBrandByIdAsync
        Task<IEnumerable<BrandResultDto>> GetAllBrandessAsync();
        //GetTypeByIdAsync
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
