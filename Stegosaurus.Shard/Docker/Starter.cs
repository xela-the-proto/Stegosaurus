using Docker.DotNet.Models;

namespace Stegosaurus.Shard.Docker;

public class Starter
{
    public async Task Start(string id)
    {
        await Worker.Client.Containers.StartContainerAsync(id,new ContainerStartParameters());
    }
}