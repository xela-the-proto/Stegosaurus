using System.Net.Sockets;

namespace Stegosaurus.Shard.Net;

public class SocketExtensions
{
    public static bool IsConnected(Socket socket)
    {
        try
        {
            return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
        }
        catch (SocketException) { return false; }
    }
}