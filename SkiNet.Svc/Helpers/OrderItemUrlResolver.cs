using AutoMapper;
using SkiNet.Core.Entities.OrderAggregate;
using SkiNet.Svc.DTOs;

namespace SkiNet.Svc.Helpers;

public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
{
  private readonly IConfiguration _config;

  public OrderItemUrlResolver(IConfiguration config)
  {
    _config = config;
  }

  public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
  {
    if (!string.IsNullOrEmpty(source.ItemOrdered.ImageUrl))
    {
      return _config["ApiUrl"] + source.ItemOrdered.ImageUrl;
    }

    return null;
  }
}