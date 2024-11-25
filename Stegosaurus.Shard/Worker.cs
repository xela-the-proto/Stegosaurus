using Docker.DotNet;
using RabbitMQ.Client;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public delegate Task<byte[]> RunDispatchListener();

    public static ILogger<Worker> _logger;
    public static DockerClient Client = new DockerClientConfiguration().CreateClient();

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Main entrypoint, define the handler for rabbitmq and initialize
    ///     all the necessary files for configurations ecc.
    ///     After that create the connection and start the queue
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var handler = new RabbitHandler();
        var init = new Init();
        List<string> queues = new()
        {
            "Creation",
            "Deletion",
            "Info"
        };
        await init.LocalFiles();
        var config = await init.Config();
        var factory = new ConnectionFactory
        {   
            HostName = config.Ip
        };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        var id = GenerateID.Generate().Result;
        Broadcast br = new Broadcast(id, channel);
        Thread t_BroadcastID = new Thread(new ThreadStart(br.BroadcastID));
        t_BroadcastID.Name = "Broadcast_Thread";
        t_BroadcastID.Start();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            //TODO:MORE CHANNELS HANDLING AT ONCE?
            await handler.Receive(channel, queues,id);
            await Task.Delay(5000, stoppingToken);
        }
    }
}