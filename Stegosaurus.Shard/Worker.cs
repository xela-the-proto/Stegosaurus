using System.Configuration;
using Docker.DotNet;
using RabbitMQ.Client;
using Stegosaurus.Shard.Helpers;
using Stegosaurus.Shard.Net;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public static ILogger<Worker> _logger;
    
    public static List<string> queues = new()
    {
        "creation",
        "deletion",
        "info"
    };

    private static string connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
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
        _logger.LogCritical(connectionString);
        GenerateID id = new GenerateID();
        var args = Environment.GetCommandLineArgs();
        var handler = new RabbitHandler();
        await ConfigsHelper.LocalFiles();
        var connection = RabbitMQHelpers.RabbitMQConnectionHelper().Result;
        var channel = await connection.CreateChannelAsync();
        var shard_id = id.GetID().Result;
        
        if (args.Contains("--discover"))
        {
            Broadcast br = new Broadcast(shard_id, channel);
            Thread th = new Thread(br.BroadcastID);
            th.Name ="Broadcast";
            th.Start();
        }
        
        while (!stoppingToken.IsCancellationRequested)
        { 
            await handler.Receive(channel, queues, shard_id);
            await Task.Delay(5000, stoppingToken);
        }
    }
}