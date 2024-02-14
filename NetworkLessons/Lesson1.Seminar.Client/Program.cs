namespace Lesson1.Seminar.Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        ChatClient chatClient = new ChatClient();
        await chatClient.RunAsync(args);
    }
}
