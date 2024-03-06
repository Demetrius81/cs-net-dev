using Lesson1.Seminar.Server.Context;
using Lesson1.Seminar.Server.Models;
using Lesson1.Seminar.Server.Models.DTO;
using System.Net.Sockets;
using System.Xml.Linq;

namespace Lesson1.Seminar.Server;

public sealed class ClientInfo : IDisposable
{
    public string Id { get; set; }
    public StreamWriter Writer { get; init; }
    public StreamReader Reader { get; init; }

    private readonly TcpClient _client;
    private readonly ChatServer _server;
    private bool disposedValue;

    public ClientInfo(TcpClient tcpClient, ChatServer serverObject)
    {
        //this.Id = Guid.NewGuid().ToString();
        this._client = tcpClient;
        this._server = serverObject;
        var stream = this._client.GetStream();
        this.Reader = new StreamReader(stream);
        this.Writer = new StreamWriter(stream);
    }

    /// <summary>Run process for each ckient</summary>
    public async Task ClientProcessAsync()
    {
        try
        {
            //string? userName = await this.Reader.ReadLineAsync();
            //string? json = $"{userName ?? ""} Connected to chat";
            //await this._server.BroadcastMessageAsync(json ?? "", this.Id);
            //Console.WriteLine(json ?? "");
            ////--------------------
            //_user = new() { Id = Id, Username = userName, RecivedMessage = new List<Message>(), SendedMessage = new List<Message>() };
            ////--------------------
            string? json = null;

            while (true)
            {
                try
                {
                    json = await this.Reader.ReadLineAsync();

                    if (json == null)
                    {
                        continue;
                    }

                    var msg = MessageDto.Deerialize(json);

                    if (msg is null)
                    {
                        throw new NullReferenceException("Wrong json");
                    }

                    switch (msg.Status)
                    {
                        case Command.Registred:
                            RegisterClient(msg.SenderName);
                            break;
                        case Command.Confirmed:
                            Confirmed(msg.Id);
                            break;
                        case Command.Message:
                            //Get msg, save to db and send to client
                            AddMessageToBd(msg);
                            break;
                        default:
                            break;
                    }

                    //if (json == "Exit")
                    //{
                    //    json = $"{userName}: Disconnected.";
                    //    Console.WriteLine(json);
                    //await this._server.BroadcastMessageAsync(json, this.Id);
                    //    this._server.RemoveConnection(this.Id);
                    //}


                    //json = $"{userName}: {json}";
                    //Console.WriteLine(json);
                    //await this._server.BroadcastMessageAsync(json, this.Id);
                }
                catch
                {
                    //json = $"{userName} disconnected.";
                    //Console.WriteLine(json);
                    //await this._server.BroadcastMessageAsync(json, this.Id);
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

    private void AddMessageToBd(MessageDto msg)
    {
        //Get msg, save to db and send to client
        using (var context = new ChatContext())
        {
            var author = context.Users.FirstOrDefault(x => x.Username == msg.SenderName);
            var reciver = context.Users.FirstOrDefault(x => x.Username == msg.ReciverName);
            if (author is null || reciver is null)
            {
                throw new NullReferenceException("DB not contain authot or reciver");
            }
            context.Messages.Add(new Message()
            {
                Id = msg.Id,
                Author = author,
                Consumer = reciver,
                AuthorId = author.Id,
                ConsumerId = reciver.Id,
                content = msg.Text,
                IsRecived = false
            });
            this._server.SendMessage(msg, reciver.Id);

        }
    }

    private void Confirmed(string id)
    {
        using (var context = new ChatContext())
        {
            var msg = context.Messages.FirstOrDefault(x => x.Id == id);

            if (msg is not null)
            {
                msg.IsRecived = true;
                context.SaveChanges();
            }
        }
    }

    private void RegisterClient(string? name)
    {
        this.Id = Guid.NewGuid().ToString();
        using (var context = new ChatContext())
        {
            context.Users.Add(new User() { Username = name, Id = this.Id });
        }
    }

    /// <summary>Close connection</summary>
    private void Close()
    {
        this.Writer.Close();
        this.Reader.Close();
        this._client.Close();
        Console.WriteLine($"Client {this.Id} disconnected.");
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Close();
            }

            Close();
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}