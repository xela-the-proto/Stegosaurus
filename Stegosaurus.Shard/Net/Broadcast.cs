using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Stegosaurus.Shard.Net;

public class Broadcast
{
    private string id;
    private IChannel channel;

    public Broadcast(string ID, IChannel channel)
    {
        this.id = ID;
        this.channel = channel;
    }
    public void BroadcastID()
    { 
        channel.BasicPublishAsync(exchange:string.Empty, routingKey:string.Empty,body:Encoding.UTF8.GetBytes(id));
    }
}