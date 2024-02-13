using System.Net.Sockets;
using System.Net;

namespace Lesson1.Seminar.Server;

public sealed class ChatServer
{
    private readonly TcpListener _listener;
    private readonly Dictionary<int, TcpClient> _listClients;
    private int _count;

    public ChatServer()
    {
        this._listener = new TcpListener(IPAddress.Any, 5000);
        this._listClients = new Dictionary<int, TcpClient>();
        this._count = 0;
    }

    public async Task Run(string[] args)
    {
        try
        {
            _listener.Start();
            await Console.Out.WriteLineAsync("Server is started.");

            while (true)
            {
                using (var tcpClient = await this._listener.AcceptTcpClientAsync())
                {
                    this._listClients.Add(++this._count, tcpClient);
                    var client = new ClientInfo(tcpClient, this._count);
                    await Console.Out.WriteLineAsync($"Client No {this._count} IP: {tcpClient.Client.RemoteEndPoint} successfully connected");
                    _ = Task.Run(() => ProcessClient(client));
                }
            }
        }
        catch (SocketException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            _listener?.Stop();
            _listener?.Dispose();
        }
    }

    private async Task ProcessClient(ClientInfo clientInfo)
    {
        var reader = new StreamReader(clientInfo.Client.GetStream());
        var message = await reader.ReadLineAsync();

        if (message != null)
        {
            await BroadcastSending(clientInfo, message);
        }
        else
        {
            Console.WriteLine("Message is null");
        }


    }

    private async Task BroadcastSending(ClientInfo clientInfo, string data)
    {
        foreach (var key in this._listClients.Keys)
        {
            //if (key != clientInfo.Id)
            {
                var writer = new StreamWriter(this._listClients[key].GetStream());
                await writer.WriteLineAsync(data);
            }
        }
    }
}



internal sealed class ClientInfo
{
    public TcpClient Client { get; init; } = null!;
    public int Id { get; init; }

    public ClientInfo(TcpClient client, int id)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client), "Class ClientInfo method ClientInfo. Argument is null");
        }

        this.Client = client;
        this.Id = id;
    }
}
