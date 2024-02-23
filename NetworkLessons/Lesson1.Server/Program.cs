using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lesson1.Server;

internal class Program
{
    static void Main(string[] args)
    {
        var listener = new TcpListener(IPAddress.Any, 5000);

        listener.Start();

        using (TcpClient client = listener.AcceptTcpClient())
        {
            Console.WriteLine("Connected!");

            Console.WriteLine($"LocalEndPoint = {client.Client.LocalEndPoint}");
            Console.WriteLine($"RemoteEndPoint = {client.Client.RemoteEndPoint}");

            var reader = new StreamReader(client.GetStream());
            var writer = new StreamWriter(client.GetStream());

            var s = reader.ReadLine();
            Console.WriteLine($"Recived: <{s}>");
            var r = new String(s.Reverse().ToArray());
            Console.WriteLine($"Send: <{r}>");
            writer.WriteLine(r);
            writer.Flush();
        }

    }
}
