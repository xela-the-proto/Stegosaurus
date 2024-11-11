using System.Net;
using System.Net.Sockets;
using Docker.DotNet;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public static ILogger<Worker> _logger;
    public static DockerClient client = new DockerClientConfiguration().CreateClient();
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        RabbitHandler handler2 = new RabbitHandler();
        Init init = new Init();
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
            var message = await handler2.Receive();
            //Main entrypoint to code
            //await handler.AwaitMessage(socket);
            await Task.Delay(500, stoppingToken);
        }
    }
}