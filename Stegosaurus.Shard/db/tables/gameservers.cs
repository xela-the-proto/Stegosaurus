namespace Stegosaurus.Shard.db.tables;

public class gameservers
{
    public int id { get; set; }
    public string shard_id { get; set; }
    public string container_id { get; set; }
    public status status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}

public enum status
{
    running,
    stopped,
    error
}