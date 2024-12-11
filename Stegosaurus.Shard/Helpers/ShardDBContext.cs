using Docker.DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace Stegosaurus.Shard.Helpers;

public class ShardDBContext :DbContext
{
    public DbSet<Container> Containers { get; set; }
    public DbSet<Request> Requests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}

public class Container()
{
    public string? Id { get;set; }
    public string Image { get;set; }
    public string? Name { get;set; }
}

public class Request()
{
    public CreateContainerParameters CreateContainerParameters { get; init; }

}