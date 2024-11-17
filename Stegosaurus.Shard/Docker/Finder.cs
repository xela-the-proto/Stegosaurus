using Docker.DotNet.Models;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Docker;

public class Finder
{
    /// <summary>
    /// Kind of a "helper" to find containers, shoudl be filled like ("name",foo_name)
    /// </summary>
    /// <param name="container"></param>
    /// <param name="key_filter"></param>
    /// <param name="value_filter"></param>
    /// <param name="get_offline"></param>
    public async Task<IList<ContainerListResponse>> Find(string attribute, string attribute_to_match, bool get_offline = true)
    {
        var client = Worker.client;
        var currentContainers = await client.Containers.ListContainersAsync(
            new ContainersListParameters()
            {
                All = get_offline,
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        attribute, new Dictionary<string, bool>
                        {
                            [attribute_to_match] = true
                        }
                    }
                }
            });
        return currentContainers;
    }

}