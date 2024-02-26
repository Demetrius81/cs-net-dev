using System.Net;
using System.Net.Sockets;

namespace Lesson1.ClientUdp;

internal class Program
{
    static void Main(string[] args)
    {
        Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Socket socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        var localEP1 = new IPEndPoint(IPAddress.Any, 2234);
        var localEP2 = new IPEndPoint(IPAddress.Any, 2233);

        socket1.Bind(localEP1);
        socket2.Bind(localEP2);

        socket1.Connect("127.0.0.1", 1234);
        socket1.Connect("127.0.0.1", 1234);

        new Thread(() => Send(new byte[] {3}, socket1)).Start();
        new Thread(() => Send(new byte[] {4}, socket2)).Start();
    }

    static void Send(byte[] buffer, Socket socket)
    {
        for (int i = 0; i < 100; i++)
        {
            socket.Send(buffer);
            var buffIn = new byte[1];

            int count = socket.Receive(buffIn);
            if (count == 1)
            {
                Console.WriteLine(buffIn[0]);
            }
        }

        socket.Close();
    }
}
