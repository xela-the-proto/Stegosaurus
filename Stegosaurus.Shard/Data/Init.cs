using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.Json;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Shard;

public class Init
{
    /// <summary>
    ///     Check and if needed creates all the needed directories
    /// </summary>
    public async Task LocalFiles()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (!new DirectoryInfo(JsonManager.WIN_ROOT).Exists) Directory.CreateDirectory(JsonManager.WIN_ROOT);

            if (!new DirectoryInfo(JsonManager.WIN_ROOT + @"/configs").Exists)
                Directory.CreateDirectory(JsonManager.WIN_ROOT + @"/configs");

            if (!new DirectoryInfo(JsonManager.WIN_ROOT + @"/containers").Exists)
                Directory.CreateDirectory(JsonManager.WIN_ROOT + @"/containers");
            return;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            if (!new DirectoryInfo(JsonManager.LIN_ROOT).Exists) Directory.CreateDirectory(JsonManager.LIN_ROOT);

            if (!new DirectoryInfo(JsonManager.LIN_ROOT + @"/configs").Exists)
                Directory.CreateDirectory(JsonManager.LIN_ROOT + @"/configs");

            if (!new DirectoryInfo(JsonManager.LIN_ROOT + @"/containers").Exists)
                Directory.CreateDirectory(JsonManager.LIN_ROOT + @"/containers");
        }
    }

    /// <summary>
    ///     creates the config files if they don't exist, else returns them
    /// </summary>
    /// <returns></returns>
    public async Task<ShardConfig> Config()
    {
        var serializer = new JsonSerializer();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (!File.Exists(JsonManager.WIN_ROOT + @"/configs/shard.json"))
            {
                var shardConfig = new ShardConfig
                {
                    Ip = "localhost",
                    ShardID = GenerateID.Generate().Result,
                    Broadcast = false
                };
                using (var stream = File.CreateText(JsonManager.WIN_ROOT + @"/configs/shard.json"))
                {
                    serializer.Serialize(stream, shardConfig);
                }
                shardConfig.Broadcast = true;
                return shardConfig;
            }

            using (var stream = File.OpenText(JsonManager.WIN_ROOT + @"/configs/shard.json"))
            {
                return (ShardConfig)serializer.Deserialize(stream, typeof(ShardConfig));
            }
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            if (!File.Exists(JsonManager.LIN_ROOT + @"/configs/shard.json"))
            {
                var shardConfig = new ShardConfig
                {
                    Ip = "localhost"
                };
                using (var stream = File.CreateText(JsonManager.LIN_ROOT + @"/configs/shard.json"))
                {
                    serializer.Serialize(stream, shardConfig);
                }

                return shardConfig;
            }

            using (var stream = File.OpenText(JsonManager.LIN_ROOT + @"/configs/shard.json"))
            {
                return (ShardConfig)serializer.Deserialize(stream, typeof(ShardConfig));
            }
        }

        return null;
    }
}