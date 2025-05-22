using Microsoft.AspNetCore.Mvc;

namespace Distribt.Services.Emails.Controllers;
[ApiController]
[Route("[controller]")]
public class EmailController: ControllerBase
{
    [HttpPost(Name = "send")]
    public ActionResult<bool> Send(EmailDto emailDto)
    {
        //TODO: logic to send the email.
        return Ok(true);
    }
}
public record EmailDto(string From, string To, string Subject, string Body);

