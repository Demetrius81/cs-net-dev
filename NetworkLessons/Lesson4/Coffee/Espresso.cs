using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4.Coffee;
public class Espresso : ICoffee
{
    public void GrindCoffee()
    {
        Console.WriteLine("Перемалываем зерна эспрессо");
    }

    public void IntoCup()
    {
        Console.WriteLine("Завариваем зерна эспрессо");

    }

    public void MakeCoffee()
    {
        Console.WriteLine("Наливаем в чашку эспрессо");
    }
}
