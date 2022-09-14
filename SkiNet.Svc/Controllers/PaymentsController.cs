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
  private const string WhSecret = "whsec_9e9b50ec2a200ebce1b585d6e90434b008222830367e93e46582252f29c880ad";

  public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
  {
    _paymentService = paymentService;
    _logger = logger;
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
    var stripEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

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