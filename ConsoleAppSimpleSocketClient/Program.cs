using System.Text.Json;
using ConsoleAppSimpleSocketClient;
using ConsoleAppSimpleSocketClient.NetEngine;
using ConsoleAppSimpleSocketClient.NetModel;

ClientEngine clientEngine = new ClientEngine("127.0.0.1", 34536);
clientEngine.ConnectToServer();

Man man = new Man()
{
    Name = "Mik",
    Age = 17
};

Request request = new Request()
{
    Command = Commands.AddAge,
    JsonData = JsonSerializer.Serialize(man)
};

string messageToServer = JsonSerializer.Serialize(request);

clientEngine.SendMessage(messageToServer);
string messageFromServer = clientEngine.ReceiveMessage();

Response response = JsonSerializer.Deserialize<Response>(messageFromServer);

if (response.Status == Statuses.Ok)
{
    Man receivedMan = JsonSerializer.Deserialize<Man>(response.JsonData);
    Console.WriteLine(receivedMan);
}
else
{
    Console.WriteLine($"Something wrong Status = {response.Status}");
}

clientEngine.CloseClientSocket();