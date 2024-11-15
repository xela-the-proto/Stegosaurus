using System.Net;
using System.Net.Sockets;
using Docker.DotNet;
using RabbitMQ.Client;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public static ILogger<Worker> _logger;
    public static DockerClient client = new DockerClientConfiguration().CreateClient();
    public delegate Task<byte[]> RunDispatchListener();
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Main entrypoint, define the handler for rabbitmq and initialize
    /// all the necessary files for configurations ecc.
    /// After that create the connection and start the queue
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        RabbitHandler handler = new RabbitHandler();
        Init init = new Init();
        
        await init.Local();
        var config = await init.Config();
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = "127.0.0.1",
        };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        while (!stoppingToken.IsCancellationRequested)
        {
            //TODO:MORE CHANNELS HANDLING AT ONCE?
            var message = await handler.Receive(channel, "Dispatcher");
            await Task.Delay(0, stoppingToken);
        }
    }
}