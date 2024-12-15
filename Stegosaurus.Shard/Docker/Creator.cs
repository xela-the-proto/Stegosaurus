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
        var db_shard = new EFManagerGameServers();
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
                }, new Progress<JSONMessage>(m => logger.LogInformation(m.Status)));
            
            logger.LogInformation("creating container with name " + creator.Name + " and image " + creator.Image);
            var reply = client.Containers.CreateContainerAsync(creator).Result;
            container.Name = creator.Name;
            container.Image = creator.Image;
            container.Id = reply.ID;
            jsonManager.SaveContainerAsync(container);
            db_shard.Add(new GameServers
            {
                shard_id = ConfigsHelper.Config().Result.ShardID,
                container_id = container.Id,
                updated_at = DateTime.Now,
                created_at = DateTime.Now,
                status = Status.created
            });
            return container.Id;
        }
    }
}