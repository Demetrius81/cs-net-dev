namespace Lesson2;

internal class Program
{
    static void Main(string[] args)
    {
        //var example = new ExampleThreads();
        //example.Run();

        var task1 = new Task1();
        task1.Run();

        var task2 = new Task2();
        task2.Run();
    }
}
