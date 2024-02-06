using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lesson1.Server;

internal class Program
{
    static void Main(string[] args)
    {
        using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            listener.Blocking = true;

            listener.Bind(localEndPoint);

            listener.Listen(100);

            Console.WriteLine("Waiting for connection...");

            Socket? socket = null;

            while (true)
            {
                List<Socket> sread = new List<Socket> { listener };
                List<Socket> swrite = new List<Socket> { listener };
                List<Socket> serror = new List<Socket> { listener };

                Socket.Select(sread, swrite, serror, 100);

                if (sread.Count > 0)
                {
                    break;
                }
                else
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
            }

            socket = listener.Accept();

            Console.WriteLine("Connected!");

            Console.WriteLine($"LocalEndPoint = {socket.LocalEndPoint}");
            Console.WriteLine($"RemoteEndPoint = {socket.RemoteEndPoint}");

            byte[] buffer = new byte[255];

            while (socket.Available == null) ;

            int count = socket.Receive(buffer);

            if (count > 0)
            {
                string message = Encoding.UTF8.GetString(buffer);

                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Message not recived.");
            }


            listener.Close();
        }
    }
}
