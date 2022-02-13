using Microsoft.Extensions.Configuration;
using Npgsql;

Console.WriteLine("Starting Old Roster Flood...");

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var conn = new NpgsqlConnection(configuration.GetConnectionString("OldRoster"));
conn.Open();

conn.Notification += (o, e) => { 
    Console.WriteLine($"Received event {e.Payload}."); 
};

using (var cmd = new NpgsqlCommand("LISTEN members_stream", conn))
{
    cmd.ExecuteNonQuery();
}

while(true)
{
    conn.Wait();
}