﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MonsterArena
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Monster> monsters = new List<Monster>()
            {
                new SpanishInquisition("Nobody expects"),
                new AtlasWorldLifter("Victor"),
                new Daniel("David"),
                new GiantSlug("Babygirl"),
                new HeroForFun("Saitama"),
                new Leprauchaun("Echo"),
                new Nana.Spirit("Brad"),
                new Palico("Mittens"),
                new PereFwetar("Samuel"),
                new TheLegend27("Colin"),
                new XxdragonBoss69xx("Adrien"),
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
                    int attackIndex = activeMonster.GetAttackIndex(monsterData);

                    if (attackIndex < 0 || attackIndex >= monsters.Count)
                    {
                        activeMonster.Attack(activeMonster);
                    }
                    else
                    {
                        activeMonster.Attack(monsters[attackIndex]);
                    }
                }
            }

            Monster winner = monsters.Find(m => !m.IsDead());
            if (winner != null)
            {
                Console.WriteLine($"The winner is {winner}!");
            }
            else
            {
                Console.WriteLine("All combatants have perished...");
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
