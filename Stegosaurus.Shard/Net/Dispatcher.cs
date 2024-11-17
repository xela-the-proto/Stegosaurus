using System.Net.Sockets;
using Docker.DotNet.Models;
using Newtonsoft.Json.Linq;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class Dispatcher
{
    /// <summary>
    /// Called when we receive a message from the queue, checks what type of request we got and precesses it accordingly
    /// </summary>
    /// <param name="msg"></param>
     public static async Task Dispatch(Packet packet)
    {
        //TODO:RUN MULTIPLE OPERATIONS AT THE SAME TIME?
        Deserializer deserializer = new Deserializer();
            try
            {
                switch (packet.message)
                {
                    case "Creation":
                        Creator creator = new Creator();
                        await creator.CreateContainer((CreateContainerParameters)deserializer.AssessType(packet).Result);
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