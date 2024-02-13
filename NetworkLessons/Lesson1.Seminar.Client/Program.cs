using System.Net.Sockets;

namespace Lesson1.Seminar.Client;

internal class Program
{
    static void Main(string[] args)
    {
        TcpClient tcpClient = new TcpClient();
        Thread.Sleep(1000);
        tcpClient.Connect("127.0.0.1", 5000);
        Console.ReadKey();
    }
}
