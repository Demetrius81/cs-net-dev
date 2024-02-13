using System.Net;
using System.Net.Sockets;

namespace Lesson1.Seminar.Client;

public class ChatClient
{
    private readonly IPEndPoint _remoteEndPoint;

    public ChatClient()
    {
        _remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
    }
    public async Task Run(string[] args)
    {
        using (TcpClient client = new())
        {
            string? msg;

            client.Connect(this._remoteEndPoint);

            while (true)
            {
                var reader = new StreamReader(client.GetStream());
                var writer = new StreamWriter(client.GetStream());

                msg = InputMessage();

                await writer.WriteLineAsync(msg);
                await writer.FlushAsync();

                msg = await reader.ReadLineAsync();

                if (msg is not null)
                {
                    await Console.Out.WriteLineAsync(msg);
                }
                msg = reader.ReadLine();
                if (msg is not null)
                {
                    await Console.Out.WriteLineAsync(msg);
                }
            }
        }





        //TcpClient tcpClient = new TcpClient();
        //Thread.Sleep(1000);
        //tcpClient.Connect("127.0.0.1", 5000);
        //Console.ReadKey();
    }

    private string InputMessage()
    {
        Console.Write(">");
        var str = Console.ReadLine();
        return str ?? "";
    }
}
