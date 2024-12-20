using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Stegosaurus.Shard.Helpers;

public class RabbitMQHelpers
{
    public static async Task<IConnection> RabbitMQConnectionHelper(CancellationToken token = default(CancellationToken))
    {
        var ShardConfig = ConfigsHelper.Config().Result;
        var connection = new ConnectionFactory();
        if (!(ShardConfig.Username == string.Empty && ShardConfig.Password == string.Empty))
        {
            var conn = new ConnectionFactory
            {
                HostName = ShardConfig.Ip,
                UserName = ShardConfig.Username,
                Password = ShardConfig.ShardID
            };
            connection = conn;
        }
        else
        {
            var conn = new ConnectionFactory
            {
                HostName = ShardConfig.Ip
            };
            connection = conn;
        }
        if (token == default(CancellationToken))
        {
            return connection.CreateConnectionAsync().Result;
        }
        else
        {
            return connection.CreateConnectionAsync(token).Result;
        }
    }

    public static async Task RabbitMQSenderHelper(IChannel channel, string exchange = "", string routingKey = "" ,
        string message = null)
    {
        channel.BasicPublishAsync(exchange:exchange, routingKey:routingKey,body:Encoding.UTF8.GetBytes(message));
        return;
    }

    public static async Task<ReturnReceiverHelper> RabbitMQReceiveHelper(IChannel channel,
        string RoutingKey, string exchange = "dispatch")
    {
        var queueResult = await channel.QueueDeclareAsync();
        await channel.ExchangeDeclareAsync(exchange, ExchangeType.Topic);
        var queueName = queueResult.QueueName;
        await channel.QueueBindAsync(queueName, exchange, RoutingKey);
        var consumer = new AsyncEventingBasicConsumer(channel);

        var Receiver = new ReturnReceiverHelper
        {
            Consumer = consumer,
            QueueName = queueName,
        };
        
        return Receiver;
    }
}

public class ReturnReceiverHelper
{
    public AsyncEventingBasicConsumer Consumer {get; set; }
    public string QueueName { get; set; }
}