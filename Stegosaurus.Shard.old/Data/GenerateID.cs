using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Stegosaurus.Shard.Data;

namespace Stegosaurus.Shard.Net;

public class GenerateID
{
    public Task<string> Generate()
    {
        string ID ="";
        var fieldOptions = new FieldOptionsTextWords();
        fieldOptions.Max = 1;
        var randomWords = RandomizerFactory.GetRandomizer(fieldOptions);
        var randomHex = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN());
        string word1 = randomWords.Generate();
        string word2 = randomWords.Generate();
        string iban = randomHex.Generate();
        ID = word1 + "-" + word2 + "-" + iban;
        string shuffled;
        do
        {
            shuffled = new string(
                ID
                    .OrderBy(character => Guid.NewGuid())
                    .ToArray()
            );
        } while (shuffled == ID);
        var shuffled_trimmed = shuffled.Remove(35);
       
        return Task.FromResult(shuffled);
    }

    public Task<string> GetID()
    {
        if (File.Exists(JsonManager.WIN_ROOT + "/configs/shard.json") && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            using (StreamReader file = File.OpenText(JsonManager.WIN_ROOT + "/configs/shard.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return Task.FromResult(o2["ShardID"].ToString());
            }
        }
        if (File.Exists(JsonManager.LIN_ROOT + "/configs/shard.json") && RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            using (StreamReader file = File.OpenText(JsonManager.LIN_ROOT + "/configs/shard.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return Task.FromResult(o2["ShardID"].ToString());
            }
        }

        return null;
    }
}