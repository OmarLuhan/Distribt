using System.Reflection;
using Distribt.Shared.Communication.Messages;

namespace Distribt.Shared.Communication.Publisher.Integration;

public class IntegrationMessageMapper
{
    public static IntegrationMessage MapToMessage(object message, Metadata metadata)
    {
        if (message is IntegrationMessage)
            throw new ArgumentException("Message should not be of type IntegrationMessage, it should be a plain type");

        MethodInfo? buildWrapperMethodInfo = typeof(IntegrationMessageMapper).GetMethod(
            nameof(ToTypedIntegrationEvent),
            BindingFlags.Static | BindingFlags.NonPublic
        );
        
        MethodInfo? buildWrapperGenericMethodInfo = buildWrapperMethodInfo?.MakeGenericMethod(new[] {message.GetType()});
        object? wrapper = buildWrapperGenericMethodInfo?.Invoke(
            null,
            [
                message,
                metadata
            ]
        );
        return (wrapper as IntegrationMessage)!;
    }
    private static IntegrationMessage<T> ToTypedIntegrationEvent<T>(T message, Metadata metadata)
    {
        return new IntegrationMessage<T>(Guid.NewGuid().ToString(), typeof(T).Name, message, metadata);
    }
}