using System.Net.Sockets;
using System.Net;

namespace Lesson1.Seminar.Server;

public sealed class ChatServer : IDisposable
{
    private readonly TcpListener _listener;
    private readonly List<ClientInfo> _clients;
    private static readonly Lazy<ChatServer> _instance = new(() => new ChatServer());
    private bool disposedValue;

    public static ChatServer Instance => _instance.Value;

    private ChatServer()
    {
        this._listener = new TcpListener(IPAddress.Any, 5000);
        this._clients = new List<ClientInfo>();
    }

    /// <summary>Removing connection</summary>
    internal void RemoveConnection(string id)
    {
        ClientInfo? client = this._clients.Find(x => x.Id == id);

        if (client is not null)
        {
            this._clients.Remove(client);
        }

        client?.Dispose();
    }

    /// <summary>Run server and listen the connect</summary>
    internal async Task RunAsync(string[] args, CancellationToken cancel)
    {
        try
        {
            this._listener.Start();
            await Console.Out.WriteLineAsync("Server is started, wait for connections...");

            while (!cancel.IsCancellationRequested)
            {
                TcpClient tcpClient = await this._listener.AcceptTcpClientAsync();

                ClientInfo clientInfo = new(tcpClient, this);
                await Console.Out.WriteLineAsync($"Client {clientInfo.Id} connected.");
                this._clients.Add(clientInfo);
                _ = Task.Run(clientInfo.ClientProcessAsync);
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
        }
        finally
        {
            Dispose();
        }
    }



    /// <summary>Send messages to all clients</summary>
    internal async Task BroadcastMessageAsync(string message, string id)
    {
        foreach (var client in this._clients)
        {
            if (client.Id != id)
            {
                await client.Writer.WriteLineAsync(message);
                await client.Writer.FlushAsync();
            }
        }
    }

    /// <summary>Disconnect all clients and stop server</summary>
    private void Disconnect()
    {
        foreach (var client in this._clients)
        {
            client.Dispose();
        }
        this._listener.Stop();
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Disconnect();
            }

            Disconnect();
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
