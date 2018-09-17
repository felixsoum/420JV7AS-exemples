using System;

namespace GoblinTribe
{
    class Program
    {
        static void Main(string[] args)
        {
            var hobGoblin = new Goblin();
            Console.WriteLine(hobGoblin);

            var dragon = new Dragon();
            Console.WriteLine(dragon);

            var fireGoblin = new Goblin();
            Console.WriteLine(fireGoblin);

            Console.WriteLine($"Goblin HP: {hobGoblin.GetHp()}");
            dragon.Attack(hobGoblin);
            Console.WriteLine($"Goblin HP: {hobGoblin.GetHp()}");
        }
    }
}
