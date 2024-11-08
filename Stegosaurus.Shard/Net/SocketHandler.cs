using System.Net;
using System.Net.Sockets;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class SocketHandler
{
    public async Task SocketOpener()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 53871);
        int i = 1;
        while (!Worker.SocketListener.IsBound)
        {
            try
            {
                Worker._logger.LogWarning("Trying for socket connection...");
                Worker.SocketListener.Bind(localEndPoint);
                Worker.SocketListener.Listen();
                await Worker.SocketListener.AcceptAsync();
            }
            catch (Exception e)
            {
                Worker._logger.LogCritical(e.Message);
                /*
                Worker._logger.LogError(@"failed to connect to {ip}:{port} {i} times",localEndPoint.Address,localEndPoint.Port, i);
                i++;
                */
            }
            
            
            Thread.Sleep(500);
        }
        Worker._logger.LogWarning("Socket connected!");
        
        Dispatcher().Wait(60000);
        if (!Dispatcher().IsCompleted)
        {
            throw new TimeoutException("Failed to crete container in less than 60 seconds!");
        }
    }

    async Task Dispatcher()
    { 
        byte[] bytes = new byte[1024];
        Deserializer deserializer = new Deserializer();
        while (true)
        {
            try
            {
                Worker._logger.LogInformation("Awaiting new socket message");
                Worker.SocketListener.Receive(bytes);
                //check if we got an empty payload
                bool hasAllZeroes = bytes.All(singleByte => singleByte == 0);
                if (!hasAllZeroes)
                {
                    Worker._logger.LogWarning("Got socket message creating container");
                    var c_class = deserializer.AssessType(bytes, false).Result;
                    string class_name = c_class.ToString().Substring(c_class.ToString().LastIndexOf(".", StringComparison.Ordinal)+ 1);
                
                    switch (class_name)
                    {
                        case "Container":
                            Creator creator = new Creator();
                            await creator.CreateContainer((Container)c_class);
                            return;
                    }
                }
                else if(hasAllZeroes)
                {
                    Worker._logger.LogInformation("[" + DateTime.Now + "]" + " Idling");
                }
            }
            catch (SocketException e)
            {
                Worker._logger.LogCritical("Socket closed!");
                Worker.SocketListener.Shutdown(SocketShutdown.Both);
                throw;
            }
            Thread.Sleep(500);
        }
    }
}