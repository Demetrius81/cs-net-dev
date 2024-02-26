using System.Net;
using System.Net.NetworkInformation;

namespace Lesson2;
internal class Task2
{
    public void Run()
    {
        string host = "yandex.ru";
        IPAddress[] iPs = Dns.GetHostAddresses(host, System.Net.Sockets.AddressFamily.InterNetwork);
        Console.WriteLine(iPs.Length);
        Dictionary<long, IPAddress> pairs = [];
        List<Thread> threads = [];

        foreach (var ip in iPs)
        {
            var t = new Thread(() =>
            {
                Ping ping = new Ping();
                var time = ping.Send(ip).RoundtripTime;
                pairs.Add(time, ip);
                Console.WriteLine(time);
            });
            threads.Add(t);
            t.Start();
        }

        threads.ForEach(thread => thread.Join());
        long min = long.MaxValue;
        IPAddress minIP = null!;

        foreach (var item in pairs)
        {
            if (item.Key < min)
            {
                min = item.Key;
                minIP = item.Value;
            }

        }

        Console.WriteLine($"{minIP} : {min}");
    }
}
