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
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        RabbitHandler handler = new RabbitHandler();
        Init init = new Init();
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        //RunDispatchListener dispatchListener = handler.Receive;
        await init.Local();
        /*
        var shardConfig = init.Config().Result;
        IPHostEntry host = Dns.GetHostEntry(shardConfig.ip);
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddress, shardConfig.port);
        var socket = await handler.Open(ipAddress,endPoint);
        if (socket == null)
        {
            throw new InvalidDataException("Socket returned null!");
        }
        */
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await handler.Receive(channel);
            //Main entrypoint to code
            //await handler.AwaitMessage(socket);
            await Task.Delay(100, stoppingToken);
        }
    }
}