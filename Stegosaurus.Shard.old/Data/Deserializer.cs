using Docker.DotNet.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stegosaurus.Shard.Json;

namespace Stegosaurus.Shard.Data;

public class Deserializer
{
    /// <summary>
    ///     Assess the type of class we got from a message and returns it
    /// </summary>
    /// <param name="packet"></param>
    /// <returns>Object thats been deserialized</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<object> AssessType(Packet packet)
    {
        var request = packet.Message;
        var cleanedObj = JsonConvert.DeserializeObject(packet.Data);
        if (cleanedObj is null) throw new NullReferenceException();
        var cleanedJson = JObject.Parse(JsonConvert.SerializeObject(cleanedObj));


        switch (request)
        {
            case "Crea    tion":
                Worker._logger.LogWarning("Found type id for jobject " + cleanedJson.First);
                var cleanClass = _creation(cleanedJson);
                return cleanClass;
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
    ///     single use classes to convert the jobject to the actual class
    /// </summary>
    /// <param name="jObject"></param>
    /// <returns></returns>
    private Container _container(JObject jObject)
    {
        var container = new Container();
        if (!jObject.ContainsKey("id"))
        {
            container.Id = null;
        }
        container.Name = jObject["name"].ToString().Normalize();
        container.Image = jObject["image"].ToString().Normalize();

        return container;
    }
    
    /// <summary>
    ///     single use classes to convert the jobject to the actual class
    /// </summary>
    /// <param name="jObject"></param>
    /// <returns></returns>

    public CreateContainerParameters _creation(JObject jObject)
    {
        var creation = jObject.ToObject<CreateContainerParameters>();
        //i have no clue why it doesn't grab the name
        creation.Name = (string)jObject["Name"];
        return creation;
    }
}