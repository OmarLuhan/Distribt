using Microsoft.AspNetCore.Mvc;

namespace Distribt.Services.Products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController:ControllerBase
{
    [HttpGet("{productId:guid}")]
    public ActionResult<ProductDto> GetProduct(Guid productId)
    {
        return Ok(new ProductDto(productId, "TODO", "TODO"));
    }

    [HttpPost(Name = "add-product")]
    public ActionResult<bool> GetProduct(ProductDto product)
    {
        return Ok(true);
    }
}

public record ProductDto(Guid ProductId, string ProductName, string ProductDescription);

