using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RabbitMQ.Client;

namespace Stegosaurus.Dispatcher;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static IPHostEntry host = Dns.GetHostEntry("localhost");
    public static IPAddress ipAddress = host.AddressList[0];
    public static IPEndPoint remoteEP = new IPEndPoint(ipAddress, 53871); 
    public static TcpClient socket = new();     
    public MainWindow()
    {
        InitializeComponent();
        Application.Current.MainWindow.Closing += Window_Closed;
        btn_connect.Click += myButton_Click;

        /*
        while (true)
        {
            try
            {
            /*
            byte[] bytes = new byte[1024];
            // Connect the socket to the remote endpoint. Catch any errors.

                if (!sender.Connected)
                {
                    sender.Connect(remoteEP);
                }
                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes(
                    "{\n    \"request_type\":\"creation\",\n    \"id\":\"81b043a3d64169dab949461b9ef48b444891a387f02e4c9c9160ab3bf9bada66\",\n    \"name\":\"send\",\n    \"image\":\"ghcr.io/pterodactyl/yolks:debian\"\n}");

                // Send the data through the socket.
                int bytesSent = sender.Send(msg);

                var buffer = new byte[1_024];

                socket.Connect(remoteEP);
                using NetworkStream stream = socket.GetStream();
                string message_to_send =
                    "{\n    \"request_type\":\"creation\",\n    \"id\":\"81b043a3d64169dab949461b9ef48b444891a387f02e4c9c9160ab3bf9bada66\",\n    \"name\":\"cde\",\n    \"image\":\"ghcr.io/pterodactyl/yolks:alpine\"\n}";
                var msg = Encoding.UTF8.GetBytes(message_to_send);
                stream.Write(msg);

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
            Thread.Sleep(1000);
        }
        */
    }
    async void myButton_Click(object sender, RoutedEventArgs e)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "Docker-container", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        const string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
    }
    private void Window_Closed(object sender, EventArgs e)
    {
    }
    
}