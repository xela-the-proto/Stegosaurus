using Docker.DotNet.Models;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Docker;

public class Creator
{
    /// <summary>
    ///     Downloads the image of the container and then creates it, also checks if it exists.
    /// </summary>
    /// <param name="creator"></param>
    /// <exception cref="TimeoutException"></exception>
    public async Task CreateContainer(CreateContainerParameters creator)
    {
        var container = new Container();
        var jsonManager = new JsonManager();
        var finder = new Finder();
        var client = Worker.Client;
        var logger = Worker._logger;
        //pull image from the docker repo
        await client.Images.CreateImageAsync(
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
        //if we dont have any containers then we create
        if (!currentContainers.Any())
        {
            logger.LogInformation("creating container with name " + creator.Name + " and image " + creator.Image);
            var reply = await client.Containers.CreateContainerAsync(creator);
            container.Name = creator.Name;
            container.Image = creator.Image;
            container.Id = reply.ID;
            jsonManager.SaveContainerAsync(container);
            //check if the same container exists
        }
        else if (currentContainers[0].Names[0] == "/" + container.Name && currentContainers[0].Image == container.Image)
        {
            logger.LogWarning("Container exists!");
        }
    }
}