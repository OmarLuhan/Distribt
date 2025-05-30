namespace Distribt.Shared.Communication.Consumer.Manager;

public class ConsumerManager<TMessage> : IConsumerManager<TMessage>
{
    private CancellationTokenSource _cancellationTokenSource = new();

    public void RestartExecution()
    {
        CancellationTokenSource cancellationTokenSource = _cancellationTokenSource;
        _cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();
    }

    public void StopExecution()
    {
        _cancellationTokenSource.Cancel();
    }

    public CancellationToken GetCancellationToken()
    {
        return _cancellationTokenSource.Token;
    }
}