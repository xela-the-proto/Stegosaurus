using System.ComponentModel.Design.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Stegosaurus.Dispatcher;

public partial class Form1 : Form
{
    public static Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
    public static CancellationTokenRegistration
    public Form1()
    {
        
        Thread myThread = new Thread(() => ReceiveIDs());
        myThread.Name = Convert.ToString("ReceiveIDs");
        myThread.Start();
        threadDictionary.Add("ReceiveIDs", myThread);
        InitializeComponent();
    }

    private async void btn_connect_exchange_windows_Click(object sender, EventArgs e)
    {
        string msg;
        var factory = new ConnectionFactory
        {
            HostName = "game.xela.space",
            UserName = "admin",
            Password = "admin"
        };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync("dispatch", ExchangeType.Topic);
        //await channel.QueueDeclareAsync(queue: "Creation", durable: false, exclusive: false, autoDelete: false, arguments: null);
        // read JSON directly from a file
        using (var file = File.OpenText(@"C:\Users\thega\AppData\Roaming\StegoShard\request.json"))
        using (var reader = new JsonTextReader(file))
        {
            var o2 = (JObject)JToken.ReadFrom(reader);
            msg = o2.ToString();
        }

        var body = Encoding.UTF8.GetBytes(msg);

        await channel.BasicPublishAsync("dispatch", "0.creation", body);
    }

    private async void btn_connect_exchange_linux_Click(object sender, EventArgs e)
    {
        string msg;
        var factory = new ConnectionFactory
        {
            HostName = "game.xela.space",
            UserName = "admin",
            Password = "admin"
        };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync("dispatch", ExchangeType.Topic);
        //await channel.QueueDeclareAsync(queue: "Creation", durable: false, exclusive: false, autoDelete: false, arguments: null);

        const string message = "Hello World!";
        // read JSON directly from a file
        using (var file = File.OpenText(@"C:\Users\thega\AppData\Roaming\StegoShard\request.json"))
        using (var reader = new JsonTextReader(file))
        {
            var o2 = (JObject)JToken.ReadFrom(reader);
            msg = o2.ToString();
        }

        var body = Encoding.UTF8.GetBytes(msg);

        await channel.BasicPublishAsync("dispatch", "1.creation", body);
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
    }
    
    private async void ReceiveIDs()
    {
        var factory = new ConnectionFactory
        {
            HostName = "game.xela.space",
            UserName = "admin",
            Password = "admin"
        };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.ExchangeDeclareAsync(exchange:"id",
            type: ExchangeType.Fanout);

        // declare a server-named queue
        QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
        string queueName = queueDeclareResult.QueueName;
        await channel.QueueBindAsync(queue: queueName, exchange: "id", routingKey: string.Empty);
        var consumer = new AsyncEventingBasicConsumer(channel);
        while (true)
        {
            consumer.ReceivedAsync += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (message is not null)
                {
                    Console.WriteLine($" [x] {message}");
                    CheckID(message);
                    return Task.CompletedTask;
                }
                
                return Task.CompletedTask;
            };
            await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);
            Thread.Sleep(1000);
        }
    }

    private void CheckID(string message)
    {
        threadDictionary["ReceiveIDs"].Interrupt();
    }
}