using Docker.DotNet.Models;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.db;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Docker;

public class Creator
{
    /// <summary>
    ///     Downloads the image of the container and then creates it, also checks if it exists.
    /// </summary>
    /// <param name="creator"></param>
    /// <exception cref="TimeoutException"></exception>
    public async Task<string> CreateContainer(CreateContainerParameters creator)
    {
        using var db_shard = new EFManagerGameServers();
        var container = new Container();
        var jsonManager = new JsonManager();
        var finder = new Finder();
        var client = Worker.Client;
        var logger = Worker._logger;
        lock (client)
        {
            //pull image from the docker repo
            client.Images.CreateImageAsync(
                new ImagesCreateParameters
                {
                    FromImage = creator.Image
                },
                new AuthConfig
                {
                    Email = null,
                    Username = null,
                    Password = null
                },
                new Progress<JSONMessage>(m => logger.LogInformation(m.ProgressMessage)));

            //gets the containers that match the name for checks
            var currentContainers = finder.Find("name", creator.Name).Result;
            //if we don't have any containers then we create
            if (!currentContainers.Any())
            {
                logger.LogInformation("creating container with name " + creator.Name + " and image " + creator.Image);
                var reply = client.Containers.CreateContainerAsync(creator).Result;
                container.Name = creator.Name;
                container.Image = creator.Image;
                container.Id = reply.ID;
                jsonManager.SaveContainerAsync(container);
                db_shard.Add(new gameservers
                {
                    container_id = container.Id,
                    updated_at = DateTime.Now,
                    created_at = DateTime.Now,
                    id = 1,
                    shard_id = ConfigsHelper.Config().Result.ShardID
                });
                return container.Id;
                
            }
            //check if the same container exists
            else if (currentContainers[0].Names[0] == "/" + container.Name && currentContainers[0].Image == container.Image)
            {
                logger.LogWarning("Container exists!");
            }
        }
        
        return null;
    }
}