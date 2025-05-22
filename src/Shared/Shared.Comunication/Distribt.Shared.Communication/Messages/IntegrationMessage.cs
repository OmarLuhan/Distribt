namespace Distribt.Shared.Communication.Messages;

public record IntegrationMessage(string MessageIdentifier, string Name) : IMessage;

public record IntegrationMessage<T>(string MessageIdentifier, string Name, T Content, Metadata Metadata)
    : IntegrationMessage(MessageIdentifier, Name);