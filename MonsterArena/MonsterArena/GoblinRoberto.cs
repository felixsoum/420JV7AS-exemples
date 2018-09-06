using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class GoblinRoberto : Monster
    {
        public GoblinRoberto(string name) : base(name)
        {
            AddBonusDexterity(10);
            AddBonusLuck(10);
            AddBonusStrength(40);
            AddBonusVitality(40);
        }
    }
}
