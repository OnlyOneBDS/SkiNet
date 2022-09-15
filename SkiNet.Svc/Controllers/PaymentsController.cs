using Microsoft.AspNetCore.Authorization;
using SkiNet.Core.Entities;
using SkiNet.Core.Entities.OrderAggregate;
using SkiNet.Core.Interfaces;
using SkiNet.Svc.Errors;
using Stripe;

namespace SkiNet.Svc.Controllers;

public class PaymentsController : BaseApiController
{
  private readonly IPaymentService _paymentService;
  private readonly ILogger<PaymentsController> _logger;
  private readonly string _whSecret;

  public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger, IConfiguration config)
  {
    _paymentService = paymentService;
    _logger = logger;
    _whSecret = config.GetSection("StripSettings:WhSecret").Value;
  }

  [Authorize]
  [HttpPost("{basketId}")]
  public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
  {
    var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

    if (basket == null)
    {
      return BadRequest(new ApiResponse(400, "Problem with your basket"));
    }

    return basket;
  }

  [HttpPost("webhook")]
  public async Task<ActionResult> StripeWebhook()
  {
    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
    var stripEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);

    PaymentIntent intent;
    Order order;

    switch (stripEvent.Type)
    {
      case "payment_intent.succeeded":
        intent = (PaymentIntent)stripEvent.Data.Object;
        _logger.LogInformation("Payment Succeeded", intent.Id);

        order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
        _logger.LogInformation("Order updated to payment received: ", order.Id);
        break;
      case "payment_intent.payment_failed":
        intent = (PaymentIntent)stripEvent.Data.Object;
        _logger.LogInformation("Payment Failed", intent.Id);

        order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
        _logger.LogInformation("Payment Failed: ", order.Id);
        break;
    }

    return new EmptyResult();
  }
}