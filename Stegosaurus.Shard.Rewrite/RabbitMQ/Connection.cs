using RabbitMQ.Client;

namespace Stegosaurus.shard.RabbitMQ;

public class Connection
{
    public static async Task<IConnection> Open()
    {
        //TODO:Grab ip and all of the goodies from a config
        /*
         * Open a IConnection
         */
        var connectionFactory = new ConnectionFactory
        {
            HostName = "servers.xela.space"
        };
        var iconn = await connectionFactory.CreateConnectionAsync();
        return iconn;
    }

    public static async Task Close(IConnection connection,ushort ReasonCode,string ReasonTxt)
    {
        /*
         * close the connection provided with a reason and a code
         */
        connection.CloseAsync(ReasonCode, ReasonTxt);
    }
}