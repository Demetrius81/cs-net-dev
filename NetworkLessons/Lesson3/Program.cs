namespace Lesson3;

internal class Program
{
    public static void Method()
    {
        for (int i = 0; i < 80; i++)
        {
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("-");
            throw new Exception();
        }
    }

    //помечаем ключевым словом async метод
    public async static Task MethodAsync()
    {

        //Эта часть кода будет работать в основном потоке
        //создаем задачу передаем в делегат метод Method
        Task t = new Task(Method);
        //запускаем задачу
        t.Start();


        //а вот эта часть завершится в 2 потоке
        //ожидаем завершения задачи
        await t;
    }

    public async static Task<int> Method2()
    {
        throw new Exception();
    }


    static void Main(string[] args)
    {
        //var t = Method2();

        //Console.WriteLine("Main завершился");
        //Console.ReadKey();
        //var res = t.Result;
        //Console.WriteLine(res);

        //int x = 0;
        //object locker = new(); // объект-заглушка
        //                       // запускаем пять потоков
        //for (int i = 1; i < 6; i++)
        //{
        //    Thread myThread = new(Print);
        //    myThread.Name = $"Поток {i}";
        //    myThread.Start();
        //}

        //async void Print()
        //{
        //    lock (locker)
        //    {
        //        x = 1;
        //        for (int i = 1; i < 6; i++)
        //        {
        //            await Method2();
        //            Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
        //            x++;
        //            Thread.Sleep(100);
        //        }
        //    }
        //}
    }
}
