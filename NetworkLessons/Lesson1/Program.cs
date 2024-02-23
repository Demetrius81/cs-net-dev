using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Lesson1;

internal class Program
{
    static void Main(string[] args)
    {
        PingTest();

        HTTPProtocolTest();
    }

    public static void PingTest()
    {
        var ping = new Ping();

        for (int i = 0; i <= 10; i++)
        {
            PingReply reply = ping.Send("ya.ru", 1000);
            Console.WriteLine($"Ping replied in {reply?.RoundtripTime}");
        }
    }

    public static void HTTPProtocolTest()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://ya.ru");

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine(stream.ReadToEnd());
            }
        }
    }
}
