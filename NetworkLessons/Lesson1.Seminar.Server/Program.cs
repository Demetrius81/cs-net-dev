using System.Net.Http;

namespace Lesson1.Seminar.Server;

internal class Program
{
    protected Program() { }

    static void Main(string[] args)
    {
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        ChatServer server = ChatServer.Instance;
        _ = Task.Run(() => server.RunAsync(args, token));

        Console.ReadKey();
        cts.Cancel();
    }
}
