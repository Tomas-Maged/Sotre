using Domian.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Proudect, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParamters SpecParam) : base
            (p =>
            (string.IsNullOrEmpty(SpecParam.Search) || p.Name.ToLower().Contains(SpecParam.Search.ToLower())) &&
            (!SpecParam.BrandId.HasValue || p.BrandId == SpecParam.BrandId) &&
            (!SpecParam.TypeId.HasValue || p.TypeId == SpecParam.TypeId))
        {
            ApplyIncludes();
            ApplySorting(SpecParam.Sort);
            ApplyPagination(SpecParam.PageIndex, SpecParam.PageSize);
        }

        private void ApplyIncludes()
        {
            AddInclude(p => p.ProudectBrand);
            AddInclude(p => p.ProudectType);

        }
        private void ApplySorting(string? Sort)
        {
            if (string.IsNullOrEmpty(Sort))
            {
                switch (Sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }
       

    }
}

