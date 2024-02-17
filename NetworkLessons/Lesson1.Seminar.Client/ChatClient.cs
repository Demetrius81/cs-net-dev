using System.Net.Sockets;

namespace Lesson1.Seminar.Client;

class ChatClient
{
    /// <summary>Run client</summary>
    public async Task RunAsync(string[] args)
    {
#if DEBUG
        string host = "127.0.0.1";
#else
        string host = args[0];
#endif
        int port = 5000;

        using TcpClient client = new();
        Console.Write("Enter your name> ");
        string? userName = Console.ReadLine();
        Console.WriteLine($"Welcome to supersectet chat, {userName}");
        StreamReader? Reader = null;
        StreamWriter? Writer = null;

        try
        {
            client.Connect(host, port);
            Reader = new StreamReader(client.GetStream());
            Writer = new StreamWriter(client.GetStream());

            if (Writer is null || Reader is null)
            {
                return;
            }

            _ = Task.Run(() => ReceiveMessageAsync(Reader));
            await SendMessageAsync(Writer, userName ?? "");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
        }
        Writer?.Close();
        Reader?.Close();
    }

    /// <summary>Send messages</summary>
    async Task SendMessageAsync(StreamWriter writer, string userName)
    {
        await writer.WriteLineAsync(userName);
        await writer.FlushAsync();
        await Console.Out.WriteLineAsync("For send message enter message and push <Enter>:");

        while (true)
        {
            string? message = Console.ReadLine();
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }
    }

    /// <summary>Recive messages</summary>
    async Task ReceiveMessageAsync(StreamReader reader)
    {
        while (true)
        {
            try
            {
                string? message = await reader.ReadLineAsync();

                if (string.IsNullOrEmpty(message))
                {
                    continue;
                }

                await Console.Out.WriteLineAsync(message);
            }
            catch
            {
                break;
            }
        }
    }
}
