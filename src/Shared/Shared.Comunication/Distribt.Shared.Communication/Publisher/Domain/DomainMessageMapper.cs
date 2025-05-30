using System.Reflection;
using Distribt.Shared.Communication.Messages;

namespace Distribt.Shared.Communication.Publisher.Domain;

public class DomainMessageMapper
{
    public static DomainMessage MapToMessage(object message, Metadata metadata)
    {
        if (message is IntegrationMessage)
            throw new ArgumentException("Message should not be of type DomainMessage, it should be a plain type");

        MethodInfo? buildWrapperMethodInfo = typeof(DomainMessageMapper).GetMethod(
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
        return (wrapper as DomainMessage)!;
    }
    private static DomainMessage<T> ToTypedIntegrationEvent<T>(T message, Metadata metadata)
    {
        return new DomainMessage<T>(Guid.NewGuid().ToString(), typeof(T).Name, message, metadata);
    }
}