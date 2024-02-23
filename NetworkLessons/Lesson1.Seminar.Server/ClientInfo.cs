using System.Net.Sockets;

namespace Lesson1.Seminar.Server;

internal sealed class ClientInfo
{
    internal string Id { get; init; }
    internal StreamWriter Writer { get; init; }
    internal StreamReader Reader { get; init; }

    private readonly TcpClient _client;
    private readonly ChatServer _server;

    internal ClientInfo(TcpClient tcpClient, ChatServer serverObject)
    {
        this.Id = Guid.NewGuid().ToString();
        this._client = tcpClient;
        this._server = serverObject;
        var stream = this._client.GetStream();
        this.Reader = new StreamReader(stream);
        this.Writer = new StreamWriter(stream);
    }

    /// <summary>Run process for each ckient</summary>
    internal async Task ClientProcessAsync()
    {
        try
        {
            string? userName = await this.Reader.ReadLineAsync();
            string? message = $"{userName ?? ""} Connected to chat";
            await this._server.BroadcastMessageAsync(message ?? "", this.Id);
            Console.WriteLine(message ?? "");

            while (true)
            {
                try
                {
                    message = await this.Reader.ReadLineAsync();

                    if (message == null)
                    {
                        continue;
                    }

                    if (message == "Exit")
                    {
                        message = $"{userName}: Disconnected.";
                        Console.WriteLine(message);
                        await this._server.BroadcastMessageAsync(message, this.Id);
                        this._server.RemoveConnection(this.Id);
                    }

                    message = $"{userName}: {message}";
                    Console.WriteLine(message);
                    await this._server.BroadcastMessageAsync(message, this.Id);
                }
                catch
                {
                    message = $"{userName} disconnected.";
                    Console.WriteLine(message);
                    await this._server.BroadcastMessageAsync(message, this.Id);
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            this._server.RemoveConnection(Id);
        }
    }

    /// <summary>Close connection</summary>
    internal void Close()
    {
        this.Writer.Close();
        this.Reader.Close();
        this._client.Close();
    }
}