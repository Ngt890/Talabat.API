using AutoMapper;
using Microsoft.Owin.BuilderProperties;
using Talabat.Core.Entities;
using Talabat.Core.Entities.identity;
using Talabt.API.DTOS;

using Talabat.Core.Entities.OrderAggregate;

namespace Talabt.API.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
             .ForMember(P => P.CategoryName, O => O.MapFrom(S => S.Category.Name))
              .ForMember(P => P.BrandName, O => O.MapFrom(S => S.Brand.Name))
               .ForMember(P=>P.PictureUrl,O=>O.MapFrom< ProductPictureUrlResolver>())
              ;
            CreateMap<AddAddress, AddressDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItems>();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap< AddressDto, Adress>();
            CreateMap<Order, OrderToReturnDto>()
               .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
               .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
               . ForMember(d => d.Items, o => o.MapFrom(s => s.Items));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
             
              
            
        


        }
           
        }

    }

