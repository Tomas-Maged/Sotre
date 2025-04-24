using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Contercts;
using Domian.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCountSpecifications : BaseSpecifications<Proudect,int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParamters SpecParams) :
            base(
                p=>
                (string.IsNullOrEmpty(SpecParams.Search) || p.Name.ToLower().Contains(SpecParams.Search.ToLower())) &&
                (!SpecParams.BrandId.HasValue || p.BrandId == SpecParams.BrandId) &&
                (!SpecParams.TypeId.HasValue || p.TypeId == SpecParams.TypeId)
            )
        {
        }
        
                
        }
    }
