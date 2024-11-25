using RabbitMQ.Client;

namespace Stegosaurus.Shard.Net;

public class Broadcast
{
    public static async Task BroadcastID(string ID, IChannel channel)
    {
        channel.ExchangeDeclareAsync("broadcast-id", ExchangeType.Direct);
    }
}