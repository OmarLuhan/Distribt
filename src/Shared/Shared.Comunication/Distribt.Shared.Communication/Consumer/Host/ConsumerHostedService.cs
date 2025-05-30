using Distribt.Shared.Communication.Consumer.Manager;
using Microsoft.Extensions.Hosting;

namespace Distribt.Shared.Communication.Consumer.Host;

public class ConsumerHostedService<TMessage>(
    IConsumerManager<TMessage> consumerManager,
    IMessageConsumer<TMessage> messageConsumer)
    : IHostedService
{
    private readonly CancellationTokenSource _stoppingCancellationTokenSource =
        new();
    private Task? _executingTask;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _executingTask = ConsumeMessages(_stoppingCancellationTokenSource.Token);

        return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;    
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _stoppingCancellationTokenSource.Cancel();
        consumerManager.StopExecution();
        return  Task.CompletedTask;
    }

    private async Task ConsumeMessages(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            CancellationToken ct = consumerManager.GetCancellationToken();
            if (ct.IsCancellationRequested) break;
            try
            {
                await messageConsumer.StartAsync(cancellationToken);
            }catch (OperationCanceledException)
            {
                // ignore, the operation is getting cancelled
            }
            //#3 investigate if an exception on the process breaks the consumer.
        }
    }
}