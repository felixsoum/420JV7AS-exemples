using System;
using System.Collections.Generic;
using System.Linq;

namespace MonsterArena
{
    class Program
    {

        //Amount of possibilities for each stat (0-100) 0 included = 101 poss.
        public const int POSSIBILITIES_PER_STAT = 101;
        //Amount of stats -> hp, strength, luck, dext
        public const int AMOUNT_OF_STATS = 4;
        //Amount of time executing the tests to check which stats are best
        public const int AMOUNT_OF_TEST_TO_RUN = 1;
        //Amount of combats to do for each set of stats
        public const int AMOUNT_OF_COMBATS = 100;

        static void Main(string[] args)
        {
            //ExecuteCombat();

            CalculateBestSetOfStats(AMOUNT_OF_STATS, POSSIBILITIES_PER_STAT, AMOUNT_OF_COMBATS, AMOUNT_OF_TEST_TO_RUN);
        }

        static Monster ExecuteCombat()
        {
            List<Monster> monsters = new List<Monster>()
            {
                new Goblin("Alice"),
                new Orc("Bob"),
                new Goblin("Charlie"),
                new Orc("David"),
                new AtlasWorldLifter("Victor"),
                new Daniel("Daniel"),
                new HeroForFun("Hero"),
                new Palico("Palico"),
                new TheLegend27("The legend 27"),
                new XxdragonBoss69xx("Dragon"),
                new PereFwetar("Samuel"),
                new Leprauchaun("Echo"),
                new HeroForFun("Saitama"),
                new GiantSlug("Babygirl"),
                new Furry("Trap")
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
                        continue;

                    activeMonster.Attack(monsters[attackIndex]);
                }
            }

            Monster winner = monsters.Find(m => !m.IsDead());
            if (winner != null)
            {
                Console.WriteLine($"The winner is {winner}!");
                return winner;
            }
            else
            {
                Console.WriteLine("All combatants have perished...");
                return null;
            }
        }

        static Monster ExecuteCombatWithSpecificStats(int pVitality, int pStrength, int pDext, int pLuck)
        {
            List<Monster> monsters = new List<Monster>()
            {
                new Goblin("Alice"),
                new Orc("Bob"),
                new Goblin("Charlie"),
                new Orc("David"),
                new AtlasWorldLifter("Victor"),
                new Daniel("Daniel"),
                new HeroForFun("Hero"),
                new Palico("Palico"),
                new TheLegend27("The legend 27"),
                new XxdragonBoss69xx("Dragon"),
                new PereFwetar("Samuel", pVitality, pStrength, pDext, pLuck),
                new Leprauchaun("Echo"),
                new HeroForFun("Saitama"),
                new GiantSlug("Babygirl"),
                new Furry("Trap")
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
                        continue;

                    activeMonster.Attack(monsters[attackIndex]);
                }
            }

            Monster winner = monsters.Find(m => !m.IsDead());
            if (winner != null)
            {
                Console.WriteLine($"The winner is {winner}!");
                return winner;
            }
            else
            {
                Console.WriteLine("All combatants have perished...");
                return null;
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
        
        //Will output a list with all the combinations possible for the sets of stats
        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
                return list.Select(t => new T[] { t });
            return GetCombinations(list, length - 1).SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        //Calculate the best set of stats, example : 0 hp, 50, str, 50 dext, 0 luck, etc.
        static void CalculateBestSetOfStats (int pAmountOfStats, int pPossibilitiesPerStats, int pAmountOfCombatsPerSet, int pAmountOfTest)
        {
            BestStatCalculator calculator = new BestStatCalculator();

            //Specific set of stats to test
            int hp = 0;
            int str = 0;
            int dext = 0;
            int luck = 0;

            //pointer for the current set of stats were currently at
            int sets_pointer = 0;

            //Creates all the possibilities for the sets of stats
            IEnumerable<IEnumerable<int>> combinations =
                GetCombinations(Enumerable.Range(1, (pPossibilitiesPerStats - 1)), pAmountOfStats);

            //Transforms the combinations into a list so we can access it easier like an array
            List<List<int>> possible_sets_of_stats = new List<List<int>>();
            foreach (IEnumerable<int> entry in combinations)
            {
                int sum = 0;
                List<int> lst = new List<int>(entry.ToList());
                foreach (int num in lst)
                {
                    sum += num;
                }
                if(sum == 100)
                {
                    possible_sets_of_stats.Add(entry.ToList());
                }
            }

            //Apply the first set of stats to our variables
            hp = possible_sets_of_stats[0][0];
            str = possible_sets_of_stats[0][1];
            dext = possible_sets_of_stats[0][2];
            luck = possible_sets_of_stats[0][3];

            //Amount of wins with that specific set of stats
            int wins = 0;

            //Loop for how many times executing the tests
            for (int h = 0; h < pAmountOfTest; h++)
            {
                //Loop for how many possibilities there are for each stat (0-100)
                for (int j = 0; j < possible_sets_of_stats.Count; j++)
                {
                    //Loop for how many combats to do with each set of stats
                    for (int index = 0; index < pAmountOfCombatsPerSet; index++)
                    {
                        //Execute a combat
                        Monster winner = ExecuteCombatWithSpecificStats(hp, str, dext, luck);
                        if (winner != null && winner.GetType().Name == "PereFwetar")
                            wins++;
                    }
                    //Keep in memory the winner
                    calculator.Results.Add(new PereFwetar("Samuel", hp, str, dext, luck), wins);
                    wins = 0;

                    //Change the set of stats to the next one
                    if (++sets_pointer < possible_sets_of_stats.Count)
                    {
                        hp = possible_sets_of_stats[sets_pointer][0];
                        str = possible_sets_of_stats[sets_pointer][1];
                        dext = possible_sets_of_stats[sets_pointer][2];
                        luck = possible_sets_of_stats[sets_pointer][3];
                    } 
                    else
                    {
                        calculator.WriteResults();
                        return;
                    }

                }
            }

            calculator.WriteResults();
        }

    }
}
