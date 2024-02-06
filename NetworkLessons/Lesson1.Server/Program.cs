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
            //listener.Blocking = false;

            listener.Bind(localEndPoint);

            listener.Listen(100);

            Console.WriteLine("Waiting for connection...");

            Socket? socket = null;

            do
            {
                try
                {
                    socket = listener.Accept();
                }
                catch
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
            }
            while (socket == null);

            Console.WriteLine("Connected!");

            Console.WriteLine($"LocalEndPoint = {socket.LocalEndPoint}");
            Console.WriteLine($"RemoteEndPoint = {socket.RemoteEndPoint}");

            byte[] buffer = new byte[255];

            //while (socket.Available == 0) ;

            //Console.WriteLine($"Avalible {socket.Available} bytes for reading.");

            socket.ReceiveTimeout = 2000;

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
