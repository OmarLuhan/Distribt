using System.Reflection;
using Distribt.Shared.Communication.Messages;

namespace Distribt.Shared.Communication.Consumer.Handler;
public interface IHandleMessage
{ 
    Task Handle(IMessage message, CancellationToken cancellationToken = default);
}
public class HandleMessage(IMessageHandlerRegistry messageHandlerRegistry) : IHandleMessage
{
    public Task Handle(IMessage message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        Type messageType = message.GetType();
        Type handlerType = typeof(IMessageHandler<>).MakeGenericType(messageType);
        var handlers = messageHandlerRegistry.GetMessageHandlerForType(handlerType, messageType).ToList();

        foreach (IMessageHandler handler in handlers)
        {
            Type messageHandlerType = handler.GetType();
            
            MethodInfo? handle = messageHandlerType.GetMethods()
                .Where(methodInfo => methodInfo.Name == nameof(IMessageHandler<object>.Handle))
                .FirstOrDefault(info => info.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .Contains(message.GetType()));
            
            if (handle != null) 
                return  (Task) handle.Invoke(handler, [message, cancellationToken])!;
        }
        return  Task.CompletedTask;
    }
}