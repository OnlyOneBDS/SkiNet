using SkiNet.Core.Entities;
using SkiNet.Core.Entities.OrderAggregate;
using SkiNet.Core.Interfaces;
using SkiNet.Core.Specifications;

namespace SkiNet.Infrastructure.Services;

public class OrderService : IOrderService
{
  private readonly IBasketRepository _basketRepo;
  private readonly IUnitOfWork _unitOfWork;

  public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
    _basketRepo = basketRepo;
  }

  public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
  {
    // Get basket from repo
    var basket = await _basketRepo.GetBasketAsync(basketId);

    // Get items from the product repo
    var items = new List<OrderItem>();

    foreach (var item in basket.Items)
    {
      var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
      var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.ImageUrl);
      var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);

      items.Add(orderItem);
    }

    // Get delivery method from repo
    var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

    // Calculate subtotal
    var subtotal = items.Sum(item => item.Price * item.Quantity);

    // Create order
    var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

    _unitOfWork.Repository<Order>().Add(order);

    // Save to db
    var result = await _unitOfWork.Complete();

    if (result <= 0)
    {
      return null;
    }

    // Delete basket
    await _basketRepo.DeleteBasketAsync(basketId);

    // Return order
    return order;
  }

  public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
  {
    return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
  }

  public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
  {
    var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

    return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
  }

  public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
  {
    var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

    return await _unitOfWork.Repository<Order>().ListAsync(spec);
  }
}