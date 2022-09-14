using System.ComponentModel.DataAnnotations;

namespace SkiNet.Svc.DTOs;

public class CustomerBasketDto
{
  [Required]
  public string Id { get; set; }

  public List<BasketItemDto> Items { get; set; }

  public int? DeliveryMethodId { get; set; }

  public string ClientSecret { get; set; }

  public string PaymentIntentId { get; set; }

  public decimal ShippingPrice { get; set; }
}