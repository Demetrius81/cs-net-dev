using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lesson1.Client;

internal class Program
{
    static void Main(string[] args)
    {
        using (TcpClient client = new())
        {
            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            client.Connect(remoteEndPoint);

            var reader = new StreamReader(client.GetStream());
            var writer = new StreamWriter(client.GetStream());

            writer.WriteLine("Hello, I am Client!");
            writer.Flush();
            var result = reader.ReadLine();
            Console.WriteLine(result);

        }

        Console.ReadKey(true);
    }
}
