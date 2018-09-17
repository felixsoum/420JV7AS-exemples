using System;
using System.Collections.Generic;
using System.Linq;

namespace Cours03LINQ
{
    class Program
    {
        const int David = 7;
        static void Main(string[] args)
        {
            var goblins = new List<Goblin>()
            {
                new Goblin("Dragon", 11, "David"),
                new Goblin("Orc", 20, "Adrien"),
                new Goblin("Hob", 30, "Mark"),
                new Goblin("Dragon", -1, "Daniel"),
                new Goblin("Orc", 15, "Samuel"),
                new Goblin("Hob", 30, "Colin")
            };

            var query = from goblin in goblins
                        select new { goblin.Name, goblin.Hp };

            foreach (var x in query)
            {
                Console.WriteLine(x);
            }
        }
    }
}
