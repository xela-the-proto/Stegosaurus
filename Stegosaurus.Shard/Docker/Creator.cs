using Docker.DotNet.Models;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.Json;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard.Docker;

public class Creator
{
    
    /// <summary>
    /// Downloads the image of the container and then creates it, also checks if it exists.
    /// </summary>
    /// <param name="container"></param>
    /// <exception cref="TimeoutException"></exception>
    public async Task CreateContainer(CreateContainerParameters creator)
    {
        Container container = new Container();
        JsonManager jsonManager = new JsonManager();
        Finder finder = new Finder();
        var client = Worker.client;
        var logger = Worker._logger;
            //pull image from the docker repo
            await client.Images.CreateImageAsync(
                new ImagesCreateParameters
                {
                    FromImage = creator.Image,
                },
                new AuthConfig
                {
                    Email = null,
                    Username = null,
                    Password = null
                },
                new Progress<JSONMessage>(m => logger.LogInformation(m.ProgressMessage)));

            //gets the containers that match the name for checks
            var currentContainers = finder.Find("name",creator.Name).Result;
            //if we dont have any containers then we create
            if (!currentContainers.Any())
            {
                logger.LogInformation("creating container with name " + creator.Name + " and image " + creator.Image);
                CreateContainerResponse reply = await client.Containers.CreateContainerAsync(creator);
                container.name = creator.Name;
                container.image = creator.Image;
                container.id = reply.ID;
                jsonManager.SaveContainerAsync(container);
                //check if the same container exists
            }else if (currentContainers[0].Names[0] == "/" + container.name && currentContainers[0].Image == container.image)
            { 
                logger.LogWarning("Container exists!");
            }
    }
}