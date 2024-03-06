using Lesson4.Coffee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4.CoffeeShop;
public class CoffeeFactory
{
    public ICoffee Create(CoffeeType coffeeType)
    {
        ICoffee coffee = null!;

        switch (coffeeType)
        {
            case CoffeeType.Espresso:
                coffee = new Espresso();
                break;
            case CoffeeType.Americano:
                coffee = new Americano();
                break;
            case CoffeeType.Latte:
                coffee = new Latte();
                break;
        }

        return coffee;
    }
}
