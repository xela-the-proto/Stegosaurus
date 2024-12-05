using Docker.DotNet;
using RabbitMQ.Client;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public delegate Task<byte[]> RunDispatchListener();

    public static ILogger<Worker> _logger;
    public static List<string> queues = new()
    {
        "Creation",
        "Deletion",
        "Info"
    };
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
        var args = Environment.GetCommandLineArgs();
        var handler = new RabbitHandler();
        await ConfigsHelper.LocalFiles();
        var config = await ConfigsHelper.Config();
        
        var factory = new ConnectionFactory
        {   
            HostName = config.Ip,
            UserName = "admin",
            Password = "admin"
            
        };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        var id = GenerateID.GetID().Result;
        if (args.Contains("--discover"))
        {
            Broadcast br = new Broadcast(id, channel);
            Thread th = new Thread(br.BroadcastID);
            th.Name ="Broadcast";
            th.Start();
        }
        while (!stoppingToken.IsCancellationRequested)
        { 
            await handler.Receive(channel, queues, id);
            await Task.Delay(5000, stoppingToken);
        }
    }
}