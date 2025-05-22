using Distribt.Shared.Communication.Messages;

namespace Distribt.Shared.Communication.Publisher.Domain;
public interface IDomainMessagePublisher
{
    Task Publish(object message, Metadata? metadata = null, string? routingKey = null, CancellationToken cancellationToken = default);
    Task PublishMany(IEnumerable<object> messages, Metadata? metadata = null, string? routingKey = null, CancellationToken cancellationToken = default);
}
public class DefaultDomainMessagePublisher(IExternalMessagePublisher<DomainMessage> externalPublisher)
    : IDomainMessagePublisher
{
    public Task Publish(object message, Metadata? metadata = null, string? routingKey = null,
        CancellationToken cancellationToken = default)
    {
        Metadata calculatedMetadata = CalculateMetadata(metadata);
        DomainMessage domainMessage = DomainMessageMapper.MapToMessage(message, calculatedMetadata);
        return externalPublisher.Publish(domainMessage, routingKey, cancellationToken);
    }

    public Task PublishMany(IEnumerable<object> messages, Metadata? metadata = null, string? routingKey = null,
        CancellationToken cancellationToken = default)
    {
        var domainMessages =
            messages.Select(a => DomainMessageMapper.MapToMessage(a, CalculateMetadata(metadata)));
        return externalPublisher.PublishMany(domainMessages, routingKey, cancellationToken);
    }
    private static Metadata CalculateMetadata(Metadata? metadata)
    {
        return metadata ?? new Metadata(Guid.NewGuid().ToString(), DateTime.UtcNow);
    }
}