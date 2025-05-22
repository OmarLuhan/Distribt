using Distribt.Shared.Communication.Consumer.Host;
using Distribt.Shared.Communication.Consumer.Manager;
using Distribt.Shared.Communication.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Distribt.Services.Subscriptions.Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class IntegrationConsumerController(IConsumerManager<IntegrationMessage> consumerManager)
    : ConsumerController<IntegrationMessage>(consumerManager);