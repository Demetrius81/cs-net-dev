using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4.Coffee;
public class Americano : ICoffee
{
    public void GrindCoffee()
    {
        Console.WriteLine("Перемалываем зерна американо");
    }

    public void IntoCup()
    {
        Console.WriteLine("Завариваем зерна американо");

    }

    public void MakeCoffee()
    {
        Console.WriteLine("Наливаем в чашку американо");
    }
}
