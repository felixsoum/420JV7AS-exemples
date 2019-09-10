using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonsterArena
{
    class BestStatCalculator  {

        //Amount of possibilities for each stat (0-100)
        public const int POSSIBILITIES_PER_STAT = 101;
        //Amount of stats -> hp, strength, luck, dext
        public const int AMOUNT_OF_STATS = 1;

        //matrix of possibilities
        public int TotalPossibilities { get; private set; }

        //The dictionary that holds all of the results
        public Dictionary<Monster, int> Results { get; set; }

        //Constructor of the calculator
        public BestStatCalculator ()
        {
            Results = new Dictionary<Monster, int>();
        }

        //Calculates the total possibilities for the stats for testing
        //Example : (1 hp, 0 str, 0 luck, 0 dext), (2 hp, 0 str, 0 luck, 0 dext), etc.
        public void CalculateTotalPossibilities()
        {
            TotalPossibilities = (int)Math.Pow(POSSIBILITIES_PER_STAT, AMOUNT_OF_STATS);
        }

        //Write the results into a text file
        public void WriteResults ()
        {
            StreamWriter writer = new StreamWriter("results.txt", false);
            writer.WriteLine("----------------------RESULTS-----------------------" + writer.NewLine);
            Monster bestWRMonster = new PereFwetar("Temporary Monster");
            int maxWins = int.MinValue;
            foreach (KeyValuePair<Monster, int> entry in Results)
            {
                if(entry.Value > maxWins)
                {
                    bestWRMonster = entry.Key;
                    maxWins = entry.Value;
                }
                writer.WriteLine($"Stats: {entry.Key.BaseVitality + entry.Key.BonusVitality} HP, {entry.Key.BaseStrength + entry.Key.BonusStrength} STR, {entry.Key.BaseDexterity + entry.Key.BonusDexterity} DEXT, {entry.Key.BaseLuck + entry.Key.BonusLuck} LUCK");
                writer.WriteLine($"Percentage of winning: {entry.Value} / 100   ->    {entry.Value}% {writer.NewLine}");
            }
            writer.WriteLine($"{writer.NewLine}Best stats: {bestWRMonster.BaseVitality + bestWRMonster.BonusVitality} HP, {bestWRMonster.BaseStrength + bestWRMonster.BonusStrength} STR, {bestWRMonster.BaseDexterity + bestWRMonster.BonusDexterity} DEXT, {bestWRMonster.BaseLuck + bestWRMonster.BonusLuck} LUCK");
            writer.WriteLine($"Percentage of winning: {maxWins} / 100    ->     {maxWins}%");
            writer.WriteLine("-----------------------------------------------------------------");
            writer.Close();
        }
    }

}
