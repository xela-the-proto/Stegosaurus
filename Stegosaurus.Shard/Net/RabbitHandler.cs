using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Stegosaurus.Shard.Net;

public class RabbitHandler
{

    public async Task<byte[]> Receive(IChannel channel,string queue)
    {
        await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        
        Worker._logger.LogInformation("[" + DateTime.Now +  "] Waiting for messages on " + queue + " queue");
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += Received;
        channel.BasicAcksAsync += (sender, @event) =>
        {
            Worker._logger.LogInformation("ACK");
            return Task.CompletedTask;
        };
        await channel.BasicConsumeAsync("Dispatcher",autoAck: true, consumer: consumer);
        Thread.Sleep(5000);
        return null;
    }


    Task Received(object sender, BasicDeliverEventArgs e)
    {
        Dispatcher dispatcher = new Dispatcher();
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body); 
        //TODO: HANDLE CONCURRENT CONTAINER CREATION AND DELETION
        Task.WaitAll(Dispatcher.Dispatch(message));
        Worker._logger.LogInformation(message);
        
        return Task.CompletedTask;
    }
    
    
}