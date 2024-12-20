using System.Net.Sockets;
using Docker.DotNet.Models;

namespace Stegosaurus.shard.Json.Classes;

public class ShardJSON
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Tty { get; set; }
}