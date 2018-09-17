using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class GiantSlug : Monster
    {
        public GiantSlug(string name) : base(name)
        {
            AddBonusDexterity(1);
            AddBonusLuck(5);
            AddBonusStrength(45);
            AddBonusVitality(49);
        }

        public bool BestTarget(List<MonsterData> monsters)
        {
            foreach (MonsterData monster in monsters)
            {
                if (monster.hp > 0 && monster.hp <= 100 && monster.name != "Babygirl")
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            int index = 0;
            MonsterData currentmonster;
            Random personnalrandom = new Random();

            if (BestTarget(monsters) == true)
            {
                do
                {
                    index = personnalrandom.Next(0, monsters.Count);
                    currentmonster = monsters[index];

                }
                while (currentmonster.hp <= 0 || currentmonster.name == "Babygirl" || currentmonster.hp > 100);
            }
            else
            {
                do
                {
                    index = personnalrandom.Next(0, monsters.Count);
                    currentmonster = monsters[index];

                } while (currentmonster.hp <= 0 || currentmonster.name == "Babygirl");
            }

            return index;
        }

    }
}
