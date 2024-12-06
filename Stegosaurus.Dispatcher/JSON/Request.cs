using Docker.DotNet.Models;

namespace Stegosaurus.Dispatcher.Json;

//TODO:RESPOND WITH HEALTH CHECKS?

public class Creation
{
    public CreateContainerParameters CreateContainerParameters { get; init; }
}