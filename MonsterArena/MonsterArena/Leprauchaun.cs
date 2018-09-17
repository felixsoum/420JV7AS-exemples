using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class Leprauchaun : Monster
    {
        public Leprauchaun(string name) : base(name)    
        {
            AddBonusLuck(75);
            AddBonusVitality(25);
        }

        public bool noselftarget(List<MonsterData> monsters)
        {
            bool SelfTarget = false;

            foreach (MonsterData monster in monsters)
            {
                if (monster.name == "Leprauchaun")
                {
                    SelfTarget = true;
                }
                else
                {
                    SelfTarget = false;
                }
            }
            return SelfTarget;
        }

        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            int index = 0;
            Random random = new Random();

            if (noselftarget(monsters) == false)
            {
                index = random.Next(0, monsters.Count);
            }

            return index;
        }

    }
}

