using System.Net;
using System.Net.Sockets;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class SocketHandler
{
    public async Task SocketOpener()
    {
        // Get Host IP Address that is used to establish a connection
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1
        // If a host has multiple addresses, you will get a list of addresses
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 53871);
        // Incoming data from the client.
        string data = null;
        byte[] bytes = new byte[1024];
        // Create a Socket that will use Tcp protocol
        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // A Socket must be associated with an endpoint using the Bind method
        listener.Bind(localEndPoint);
        // Specify how many requests a Socket can listen before it gives Server busy response.
        // We will listen 10 requests at a time
        listener.Listen(10);
        Console.WriteLine("Waiting for a connection...");
        Worker._logger.LogWarning("Waiting for socket connection...");
        Socket handler = listener.Accept();
        Deserializer deserializer = new Deserializer();
        while (true)
        {
            try
            {
                Worker._logger.LogInformation("Awaiting new socket message");
                handler.Receive(bytes);
                if (bytes.Any())
                {
                    Worker._logger.LogWarning("Got socket message creating container");
                    var c_class = deserializer.AssessType(bytes, false).Result;
                    string class_name = c_class.ToString().Substring(c_class.ToString().LastIndexOf(".", StringComparison.Ordinal)+ 1);
                
                    switch (class_name)
                    {
                        case "Container":
                            Creator creator = new Creator();
                            creator.CreateContainer((Container)c_class);
                            break;
                    }
                }
                else if(!bytes.Any())
                {
                    Worker._logger.LogInformation("[" + DateTime.Now + "]" + " Idling");
                }
            }
            catch (SocketException e)
            {
                Worker._logger.LogCritical("Socket closed!");
                throw;
            }
        }
        
    }
}