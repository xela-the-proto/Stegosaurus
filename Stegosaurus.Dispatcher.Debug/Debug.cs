using System.Text;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Stegosaurus.Dispatcher.Debug.JSON;

namespace Stegosaurus.Dispatcher.Debug;

public partial class Debug : Form
{
    public static Dictionary<string, ThreadCanc> threadDictionary = new Dictionary<string, ThreadCanc>();
    public static CancellationTokenSource CANCEL_BROADCAST = new CancellationTokenSource();
    public StatusStrip statusStrip;
    public Debug()
    {
        InitializeComponent();
    }
    
    private async void ReceiveIDs(object? state)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            
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
        int i = 0;
        dbg_log.AppendText("[INFO] Starting discovery...\n");
        while (true)
        {
            statusStrip.Items[0].Text = "thread=" + i++;
            if (CANCEL_BROADCAST.IsCancellationRequested)
            {
                dbg_log.AppendText("[CRITICAL] Discovery canceled!\n");
                return;
            }
            consumer.ReceivedAsync += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (message is not null)
                {
                    dbg_log.AppendText("[WARNING] Received id: " + message + "\n");
                    StoreID(message);
                }
                
                return Task.CompletedTask;
            };
            await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);
            Thread.Sleep(1000);
        }
    }

    private void btn_break_discovery_Click_1(object sender, EventArgs e)
    {
        dbg_log.AppendText("[CRITICAL] Killing discovery...\n");
        CANCEL_BROADCAST.Cancel();
        CANCEL_BROADCAST.Dispose();    
    }
    private async void btn_connect_lin_Click(object sender, EventArgs e)
    {
        string msg;
        var factory = new ConnectionFactory
        {
            HostName = "servers.xela.space",
            
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

        await channel.BasicPublishAsync("dispatch", txt_id.Text + ".creation", body);
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
    }
    
    private void StoreID(string message)
    {
        
    }

    private async void btn_start_Click(object sender, EventArgs e)
    {
        string msg;
        var factory = new ConnectionFactory
        {
            HostName = "servers.xela.space",
        }; 
        
        dbg_log.AppendText("[INFO] Connecting to RabbitMQ...\n");
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync("dispatch", ExchangeType.Topic);
        dbg_log.AppendText("[INFO] Declaring exchange to RabbitMQ...\n");

        //await channel.QueueDeclareAsync(queue: "Creation", durable: false, exclusive: false, autoDelete: false, arguments: null);
        // read JSON directly from a file
        using (var file = File.OpenText(@"C:\Users\thega\AppData\Roaming\StegoShard\request.json"))
        using (var reader = new JsonTextReader(file))
        {
            var o2 = (JObject)JToken.ReadFrom(reader);
            msg = o2.ToString();
        }
        var body = Encoding.UTF8.GetBytes(msg);
        await channel.BasicPublishAsync("dispatch", txt_id.Text + ".start." + txt_container_id.Text, null);
        dbg_log.AppendText("[INFO] Sending message to RabbitMQ...\n");
    }
}