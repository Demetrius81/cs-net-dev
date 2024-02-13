namespace Lesson1.Seminar.Client;

internal class Program
{
    static void Main(string[] args)
    {
        ChatClient chatClient = new ChatClient();
        chatClient.Run(args);
    }
}
