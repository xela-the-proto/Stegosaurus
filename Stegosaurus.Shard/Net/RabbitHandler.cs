using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Stegosaurus.Shard.Net;

public class RabbitHandler
{
    public async Task Receive()
    {
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = "localhost"
            
        };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: "Docker-container", durable: false, exclusive: false, autoDelete: false, arguments: null);
        Worker._logger.LogInformation("Waiting for messages...");
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Worker._logger.LogInformation(message);
            return Task.CompletedTask;
        };
        await channel.BasicConsumeAsync("Docker-container",autoAck: true, consumer: consumer);
    }
}