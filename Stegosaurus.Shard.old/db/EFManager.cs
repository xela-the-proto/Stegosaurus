using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Stegosaurus.Shard.db;

public class EFManagerGameServers : DbContext
{
    public DbSet<GameServers>  Gameservers { get; set; }

    private static string connectionString = ConfigurationManager.ConnectionStrings["DevDB"].ConnectionString; 
    MariaDbServerVersion serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString));
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString,serverVersion).LogTo(Console.WriteLine, LogLevel.Debug)
            .EnableSensitiveDataLogging().EnableDetailedErrors();
    }
}


public class GameServers
{
    public string shard_id { get; set; }
    public string container_id { get; set; }
    public Status status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int id { get; set; }
}

public class Shard
{
    public string shard_id { get; set; }
    public List<GameServers> GameserversList { get; set; }
    public string ip { get; set; }
}

public enum Status
{
    running,
    stopped,
    error,
    created
}