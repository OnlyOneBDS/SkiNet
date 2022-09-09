using AutoMapper;
using SkiNet.Core.Entities;
using SkiNet.Core.Entities.Identity;
using SkiNet.Svc.DTOs;

namespace SkiNet.Svc.Helpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Address, AddressDto>().ReverseMap();

    CreateMap<BasketItemDto, BasketItem>();

    CreateMap<CustomerBasketDto, CustomerBasket>();

    CreateMap<Product, ProductToReturnDto>()
      .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
      .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
      .ForMember(d => d.ImageUrl, o => o.MapFrom<ImageUrlResolver>());
  }
}