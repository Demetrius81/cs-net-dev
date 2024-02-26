using System.Net;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Lesson1.UDPClientAsync;

internal class Program
{
    private static readonly IPEndPoint _ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000);
    static void Main(string[] args)
    {
        //new Thread(UdpReciver).Start();
        //for (int i = 0; i < 3; i++)
        //{
        //    byte temp = (byte)(i + 1);
        //    new Thread(() => UdpSender(temp)).Start();

        //}
        new Thread(Reciver).Start();
        new Thread(Sender).Start();


        Console.ReadKey(true);
    }

    public static void UdpReciver()
    {
        using (var client = new UdpClient(6000))
        {
            var lp = new IPEndPoint(IPAddress.Any, 6000);
            int cnt = 0;
            while (cnt < 9_000)
            {
                var recv = client.Receive(ref lp);

                foreach (var item in recv)
                {
                    Console.Write($"{item} ");
                }
                cnt += recv.Length;
            }

            Console.WriteLine("\nEnd.");
        }
    }

    public static void UdpSender(byte b)
    {
        using (var client = new UdpClient())
        {
            client.Connect(_ep);
            for (int i = 0; i < 3_000; i++)
            {
                client.Send(new byte[] { b });
            }
        }
    }

    public static void Reciver()
    {
        using (var client = new UdpClient(6000))
        {
            var lp = new IPEndPoint(IPAddress.Any, 6000);
            int lastPacketNum = -1;
            while (lastPacketNum<255)
            {
                var recv = client.Receive(ref lp);

                var currPacketNum = recv[0];

                if(currPacketNum > lastPacketNum)
                {
                    lastPacketNum = currPacketNum;
                    var bs = new byte[recv.Length - 1];
                    Array.Copy(recv, 1, bs, 0, recv.Length - 1);
                    var str = Encoding.ASCII.GetString(bs);
                    Console.Write($"{str} ");
                }
                else
                {
                    Console.Write("--- ");
                }
            }

            Console.WriteLine("\nEnd.");
        }
    }

    public static void Sender()
    {
        using (var client = new UdpClient())
        {
            client.Connect(_ep);
            for (int i = 0; i < 256; i++)
            {
                var data = $"Line ${i}";
                var bData = Encoding.ASCII.GetBytes(data);
                var packet = new byte[bData.Length + 1];
                packet[0] = (byte)i;
                Array.Copy(bData, 0, packet, 1, bData.Length);
                client.Send(packet);
            }
        }
    }
}
