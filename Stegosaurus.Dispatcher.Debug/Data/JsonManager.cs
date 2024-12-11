using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Stegosaurus.Dispatcher.Debug.Json;
using Stegosaurus.Dispatcher.Debug.JSON;

namespace Stegosaurus.Shard.Data;

public class JsonManager
{
    public static string WIN_ROOT =
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/StegoShard";

    public static string LIN_ROOT = "/etc/stegoshard";

    /// <summary>
    ///     Save a container to a json file to keep track
    /// </summary>
    /// <param name="toSerialize"></param>
    public async Task SaveContainerAsync(Container toSerialize)
    {
        if (!File.Exists(WIN_ROOT + @"/containers/" + toSerialize.Id + ".json") &&
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            using (var file = File.CreateText(WIN_ROOT + @"/containers/" + toSerialize.Id + ".json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, toSerialize);
            }
        else if (File.Exists(LIN_ROOT + @"/containers/" + toSerialize.Id + ".json") &&
                 RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            using (var file = File.CreateText(LIN_ROOT + @"/containers/" + toSerialize.Id + ".json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, toSerialize);
            }
    }
}