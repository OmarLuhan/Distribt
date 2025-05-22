using Distribt.Shared.Communication.Consumer.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Distribt.Shared.Communication.Consumer.Host;

public class ConsumerController<TMessage>(IConsumerManager<TMessage> consumerManager) :ControllerBase
{
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("start")]
    public virtual IActionResult Start()
    {
        consumerManager.RestartExecution();
        return Ok();
    }
}