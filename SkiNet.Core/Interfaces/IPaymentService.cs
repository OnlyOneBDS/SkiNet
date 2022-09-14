using SkiNet.Core.Entities;
using SkiNet.Core.Entities.OrderAggregate;

namespace SkiNet.Core.Interfaces;

public interface IPaymentService
{
  Task<CustomerBasket> CreateOrUpdatePaymentIntent(string baskedId);
  Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
  Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
}