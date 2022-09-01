using Microsoft.AspNetCore.Mvc;
using SkiNet.Core.Entities;
using SkiNet.Core.Interfaces;

namespace SkiNet.Svc.Controllers;

public class ProductsController : BaseApiController
{
  private readonly IProductRepository _respository;

  public ProductsController(IProductRepository respository)
  {
    _respository = respository;
  }

  [HttpGet]
  public async Task<ActionResult<List<Product>>> GetProducts()
  {
    var products = await _respository.GetProductsAsync();

    return Ok(products);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    return await _respository.GetProductByIdAsync(id);
  }

  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
  {
    return Ok(await _respository.GetProductBrandsAsync());
  }

  [HttpGet("types")]
  public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
  {
    return Ok(await _respository.GetProductTypesAsync());
  }
}