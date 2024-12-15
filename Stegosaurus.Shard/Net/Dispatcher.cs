using System.Net.Sockets;
using Docker.DotNet.Models;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.Docker;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Net;

public class DockerDispatcher
{
    /// <summary>
    ///     Called when we receive a message from the queue, checks what type of request we got and precesses it accordingly
    /// </summary>
    /// <param name="packet"></param>
    public static async Task DockerDispatch(Packet packet)
    {
        //TODO:RUN MULTIPLE OPERATIONS AT THE SAME TIME?
        var deserializer = new Deserializer();
        try
        {
            switch (packet.Message)
            {
                case "creation":
                    var creator = new Creator();
                    var id = await creator.CreateContainer((CreateContainerParameters)deserializer.AssessType(packet).Result);
                    return;
                case "deletion":
                    throw new NotImplementedException();
                case "status":
                    throw new NotImplementedException();
                case "start":
                    var starter = new Starter();
                    return;
                case "stop":
                    throw new NotImplementedException();
            }
        }
        catch (SocketException e)
        {
            Worker._logger.LogCritical(e.Message);
            throw;
        }

        Thread.Sleep(500);
    }
}
