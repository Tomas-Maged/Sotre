using AutoMapper;
using Domian.Models.OrderModels;
using Services.Abstractions;
using Shared.ORderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<order, OrderResultDto>()
                .ForMember(d => d.PaymentStutas, o => o.MapFrom(s => s.PaymentStutas.ToString()))
                .ForMember(d => d.deliveryMethod, o => o.MapFrom(s => s.deliveryMethod.ShortName)) 
                .ForMember(d => d.Total, o => o.MapFrom(s => s.SubTotal + s.deliveryMethod.cost))
                ;
            CreateMap<DeliveryMethod, DeliveryMethodDTO>();

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId,o=>o.MapFrom(s => s.Proudect.ProductId))
                .ForMember(d => d.ProductName,o=>o.MapFrom(s => s.Proudect.ProductName))
                .ForMember(d => d.PictureUrl,o=>o.MapFrom(s => s.Proudect.PictureUrl))
                ;
            CreateMap<Address,AddressDTO>().ReverseMap();
        }
    }
}
