using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Net.WebSockets;

namespace Lesson1.ServerUDP;

internal class Program
{
    static void Main(string[] args)
    {
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            Console.WriteLine("Connecting...");

            socket.Bind(localEndPoint);

            byte[] buffer = new byte[255];

            int count = 0;

            while (count < 200)
            {
                EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

                var sf = new SocketFlags();

                int c = socket.ReceiveMessageFrom(buffer, 0, 1, ref sf, ref endPoint, out IPPacketInformation info);

                if (c == 1 && (endPoint as IPEndPoint)?.Port == 2233)
                {
                    var buffOut = new byte[1] { (byte)(buffer[0] * 2) };
                    socket.SendTo(buffOut, endPoint);
                }

                count += c;
            }

            Console.WriteLine("\nRead 200 bytes");
        }
    }
}
