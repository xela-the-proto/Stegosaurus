using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class RabbitHandler
{
    public async Func<CancellationToken, ValueTask> Receive(IChannel channel, List<string> queues,string ID)
    {
        var queueDeclareResult = await channel.QueueDeclareAsync();
        await channel.ExchangeDeclareAsync("dispatch", ExchangeType.Topic);
        var queueName = queueDeclareResult.QueueName;
        await channel.QueueBindAsync(queueName, "dispatch", ID + ".*");
        Worker._logger.LogInformation("Waiting for message on exchange...");
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += Received;
        await channel.BasicConsumeAsync(queueName, true, consumer);
        return ValueTask.CompletedTask;
    }


    private Task Received(object sender, BasicDeliverEventArgs e)
    {
        var packet = new Packet();
        Worker._logger.LogInformation(e.RoutingKey.LastIndexOf(@".").ToString());
        packet.Message = e.RoutingKey.Substring(e.RoutingKey.LastIndexOf(@".") + 1);
        var body = e.Body.ToArray();
        packet.Data = Encoding.UTF8.GetString(body);
        Worker._logger.LogInformation(packet.Data);
        Worker._logger.LogInformation(packet.Message);
        //TODO: HANDLE CONCURRENT CONTAINER CREATION AND DELETION
        if (e.RoutingKey == "creation")
        {
            Dispatcher.Dispatch(packet).Wait();
        }
        return Task.CompletedTask;
    }
}