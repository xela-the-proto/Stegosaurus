namespace Stegosaurus.Shard.Json;

/// <summary>
///     JSON cookie cutter class for config
/// </summary>
public class ShardConfig
{
    public string ShardID { get; set; }
    public string Ip { get; set; }
    
    public bool Broadcast { get; set; }
}