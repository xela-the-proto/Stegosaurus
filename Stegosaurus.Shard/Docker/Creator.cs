using Docker.DotNet.Models;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Docker;

public class Creator
{
    public async Task CreateContainer(Container container)
    {
        JsonManager jsonManager = new JsonManager();
        var client = Init.client;
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
        var currentContainers = await client.Containers.ListContainersAsync(
            new ContainersListParameters(){
                All = true,
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        "name", new Dictionary<string, bool>
                        {
                            [container.name] = true
                        }
                    }
                }});
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
                    "echo hello world!"
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