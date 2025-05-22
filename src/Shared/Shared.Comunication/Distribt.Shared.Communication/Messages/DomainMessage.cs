namespace Distribt.Shared.Communication.Messages;

public record DomainMessage(string MessageIdentifier, string Name) : IMessage
{
    public string MessageIdentifier { get; } = MessageIdentifier;
    public string Name { get; } = Name;
}
public record DomainMessage<T>(string MessageIdentifier, string Name, T Content, Metadata Metadata)
    : DomainMessage(MessageIdentifier, Name);