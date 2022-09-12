using AutoMapper;
using SkiNet.Core.Entities;
using SkiNet.Core.Entities.OrderAggregate;
using SkiNet.Svc.DTOs;

namespace SkiNet.Svc.Helpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<SkiNet.Core.Entities.Identity.Address, AddressDto>().ReverseMap();

    CreateMap<AddressDto, SkiNet.Core.Entities.OrderAggregate.Address>();

    CreateMap<BasketItemDto, BasketItem>();

    CreateMap<CustomerBasketDto, CustomerBasket>();

    CreateMap<Order, OrderToReturnDto>()
      .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
      .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

    CreateMap<OrderItem, OrderItemDto>()
      .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
      .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
      .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ItemOrdered.ImageUrl))
      .ForMember(d => d.ImageUrl, o => o.MapFrom<OrderItemUrlResolver>());

    CreateMap<Product, ProductToReturnDto>()
      .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
      .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
      .ForMember(d => d.ImageUrl, o => o.MapFrom<ImageUrlResolver>());
  }
}