using Docker.DotNet.Models;

namespace Stegosaurus.Shard.Docker;

public class Finder
{
    /// <summary>
    ///     Kind of a "helper" to find containers, shoudl be filled like ("name",foo_name)
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="attributeToMatch"></param>
    /// <param name="getOffline"></param>
    public async Task<IList<ContainerListResponse>> Find(string attribute, string attributeToMatch,
        bool getOffline = true)
    {
        var client = Worker.Client;
        var currentContainers = await client.Containers.ListContainersAsync(
            new ContainersListParameters
            {
                All = getOffline,
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        attribute, new Dictionary<string, bool>
                        {
                            [attributeToMatch] = true
                        }
                    }
                }
            });
        return currentContainers;
    }
}