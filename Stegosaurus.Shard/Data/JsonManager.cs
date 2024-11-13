using Newtonsoft.Json;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Data;

public class JsonManager
{

    public static string ROOT = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StegoShard";
    /// <summary>
    /// Save a container to a json file to keep track
    /// </summary>
    /// <param name="ToSerialize"></param>
    public async Task SaveContainerAsync(Container ToSerialize)
    {
        if (!File.Exists(ROOT + @"\containers\" + ToSerialize.id + ".json"))
        {
            using (StreamWriter file = File.CreateText(ROOT + @"\containers\" + ToSerialize.id + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, ToSerialize);
            }
        }
        
    }
}