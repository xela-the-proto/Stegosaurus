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
        bool break_broadcast = false;
        Worker._logger.LogWarning("Broadcasting to queue to register id...");
        
        /*
        channel.BasicAcksAsync += (sender, @event) =>
        {
            Worker._logger.LogWarning("Received Ack stopping broadcast...");
            break_broadcast = true;
            return Task.CompletedTask;
        };
        */
        while (!break_broadcast)
        {
            channel.BasicPublishAsync(exchange:"id", routingKey:"Discover",body:Encoding.UTF8.GetBytes(id));
            Thread.Sleep(5000);
        }
    }
}