using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2;
internal class ExampleThreads
{
    static LocalDataStoreSlot LocalDataStoreSlot = Thread.AllocateDataSlot();

    #region example 1

    public void RunInit()
    {
        for (int i = 0; i < 10; i++)
        {
            //Thread t = new Thread(PrintThread);
            //Thread t = new Thread(new ThreadStart(PrintThread));
            Thread t = new Thread(() =>
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - one operation");
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - two operation");
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - three operation");
                }
            });
            t.Start();
        }
    }

    private void PrintThread()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - one operation");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - two operation");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - three operation");
        }
    }

    #endregion

    public void Run()
    {
    }

    private void TreadProc()
    {
        for (int i = 0; i < 10; i++)
        {
            var data = ((int?)Thread.GetData(LocalDataStoreSlot)) ?? 0;

            Thread.SetData(LocalDataStoreSlot, data + i);
        }
    }
}
