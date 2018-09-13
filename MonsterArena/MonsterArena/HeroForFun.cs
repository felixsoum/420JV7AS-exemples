using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class HeroForFun: Monster
    {
        public HeroForFun(string name) : base(name)
        {
            AddBonusStrength(85);
            AddBonusVitality(5);
            AddBonusLuck(5);
            AddBonusDexterity(5);

        }
        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            int index = 0;
            MonsterData currentMonster;
            Random nrandom = new Random();
            do
            {

                if (Find(monsters))
                    index = nrandom.Next(0, monsters.Count);

                currentMonster = monsters[index];


            } while (currentMonster.hp <= 0 || currentMonster.name == "Saitama");

            return index;
        }
        public bool Find(List<MonsterData> monsters)
        {
            bool found = false;

            foreach (MonsterData monster in monsters)
            {
                if (monster.hp <= 100)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
    }
}
