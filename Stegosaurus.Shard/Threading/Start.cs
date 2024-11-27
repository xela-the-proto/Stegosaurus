using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard.Threading;

public class Start
{
    public async void StartThread(Func<CancellationToken, ValueTask> func,CancellationTokenSource Token)
    {
        CancellationToken cancellationToken = Token.Token;
        ResiliencePipeline pipeline = new ResiliencePipelineBuilder().AddRetry(new RetryStrategyOptions())
            .AddTimeout(TimeSpan.FromSeconds(10)).Build();
        await pipeline.ExecuteAsync(func,cancellationToken);
    }
}