using AutoMapper;
using SkiNet.Core.Entities;
using SkiNet.Svc.DTOs;

namespace SkiNet.Svc.Helpers;

public class ImageUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
  private readonly IConfiguration _config;

  public ImageUrlResolver(IConfiguration config)
  {
    _config = config;
  }

  public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
  {
    if (!string.IsNullOrEmpty(source.ImageUrl))
    {
      return _config["ApiUrl"] + source.ImageUrl;
    }

    return null;
  }
}