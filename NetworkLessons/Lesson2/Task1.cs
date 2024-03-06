using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2;
internal class Task1
{
    public void Run()
    {
        int[] results = new int[2];
        var arr1 = Enumerable.Range(0, 1000).ToArray();
        var arr2 = Enumerable.Range(0, 10000).ToArray();

        Thread task1 = new Thread(() => SunElements(arr1, out results[0]));
        Thread task2 = new Thread(() => SunElements(arr2, out results[1]));

        task1.Start();
        task2.Start();

        task1.Join();
        task2.Join();


        SunElements(arr1, out int res);

        Console.WriteLine(res);
    }

    private void SunElements(int[] arr, out int sum)
    {
        sum = 0;
        foreach (var item in arr)
        {
            sum += item;
        }

    }
}
