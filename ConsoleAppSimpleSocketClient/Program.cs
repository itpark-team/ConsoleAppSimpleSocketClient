using ConsoleAppSimpleSocketClient;

ClientEngine clientEngine = new ClientEngine("127.0.0.1", 34536);
clientEngine.ConnectToServer();

Console.Write("Введите сообщение для сервера: ");
string messageToServer = Console.ReadLine();

clientEngine.SendMessage(messageToServer);
string messageFromServer = clientEngine.ReceiveMessage();

clientEngine.CloseClientSocket();