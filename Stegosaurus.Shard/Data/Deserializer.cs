using Docker.DotNet.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Stegosaurus.Shard.Json;

public class Deserializer
{
    /// <summary>
    ///     Assess the type of class we got from a message and returns it
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="deserialize"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<object> AssessType(Packet packet)
    {
        var request = packet.message;
        var cleaned_obj = JsonConvert.DeserializeObject(packet.data);
        if (cleaned_obj is null) throw new NullReferenceException();
        var cleaned_json = JObject.Parse(JsonConvert.SerializeObject(cleaned_obj));


        switch (request)
        {
            case "Crea    tion":
                Worker._logger.LogWarning("Found type id for jobject " + cleaned_json.First);
                var clean_class = _creation(cleaned_json);
                return clean_class;
            case "shutdown":
                Environment.Exit(1);
                break;
            case null:
                Worker._logger.LogCritical("Badly formed packet exiting!");
                Environment.Exit(2);
                break;
        }

        return null;
    }

    /// <summary>
    ///     single use class to convert the jobject to the actual class
    /// </summary>
    /// <param name="jObject"></param>
    /// <returns></returns>
    private Container _container(JObject jObject)
    {
        var container = new Container();
        if (!jObject.ContainsKey("id"))
        {
            container.id = null;
        }
        container.name = jObject["name"].ToString().Normalize();
        container.image = jObject["image"].ToString().Normalize();

        return container;
    }

    public CreateContainerParameters _creation(JObject jObject)
    {
        var creation = jObject.ToObject<CreateContainerParameters>();
        //i have no clue why it doesn't grab the name
        creation.Name = (string)jObject["Name"];
        return creation;
    }
}