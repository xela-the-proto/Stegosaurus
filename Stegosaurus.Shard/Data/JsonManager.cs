using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Data;

public class JsonManager
{

    public static string WIN_ROOT = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/StegoShard";
    public static string LIN_ROOT = "/etc/stegoshard";

    /// <summary>
    /// Save a container to a json file to keep track
    /// </summary>
    /// <param name="ToSerialize"></param>
    public async Task SaveContainerAsync(Container ToSerialize)
    {
        if (!File.Exists(WIN_ROOT + @"/containers/" + ToSerialize.id + ".json") && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            using (StreamWriter file = File.CreateText(WIN_ROOT + @"/containers/" + ToSerialize.id + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, ToSerialize);
            }
        }else if (File.Exists(LIN_ROOT + @"/containers/" + ToSerialize.id + ".json") &&
                  RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            using (StreamWriter file = File.CreateText(LIN_ROOT + @"/containers/" + ToSerialize.id + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, ToSerialize);
            }
        }
        
        
    }
}