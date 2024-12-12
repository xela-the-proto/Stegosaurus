using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Stegosaurus.Shard.db;

public class EFManagerGameServers : DbContext
{
    public DbSet<gameservers>  Gameservers { get; set; }

    private static string connectionString = ConfigurationManager.ConnectionStrings["DevDB"].ConnectionString; 
    MariaDbServerVersion serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString));
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString,serverVersion).LogTo(Console.WriteLine, LogLevel.Debug)
            .EnableSensitiveDataLogging().EnableDetailedErrors();
    }
}


public class gameservers
{
    public int id { get; set; }
    public string shard_id { get; set; }
    public string container_id { get; set; }
    public status status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}

public enum status
{
    running,
    stopped,
    error
}