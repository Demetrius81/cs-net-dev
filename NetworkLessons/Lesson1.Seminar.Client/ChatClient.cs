using System.Net.Sockets;

namespace Lesson1.Seminar.Client;

public class ChatClient
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
#if DEBUG
        string? userName = Guid.NewGuid().GetHashCode().ToString();
#else
        Console.Write("Enter your name> ");
        string? userName = Console.ReadLine();
#endif
        Console.WriteLine($"Welcome to supersectet chat, {userName}");
        StreamReader? reader = null;
        StreamWriter? writer = null;

        try
        {
            client.Connect(host, port);
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());

            _ = Task.Run(() => ReceiveMessageAsync(reader));
            await SendMessageAsync(writer, userName ?? "");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
        }

        writer?.Close();
        reader?.Close();
    }

    /// <summary>Send messages</summary>
    public async Task SendMessageAsync(StreamWriter? writer, string userName)
    {
        if (writer is null)
        {
            return;
        }

        await writer.WriteLineAsync(userName);
        await writer.FlushAsync();
        await Console.Out.WriteLineAsync("For send message enter message and push <Enter>:");

#if DEBUG
        bool flag = true;
#endif

        while (true)
        {
#if DEBUG
            string? message = null;
            if (flag)
            {
                message = "Hello! this is a test! One, two, three. Check the connect";
                flag = false;
            }
            else
            {
                message = Console.ReadLine();
            }

#else
            string? message = Console.ReadLine();
#endif
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();

            if (message == "Exit")
            {
                return;
            }
        }
    }

    /// <summary>Recive messages</summary>
    public async Task ReceiveMessageAsync(StreamReader? reader)
    {
        if (reader is null)
        {
            return;
        }

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
