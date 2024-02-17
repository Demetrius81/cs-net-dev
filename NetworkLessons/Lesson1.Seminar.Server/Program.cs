using System.Net.Http;

namespace Lesson1.Seminar.Server;

internal class Program
{
    protected Program() { }

    static async Task Main(string[] args)
    {
        ChatServer server = new ChatServer();
        await server.RunAsync(args);
    }
}
