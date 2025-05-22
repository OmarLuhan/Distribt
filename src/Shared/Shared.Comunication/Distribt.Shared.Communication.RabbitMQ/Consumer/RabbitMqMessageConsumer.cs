using Distribt.Shared.Communication.Consumer;
using Distribt.Shared.Communication.Consumer.Handler;
using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Serialization;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Distribt.Shared.Communication.RabbitMQ.Consumer;

public class RabbitMqMessageConsumer<TMessage> : IMessageConsumer<TMessage>
{
    private readonly ISerializer _serializer;
    private readonly RabbitMqSettings _settings;
    private readonly ConnectionFactory _connectionFactory;
    private readonly IHandleMessage _handleMessage;


    public RabbitMqMessageConsumer(ISerializer serializer, IOptions<RabbitMqSettings> settings, IHandleMessage handleMessage)
    {
        _settings = settings.Value;
        _serializer = serializer;
        _handleMessage = handleMessage;
        _connectionFactory = new ConnectionFactory()
        {
            HostName = _settings.Hostname,
            Password = _settings.Password,
            UserName = _settings.Username
        };
    }

    public Task StartAsync(CancellationToken cancelToken = default)
    {
        return Task.Run(async () => await Consume(), cancelToken);
    }

    private Task Consume()
    {
        IConnection connection = _connectionFactory.CreateConnection(); 
        IModel channel = connection.CreateModel(); 
        RabbitMqMessageReceiver receiver = new (channel, _serializer, _handleMessage);
        string queue = GetCorrectQueue();
        channel.BasicConsume(queue, false, receiver);
        return Task.CompletedTask;
    }

    private string GetCorrectQueue()
    {
        return (typeof(TMessage) == typeof(IntegrationMessage)
                   ? _settings.Consumer?.IntegrationQueue
                   : _settings.Consumer?.DomainQueue)
               ?? throw new ArgumentException("please configure the queues on the app-settings");
    }
}