using Newtonsoft.Json;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Data;

public class JsonManager
{

    private static string ROOT = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StegoShard";
    public async Task SaveContainerAsync(Container ToSerialize)
    {
        await Init();
        if (!File.Exists(ROOT + @"\containers\" + ToSerialize.id + ".json"))
        {
            using (StreamWriter file = File.CreateText(ROOT + @"\containers\" + ToSerialize.id + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, ToSerialize);
            }
        }
        
    }

    protected async Task Init()
    {
        if (!Directory.Exists(ROOT + @"\containers"))
        {
            Directory.CreateDirectory(ROOT + @"\containers");
        }
    }
}