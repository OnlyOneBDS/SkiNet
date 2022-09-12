using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SkiNet.Core.Entities.OrderAggregate;
using SkiNet.Core.Interfaces;
using SkiNet.Svc.DTOs;
using SkiNet.Svc.Errors;

namespace SkiNet.Svc.Controllers;

[Authorize]
public class OrdersController : BaseApiController
{
  private readonly IOrderService _orderService;
  private readonly IMapper _mapper;

  public OrdersController(IOrderService orderService, IMapper mapper)
  {
    _mapper = mapper;
    _orderService = orderService;
  }

  [HttpPost]
  public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
  {
    var email = User.RetrieveEmailFromPrincipal();
    var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
    var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

    if (order == null)
    {
      return BadRequest(new ApiResponse(400, "Problem creating order"));
    }

    return Ok(order);
  }

  [HttpGet]
  public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
  {
    var email = User.RetrieveEmailFromPrincipal();
    var orders = await _orderService.GetOrdersForUserAsync(email);

    return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
  {
    var email = User.RetrieveEmailFromPrincipal();
    var order = await _orderService.GetOrderByIdAsync(id, email);

    if (order == null)
    {
      return NotFound(new ApiResponse(404));
    }

    return _mapper.Map<Order, OrderToReturnDto>(order);
  }

  [HttpGet("deliverymethods")]
  public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
  {
    return Ok(await _orderService.GetDeliveryMethodsAsync());
  }
}