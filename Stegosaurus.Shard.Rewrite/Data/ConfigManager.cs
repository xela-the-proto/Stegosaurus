using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Serilog;
using Stegosaurus.shard.Json.Classes;

namespace Stegosaurus.shard.Data;

public class ConfigManager
{
    public static async Task<ShardJSON> ReadConfig()
    {
        try
        {
            /*
             * Check if the main directories exists
             */
            if (!Directory.Exists(Program.WIN_ROOT))
            {
                Directory.CreateDirectory(Program.WIN_ROOT);    
            }
            if (!Directory.Exists(Program.LIN_ROOT))
            {
                Directory.CreateDirectory(Program.LIN_ROOT);    
            }
            
            /*
             * Read the config, if its in someway empty throw FileLoadException, else return the class
             */
            var serializer = new JsonSerializer();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return serializer.Deserialize(File.OpenText(Program.WIN_ROOT + @"\config.json"),typeof(ShardJSON))
                    as ShardJSON ?? throw new FileLoadException(); 
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return serializer.Deserialize(File.OpenText(Program.LIN_ROOT + @"\config.json"),typeof(ShardJSON))
                    as ShardJSON ?? throw new FileLoadException(); 
            }
        }
        catch (FileNotFoundException e)
        {
            Log.Fatal("Couldnt read file!\n" + e.Message);
            return null;
        }

        return null;
    }

    public static async Task WriteConfig(ShardJSON shard)
    {
        /*
         * Write the config the the root path
         */
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
             File.WriteAllText(Program.WIN_ROOT + @"\config.json",JsonConvert.SerializeObject(shard));
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            File.WriteAllText(Program.LIN_ROOT + @"\config.json", JsonConvert.SerializeObject(shard));
        }
        
    }
        
}