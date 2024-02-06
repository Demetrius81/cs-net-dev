using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lesson1.Client;

internal class Program
{
    static void Main(string[] args)
    {
        using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5005);

            Console.WriteLine("Connecting...");

            client.Bind(localEndPoint);

            try
            {
                client.Connect(remoteEndPoint);
            }
            catch
            {

            }

            if (client.Connected)
            {
                Console.WriteLine("Connected!");
                Console.WriteLine($"LocalEndPoint = {client.LocalEndPoint}");
                Console.WriteLine($"RemoteEndPoint = {client.RemoteEndPoint}");
            }
            else
            {
                Console.WriteLine("Connection problems...");
                return;
            }

            byte[] buffer = Encoding.UTF8.GetBytes("Hello!");

            if (client.Poll(100, SelectMode.SelectWrite) && client.Poll(100, SelectMode.SelectError))
            {
                int count = client.Send(buffer);

                if (count == buffer.Length)
                {
                    Console.WriteLine("Send.");
                }
                else
                {
                    Console.WriteLine("something wrong...");
                }
            }
        }
    }
}
