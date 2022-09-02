using AutoMapper;
using SkiNet.Core.Entities;
using SkiNet.Svc.DTOs;

namespace SkiNet.Svc.Helpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Product, ProductToReturnDto>().ForMember(d => d.ImageUrl, o => o.MapFrom<ImageUrlResolver>());
  }
}