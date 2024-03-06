namespace Lesson1.Seminar.Client;

public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        ChatClient chatClient = new ChatClient();
        await chatClient.RunAsync(args);
    }
}
