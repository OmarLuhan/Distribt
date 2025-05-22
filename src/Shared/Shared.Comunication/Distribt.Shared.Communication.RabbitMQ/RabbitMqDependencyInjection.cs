using Distribt.Shared.Communication.Consumer;
using Distribt.Shared.Communication.Consumer.Handler;
using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Communication.Publisher;
using Distribt.Shared.Communication.RabbitMQ.Consumer;
using Distribt.Shared.Communication.RabbitMQ.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Distribt.Shared.Communication.RabbitMQ;

public static class RabbitMqDependencyInjection
{
    public static void AddRabbitMq(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<RabbitMqSettings>(configuration.GetSection("Bus:RabbitMQ"));
    }
    
    public static void AddConsumerHandlers(this IServiceCollection serviceCollection, IEnumerable<IMessageHandler> handlers)
    {
        
        serviceCollection.AddSingleton<IMessageHandlerRegistry>(new MessageHandlerRegistry(handlers));
        serviceCollection.AddSingleton<IHandleMessage, HandleMessage>();
    }

    public static void AddRabbitMqConsumer<TMessage>(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddConsumer<TMessage>();
        serviceCollection.AddSingleton<IMessageConsumer<TMessage>, RabbitMqMessageConsumer<TMessage>>();
    }
    
    public static void AddRabbitMqPublisher<TMessage>(this IServiceCollection serviceCollection)
        where TMessage : IMessage
    {
        serviceCollection.AddPublisher<TMessage>();
        serviceCollection.AddSingleton<IExternalMessagePublisher<TMessage>, RabbitMqMessagePublisher<TMessage>>();
    }
    
    
}