using Microsoft.AspNetCore.Mvc;

namespace Distribt.Services.Orders.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController: ControllerBase
{
    [HttpGet("{orderId:guid}")]
    public ActionResult<OrderDto> GetOrder(Guid orderId)
    {
        //TODO: logic
        return Ok(new OrderDto(orderId));
    }

    [HttpPost(Name = "add-order")]
    public ActionResult<Guid> AddOrder(OrderDto order)
    {
        //TODO: logic
        return Ok(Guid.NewGuid());
    }


    //TODO: finish the dto
    //TODO: move
    public record OrderDto(Guid OrderId);
}

