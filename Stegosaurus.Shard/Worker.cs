using System.Net;
using System.Net.Sockets;
using Docker.DotNet;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public static ILogger<Worker> _logger;
    
    public static IPHostEntry host = Dns.GetHostEntry("localhost");
    public static IPAddress ipAddress = host.AddressList[0];
    public static Socket SocketListener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SocketHandler handler = new SocketHandler();
        Init init = new Init();
        await init.Local();
        while (!stoppingToken.IsCancellationRequested)
        {
            //Main entrypoint to code
            await handler.SocketOpener();
            await Task.Delay(500, stoppingToken);
        }
    }
}