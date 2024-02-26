using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4.Coffee;
public class Latte : ICoffee
{
    public void GrindCoffee()
    {
        Console.WriteLine("Перемалываем зерна латте");
    }

    public void IntoCup()
    {
        Console.WriteLine("Завариваем зерна латте");

    }

    public void MakeCoffee()
    {
        Console.WriteLine("Наливаем в чашку латте");
    }
}
