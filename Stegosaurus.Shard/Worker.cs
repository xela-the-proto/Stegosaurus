using Docker.DotNet;
using Stegosaurus.Shard.Docker;

namespace Stegosaurus.Shard;

public class Worker : BackgroundService
{
    public static ILogger<Worker> _logger;
    
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Creator creator = new Creator();
        Init init = new Init();
        await init.Local();
        //simualate if we get a message
        //eventually here we await a network call telling us what to do
        while (!stoppingToken.IsCancellationRequested)
        {
            if (true)
            {
                _logger.LogWarning("Got socket message creating container");
                await creator.CreateContainer("minecraft", "itzg/minecraft-server");
            }
            else if(true)
            {
                _logger.LogInformation("[" + DateTime.Now + "]" + " Idling");
            }
            
            await Task.Delay(500, stoppingToken);
        }
        
        
    }
}