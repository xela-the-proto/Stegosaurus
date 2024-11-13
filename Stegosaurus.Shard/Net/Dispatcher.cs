using System.Net.Sockets;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class Dispatcher
{
    /// <summary>
    /// Called when we receive a message from the queue, checks what type of request we got and precesses it accordingly
    /// </summary>
    /// <param name="msg"></param>
     public static async Task Dispatch(string msg)
    {
        //TODO:RUN MULTIPLE OPERATIONS AT THE SAME TIME?
        Deserializer deserializer = new Deserializer();
            try
            {
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