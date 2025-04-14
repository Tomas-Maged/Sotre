using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Models;
using Shared;

namespace Services.MappingProfiles
{
   public class ProudectProfile : Profile
    {
        public ProudectProfile()
        {
            CreateMap<Proudect, ProudectResultDto>()
                .ForMember(d=>d.BrandName,o=>o.MapFrom(s=>s.ProudectBrand.Name))
                .ForMember(d => d.TypeName, o => o.MapFrom(s => s.ProudectType.Name))
                ;
            CreateMap<ProudectBrand, BrandResultDto>();
            CreateMap<ProudectType, TypeResultDto>();
        }

    }
}
