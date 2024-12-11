namespace Stegosaurus.Dispatcher.Debug.Json;

/// <summary>
///     JSON cookie cutter class for config
/// </summary>
public class ShardConfig
{
    public string ShardID { get; set; }
    public string Ip { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Broadcast { get; set; }
}