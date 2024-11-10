using System.Net;
using System.Net.Sockets;
using System.Text;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class SocketHandlerRewrite
{
    public async Task<TcpClient> Open(IPAddress address,IPEndPoint local)
    {
        var buffer = new byte[1_024];
        TcpListener socket = new TcpListener(local);
        
        while (!socket.Server.IsBound)
        {
            try
            {
                socket.Start();

                var handler = await socket.AcceptTcpClientAsync();
                await using NetworkStream stream = handler.GetStream();
                int received = await stream.ReadAsync(buffer);
                var message = Encoding.UTF8.GetString(buffer, 0, received);
                Worker._logger.LogInformation(message);
                return handler;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;  
            }
        }

        return null;
    }

    public async Task AwaitMessage(TcpClient socket)
    {
        byte[] bytes = new byte[1_024];
        Deserializer deserializer = new Deserializer();
        NetworkStream stream = socket.GetStream();
        while (socket.Client.IsBound)
        {
            try
            {
                Worker._logger.LogInformation("Awaiting new socket message");
                await stream.ReadAsync(bytes);
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
                socket.Client.Shutdown(SocketShutdown.Both);
                throw;
            }
            Thread.Sleep(500);
        }
    }
}