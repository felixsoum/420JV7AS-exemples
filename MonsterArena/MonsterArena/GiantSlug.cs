using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class GiantSlug:Monster
    {
        public GiantSlug(string name) : base(name)
        {
            AddBonusDexterity(40);
            AddBonusLuck(0);
            AddBonusStrength(30);
            AddBonusVitality(40);
        }

        
        
    }
}
