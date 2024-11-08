using Docker.DotNet.Models;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Docker;

public class Finder
{
    public async Task Find(Container container, string key_filter, string value_filter, bool get_offline = true)
    {
        var client = Worker.client;
        var currentContainers = await client.Containers.ListContainersAsync(
            new ContainersListParameters()
            {
                All = get_offline,
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        key_filter, new Dictionary<string, bool>
                        {
                            [value_filter] = true
                        }
                    }
                }
            });
    }

}