using Docker.DotNet;
using Docker.DotNet.Models;
using Newtonsoft.Json;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard;
using Docker;
public class Init
{
    //public readonly ILogger<Worker> logger;

    public static DockerClient client = new DockerClientConfiguration().CreateClient();
    public Task Local()
    {
        string path = "";
        if (!new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                               + @"\StegoShard").Exists)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                      + @"\StegoShard\");
        }
        return Task.CompletedTask;
    }
}