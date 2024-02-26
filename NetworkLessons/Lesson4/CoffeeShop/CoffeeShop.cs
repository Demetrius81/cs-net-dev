using Lesson4.Coffee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4.CoffeeShop;
public class CoffeeShop
{
    private readonly CoffeeFactory _factory;
    public CoffeeShop(CoffeeFactory factory)
    {
        this._factory = factory;
    }
    public ICoffee OrderCoffee(CoffeeType coffeeType)
    {
        ICoffee coffee = this._factory.Create(coffeeType);

        coffee.GrindCoffee();
        coffee.MakeCoffee();
        coffee.IntoCup();

        return coffee;
    }
}
