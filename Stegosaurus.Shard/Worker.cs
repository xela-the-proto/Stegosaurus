using System.Net;
using System.Net.Sockets;
using Docker.DotNet;
using Stegosaurus.Shard.Docker;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public static ILogger<Worker> _logger;
    
    
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Get Host IP Address that is used to establish a connection
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1
        // If a host has multiple addresses, you will get a list of addresses
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
        // Incoming data from the client.
        string data = null;
        byte[] bytes = new byte[256];
        Creator creator = new Creator();
        Init init = new Init();
        await init.Local();
        // Create a Socket that will use Tcp protocol
        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // A Socket must be associated with an endpoint using the Bind method
        listener.Bind(localEndPoint);
        // Specify how many requests a Socket can listen before it gives Server busy response.
        // We will listen 10 requests at a time
        listener.Listen(10);

        Console.WriteLine("Waiting for a connection...");
        _logger.LogWarning("Waiting for socket connection...");
        Socket handler = listener.Accept();
        //simualate if we get a message
        //eventually here we await a network call telling us what to do
        while (!stoppingToken.IsCancellationRequested)
        {
            handler.Receive(bytes);
            if (bytes.Any())
            {
                _logger.LogWarning("Got socket message creating container");
                await creator.CreateContainer(bytes);
            }
            else if(!bytes.Any())
            {
                _logger.LogInformation("[" + DateTime.Now + "]" + " Idling");
            }
            
            await Task.Delay(500, stoppingToken);
        }
    }
}