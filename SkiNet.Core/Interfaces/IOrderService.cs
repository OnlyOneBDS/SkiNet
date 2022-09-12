using SkiNet.Core.Entities.OrderAggregate;

namespace SkiNet.Core.Interfaces;

public interface IOrderService
{
  Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
  Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
  Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
  Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}