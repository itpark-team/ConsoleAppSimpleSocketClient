using System.Net;
using System.Net.Sockets;
using System.Text;

void Log(string msg)
{
    Console.WriteLine($"LOG: {DateTime.Now} --- {msg}");
}

Socket handler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 34536);

handler.Connect(ipEndPoint);

Log($"CONNECTED TO SERVER {handler.RemoteEndPoint}");

Console.Write("Введите сообщение для сервера: ");
string messageToServer = Console.ReadLine();

byte[] outputBytes = Encoding.Unicode.GetBytes(messageToServer);
handler.Send(outputBytes);

Log($"MESSAGE TO SERVER SENT: {messageToServer}");

StringBuilder messageBuilder = new StringBuilder();
do
{
    byte[] inputBytes = new byte[1024];
    int countBytes = handler.Receive(inputBytes);
    messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
} while (handler.Available > 0);

string messageFromServer = messageBuilder.ToString();

Log($"MESSAGE FROM SERVER RECIEVED: {messageFromServer}");

handler.Shutdown(SocketShutdown.Both);
handler.Close();

Log($"CLIENT CLOSED");
