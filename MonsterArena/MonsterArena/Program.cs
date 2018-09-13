using System;
using System.Collections.Generic;
using System.Linq;

namespace MonsterArena
{
    class Program
    {
        static SortedDictionary<string, int> leaderboard = new SortedDictionary<string, int>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 10000; i++)
            {
                List<Monster> monsters = new List<Monster>()
                {
                new SpanishInquisition("Nobody expects"),
                new AtlasWorldLifter("Victor"),
                new Daniel("David"),
                new GiantSlug("Babygirl"),
                new HeroForFun("Saitama"),
                new Leprauchaun("Echo"),
                //new Nana.Lich("Brad"),
                new Palico("Mittens"),
                new PereFwetar("Samuel"),
                new TheLegend27("Colin"),
                new InhumanRat("Theo"),
                new Compte_Harebourg("Adrien"),
                new GoblinRoberto("Roberto")
                };

                foreach (var monster in monsters)
                {
                    monster.Spawn();
                }



                while (!IsBattleOver(monsters))
                {
                    monsters.Sort();
                    foreach (var activeMonster in monsters)
                    {
                        if (activeMonster.IsDead())
                            continue;

                        var monsterData = CreateMonsterData(monsters);
                        Console.WriteLine($"{activeMonster} planning his attack.");
                        int attackIndex = activeMonster.GetAttackIndex(monsterData);

                        if (attackIndex < 0 || attackIndex >= monsters.Count)
                        {
                            activeMonster.Attack(activeMonster);
                        }
                        else
                        {
                            activeMonster.Attack(monsters[attackIndex]);
                        }

                        if (IsBattleOver(monsters))
                        {
                            break;
                        }
                    }
                }

                Monster winner = monsters.Find(m => !m.IsDead());
                if (winner != null)
                {
                    Console.WriteLine($"The winner is {winner}!");
                    string typeName = winner.GetType().Name;
                    if (leaderboard.ContainsKey(typeName))
                    {
                        leaderboard[typeName]++;
                    }
                    else
                    {
                        leaderboard.Add(typeName, 1);
                    }
                }
                else
                {
                    Console.WriteLine("All combatants have perished...");
                }
            }

            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"{entry.Key} won {entry.Value} times.");
            }
        }

        static bool IsBattleOver(List<Monster> monsters)
        {
            return monsters.FindAll(m => !m.IsDead()).Count <= 1;
        }

        static List<MonsterData> CreateMonsterData(List<Monster> monsters)
        {
            return monsters.Select(m => m.GetData()).ToList();
        }
    }
}
