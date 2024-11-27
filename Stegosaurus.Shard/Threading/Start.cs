using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard.Threading;

public class Start
{
    public async void StartThread(Func<object> func,CancellationTokenSource Token)
    {
        
    }
}