using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard;
using Docker;
public class Init
{
    /// <summary>
    /// Check and if needed creates all the needed directories
    /// </summary>
    public async Task Local()
    {
        if (!new DirectoryInfo(JsonManager.ROOT).Exists)
        {
            Directory.CreateDirectory(JsonManager.ROOT);
        }

        if (!new DirectoryInfo(JsonManager.ROOT + @"\configs").Exists)
        {
            Directory.CreateDirectory(JsonManager.ROOT + @"\configs");
        }

        if (!new DirectoryInfo(JsonManager.ROOT + @"\containers").Exists)
        {
            Directory.CreateDirectory(JsonManager.ROOT + @"\containers");
        }
        return;
    }

    /// <summary>
    /// creates the config files if they don't exist, else returns them
    /// </summary>
    /// <returns></returns>
    public async Task<ShardConfig> Config()
    {
        JsonSerializer serializer = new JsonSerializer();
        if (!File.Exists(JsonManager.ROOT+ @"\configs\shard.conf"))
        {
            ShardConfig shardConfig = new ShardConfig
            {
                ip = "localhost"
            };
            using (StreamWriter stream = File.CreateText(JsonManager.ROOT + @"\configs\shard.conf"))
            {
                
                serializer.Serialize(stream,shardConfig);
            }

            return shardConfig;
        }
        else
        {
            using (StreamReader stream = File.OpenText(JsonManager.ROOT + @"\configs\shard.conf"))
            {
                return (ShardConfig)serializer.Deserialize(stream,typeof(ShardConfig));
            }
        }
    }
}