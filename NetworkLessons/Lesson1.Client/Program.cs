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

            //client.ReceiveTimeout = 0;

#pragma warning disable S2486 // Generic exceptions should not be ignored
            try
            {
                client.Connect(remoteEndPoint);
            }
            catch
#pragma warning disable S108 // Nested blocks of code should not be left empty
            {
            
            }
#pragma warning restore S108 // Nested blocks of code should not be left empty
#pragma warning restore S2486 // Generic exceptions should not be ignored

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

            Console.WriteLine("Push any key to send");
            Console.ReadKey(true);

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
