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
    public async Task CreateContainer(Container container)
    {
        JsonManager jsonManager = new JsonManager();
        Finder finder = new Finder();
        var client = Worker.client;
        var logger = Worker._logger;

        
            logger.LogInformation("Downloading image " + container.image);
            //pull image from the docker repo
            await client.Images.CreateImageAsync(
                new ImagesCreateParameters
                {
                    FromImage = container.image,
                },
                new AuthConfig
                {
                    Email = null,
                    Username = null,
                    Password = null
                },
                new Progress<JSONMessage>());

            //gets the containers that match the name for checks
            var currentContainers = finder.Find("name",container.name).Result;
            //if we dont have any containers then we create
            if (!currentContainers.Any())
            {
                logger.LogInformation("creating container with name " + container.name + " and image " + container.image);
                CreateContainerResponse reply = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
                {
                    Name = container.name,
                    Image = container.image,
                    Cmd = new List<string>
                    {
                        "/bin/bash",
                        "-c",
                        "sudo apt install snapd"
                    },
                    HostConfig = new HostConfig()
                    {
                        DNS = new[] { "8.8.8.8", "8.8.4.4" }
                    }
                
                });
                container.id = reply.ID;
                jsonManager.SaveContainerAsync(container);
                //check if the same container exists
            }else if (currentContainers[0].Names[0] == "/" + container.name && currentContainers[0].Image == container.image)
            { 
                logger.LogWarning("Container exists!");
            }
    }
}