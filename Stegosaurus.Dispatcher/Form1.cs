using System;
using System.IO;
using System.Text;
using System.Threading.Channels;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;

namespace Stegosaurus.Dispatcher;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private async void btn_connect_exchange_windows_Click(object sender, EventArgs e)
    {
        
        string msg = "";
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
            
        await channel.ExchangeDeclareAsync(exchange: "dispatch", type: ExchangeType.Topic);
        //await channel.QueueDeclareAsync(queue: "Creation", durable: false, exclusive: false, autoDelete: false, arguments: null);

        const string message = "Hello World!";
        // read JSON directly from a file
        using (StreamReader file = File.OpenText(@"C:\Users\thega\AppData\Roaming\StegoShard\request.json"))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            JObject o2 = (JObject)JToken.ReadFrom(reader);
            msg = o2.ToString();
        }

        var body = Encoding.UTF8.GetBytes(msg);

        await channel.BasicPublishAsync(exchange: "dispatch", routingKey: "localhost.creation", body: body);
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
    }
}