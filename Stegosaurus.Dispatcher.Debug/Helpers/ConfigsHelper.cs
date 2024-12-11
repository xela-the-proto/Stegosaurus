using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Stegosaurus.Dispatcher.Debug.Json;
using Stegosaurus.Shard.Data;
using Stegosaurus.Shard.Net;

namespace Stegosaurus.Dispatcher.Debug.Helpers;

public class ConfigsHelper
{
    /// <summary>
    ///     Check and if needed creates all the needed directories
    /// </summary>
    public static async Task LocalFiles()
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
    public static async Task<ShardConfig> Config()
    {
        var serializer = new JsonSerializer();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (!File.Exists(JsonManager.WIN_ROOT + @"/configs/shard.json"))
            { 
                var shardConfig = new ShardConfig
                {
                    Ip = "localhost",
                    Username = "changeme",
                    Password = "changeme",
                    ShardID = GenerateID.Generate().Result,
                };
                using (var stream = File.CreateText(JsonManager.WIN_ROOT + @"/configs/shard.json"))
                {
                    serializer.Serialize(stream, shardConfig);
                }
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