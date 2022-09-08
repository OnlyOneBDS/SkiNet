using Microsoft.AspNetCore.Mvc;
using SkiNet.Core.Entities;
using SkiNet.Core.Interfaces;

namespace SkiNet.Svc.Controllers;

public class BasketController : BaseApiController
{
  private readonly IBasketRepository _basketRepository;

  public BasketController(IBasketRepository basketRepository)
  {
    _basketRepository = basketRepository;
  }

  [HttpGet]
  public async Task<ActionResult<CustomerBasket>> GetBasketBy(string id)
  {
    var basket = await _basketRepository.GetBasketAsync(id);
    return Ok(basket ?? new CustomerBasket(id));
  }

  [HttpPost]
  public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
  {
    var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
    return Ok(updatedBasket);
  }

  [HttpDelete]
  public async Task DeleteBasketAsync(string id)
  {
    await _basketRepository.DeleteBasketAsync(id);
  }
}