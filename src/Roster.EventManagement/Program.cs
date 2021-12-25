using SteamQueryNet.Interfaces;
using SteamQueryNet;
using Newtonsoft.Json;

string serverIp = "194.68.41.7";
ushort serverPort = 2303;

IServerQuery serverQuery = new ServerQuery(serverIp, serverPort);
var players = serverQuery.GetPlayers();

foreach (var player in players)
{
    string json = JsonConvert.SerializeObject(player);
    Console.WriteLine(json);
}