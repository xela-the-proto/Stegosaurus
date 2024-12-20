using Docker.DotNet.Models;

namespace Stegosaurus.Shard.Json;

//TODO:RESPOND WITH HEALTH CHECKS?

public class Creation
{
    public CreateContainerParameters CreateContainerParameters { get; init; }
}