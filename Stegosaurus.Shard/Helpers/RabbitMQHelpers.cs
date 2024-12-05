using System.Text;
using RabbitMQ.Client;

namespace Stegosaurus.Shard.Helpers;

public class RabbitMQHelpers
{
    public static async Task<IConnection> RabbitMQConnectionHelper(CancellationToken token = default(CancellationToken))
    {
        var ShardConfig = ConfigsHelper.Config();
        var conn = new ConnectionFactory
        {
            HostName = ShardConfig.Result.Ip,
            UserName = ShardConfig.Result.Username,
            Password = ShardConfig.Result.ShardID
        };
        if (token == default(CancellationToken))
        {
            var channel = conn.CreateConnectionAsync();
            return channel.Result;
        }
        else
        {
            var channel = conn.CreateConnectionAsync(token);
            return channel.Result;
        }
    }

    public static async Task<IConnection> RabbitMQSenderHelper(IChannel channel, string exchange = "", string routingKey = "" , string message = null)
    {
        channel.BasicPublishAsync(exchange:exchange, routingKey:routingKey,body:Encoding.UTF8.GetBytes(message));
    }
}