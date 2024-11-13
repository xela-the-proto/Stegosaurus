using System.Net.Sockets;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class Dispatcher
{
     public static async Task Dispatch(string msg)
    {
        Deserializer deserializer = new Deserializer();
            try
            {
                Worker._logger.LogInformation("Awaiting new socket message");
                Worker._logger.LogWarning("Got socket message creating container");
                var c_class = deserializer.AssessType(msg, false).Result;
                string class_name = c_class.ToString()
                    .Substring(c_class.ToString().LastIndexOf(".", StringComparison.Ordinal) + 1);
                
                switch (class_name)
                {
                    case "Container":
                        Creator creator = new Creator();
                        await creator.CreateContainer((Container)c_class);
                        return;
                }
                
            }
            catch (SocketException e)
            {
                Worker._logger.LogCritical("Socket closed!");
                throw;
            }

            Thread.Sleep(500);
        
    }
}