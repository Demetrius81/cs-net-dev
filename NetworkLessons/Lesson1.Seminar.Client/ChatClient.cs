using System.Net.Sockets;

namespace Lesson1.Seminar.Client;

public class ChatClient
{
    public void Run(string[] args)
    {
        TcpClient tcpClient = new TcpClient();
        Thread.Sleep(1000);
        tcpClient.Connect("127.0.0.1", 5000);
        Console.ReadKey();
    }
}
