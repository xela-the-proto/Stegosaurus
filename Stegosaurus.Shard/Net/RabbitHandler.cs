using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class RabbitHandler
{

    public async Task<byte[]> Receive(IChannel channel,List<string> queues)
    {
        QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
        await channel.ExchangeDeclareAsync(exchange: "dispatch", type: ExchangeType.Topic);
        string queueName = queueDeclareResult.QueueName;

        /*
        await channel.ExchangeDeclareAsync(exchange: "Dispatch", type: ExchangeType.Direct);
        foreach (var queue in queues)
        {
            await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            await channel.QueueBindAsync(queue: queue, exchange: "Dispatch", routingKey: "localhost");
            Worker._logger.LogInformation("[" + DateTime.Now +  "] Waiting for messages on " + queue + " queue");
        }
        /*
        await channel.QueueDeclareAsync(queue: queues[0], durable: false, exclusive: false, autoDelete: false, arguments: null);
        Worker._logger.LogInformation("[" + DateTime.Now +  "] Waiting for messages on " + queues[0] + " queue");
        await channel.QueueDeclareAsync(queue: queues[1], durable: false, exclusive: false, autoDelete: false, arguments: null);
        Worker._logger.LogInformation("[" + DateTime.Now +  "] Waiting for messages on " + queues[1] + " queue");*/
        
        await channel.QueueBindAsync(queue: queueName, exchange: "dispatch", routingKey: "localhost.*");
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += Received;
        await channel.BasicConsumeAsync(queueName,autoAck: true, consumer: consumer);
        Thread.Sleep(5000);
        return null;
    }


    Task Received(object sender, BasicDeliverEventArgs e)
    {
        Dispatcher dispatcher = new Dispatcher();
        Packet packet = new Packet();
        packet.message = e.RoutingKey.Substring(e.RoutingKey.LastIndexOf("." + 1));
        var body = e.Body.ToArray();
        packet.data = Encoding.UTF8.GetString(body); 
        Worker._logger.LogInformation(packet.data);
        Worker._logger.LogInformation(packet.message);
        //TODO: HANDLE CONCURRENT CONTAINER CREATION AND DELETION
        if (e.RoutingKey == "Creation")
        {
            Task.WaitAll(Dispatcher.Dispatch(packet));
        }
        
        
        return Task.CompletedTask;
    }
    
    
}