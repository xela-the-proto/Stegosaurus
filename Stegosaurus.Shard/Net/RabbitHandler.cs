using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Stegosaurus.Shard.Helpers;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class RabbitHandler
{
    public async Task<byte[]> Receive(IChannel channel, List<string> queues,string ID)
    {
        Worker._logger.LogInformation("Waiting for message on exchange...");
        var consumer = RabbitMQHelpers.RabbitMQReceiveHelper(channel, ID + ".*");
        consumer.Result.Consumer.ReceivedAsync += Received;
        await channel.BasicConsumeAsync(consumer.Result.QueueName, true, consumer.Result.Consumer);
        return null;
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
        if (Worker.queues.Contains(packet.Message))
        {
            DockerDispatcher.DockerDispatch(packet);
        }
        return Task.CompletedTask;
    }
}