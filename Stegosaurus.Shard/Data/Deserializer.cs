using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Stegosaurus.Shard.Json;

public class Deserializer
{
    public async Task<object> AssessType(string msg, bool deserialize)
    {
        
        object? cleaned_obj = JsonConvert.DeserializeObject(msg);
        if (cleaned_obj is null)
        {
            throw new NullReferenceException();
        }
        JObject cleaned_json = JObject.Parse(JsonConvert.SerializeObject(cleaned_obj));
        
        string request = cleaned_json["request_type"].ToString().Normalize();
        if (true)
        {
            switch (request)
            {
                case "creation":
                    Worker._logger.LogWarning("Found type id for jobject " + cleaned_json.ToString());
                    Container clean_class = this.Container(cleaned_json);
                    return clean_class;
                case "shutdown":
                    Environment.Exit(1);
                    break;
                case null:
                    Worker._logger.LogCritical("Badly formed packet exiting!");
                    Environment.Exit(2);
                    break;
            }
        }

        return null;
    }
    
    
    private Container Container(JObject jObject)
    {
        Json.Container container = new Json.Container();
        if (!jObject.ContainsKey("id"))
        {
            container.id = null;
        }
        container.name = jObject["name"].ToString().Normalize();
        container.image = jObject["image"].ToString().Normalize();

        return container;
    }

}