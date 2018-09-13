using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class SpanishInquisition : Monster
    {
        public SpanishInquisition(string name) : base(name)
        {


            AddBonusDexterity(70);
            AddBonusStrength(20);
            AddBonusVitality(5);

        }


        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            Random randy = new Random();
            int index = 0;
            MonsterData currentMonster;

            if (MonsterExists(monsters))
            {
                do
                {
                    index = randy.Next(0, monsters.Count);
                    currentMonster = monsters[index];

                } while (currentMonster.hp <= 0 || currentMonster.name == "Nobody expects" && (!(currentMonster.hp >= 100)));

            }
            else
            {
                do
                {
                    index = randy.Next(0, monsters.Count);
                    currentMonster = monsters[index];

                } while (currentMonster.hp <= 0 || currentMonster.name == "Nobody expects" && (!(currentMonster.hp >= 100)));

            }


            return index;
        }

        public bool MonsterExists(List<MonsterData> monsters)
        {
            bool found = false;

            foreach (MonsterData monster in monsters)
            {
                if (monster.hp <= 50)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }





    }
}
