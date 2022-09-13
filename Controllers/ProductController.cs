using ApiCursoDotnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiCursoDotnet.Controllers;

public class ProductController : ControllerBase
{
  private readonly IProductRepository productRepository;

  public ProductController(IProductRepository productRepository)
  {
    this.productRepository = productRepository;
  }

  [HttpGet("/products/{id}")]
  public async Task<IActionResult> GetById([FromRoute] int id) {
    if(!this.ModelState.IsValid) {
      return BadRequest(this.ModelState);
    }

    var product = await this.productRepository.getByIdAsync(id);

    return product is not null ? Ok(product) : NotFound("Product not found!");
  }

  [HttpPost("/products")]
  public async Task<IActionResult> Create([FromBody] ProductRequest productRequest) {
    if(!this.ModelState.IsValid) {
      return BadRequest(this.ModelState);
    }

    var product = await this.productRepository.addAsync(productRequest);

    return Created($"/products/{product.Id}", product.Id);
  }

  [HttpPut("/products/{id}")]
  public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductRequest productRequest) {
    if(!this.ModelState.IsValid) {
      return BadRequest(this.ModelState);
    }
    await this.productRepository.updateAsync(id, productRequest);
    return Ok();
  }

  [HttpDelete("/products/{id}")]
  public async Task<IActionResult> Delete([FromRoute] int id) {
    if(!this.ModelState.IsValid) {
      return BadRequest(this.ModelState);
    }

    await this.productRepository.deleteAsync(id);

    return NoContent();
  }
}