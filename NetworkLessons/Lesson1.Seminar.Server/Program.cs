using System.Net.Http;

namespace Lesson1.Seminar.Server;

internal class Program
{
    protected Program() { }

    static async Task Main(string[] args)
    {
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        ChatServer server = new();
        _ = Task.Run(() => server.RunAsync(args, token));

        Console.ReadKey();
        cts.Cancel();
    }
}
