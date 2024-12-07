using Dapper;
using MySql.Data.MySqlClient;

namespace Stegosaurus.Shard.Helpers;

public class DapperHelpers
{
    public static async Task ConnectAsync()
    {
        // using Dapper;
        var connectionString = "Server=192.168.1.140;database=shard_manager;Uid=xela;Pwd=antD)nIo3]dKvp7K;Charset=utf8;Port=3307;SslMode=none";
        var connection = new MySqlConnection(connectionString);
    } 
}