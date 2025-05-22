using Microsoft.AspNetCore.Mvc;

namespace Distribt.Services.Subscriptions.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController:ControllerBase
{
    [HttpPost(Name = "subscribe")]
    public ActionResult<bool> Subscribe(SubscriptionDto subscription)
    {
        //TODO: logic 
        return Ok(true);
    }

    [HttpDelete(Name = "unsubscribe")]
    public ActionResult<bool> Unsubscribe(SubscriptionDto subscription)
    {
        //TODO: logic 
        return Ok(true);
    }
}

public record SubscriptionDto(string Email);
