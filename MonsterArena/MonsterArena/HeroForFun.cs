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

    }
}
