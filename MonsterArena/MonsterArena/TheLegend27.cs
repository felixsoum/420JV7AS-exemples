using System.Collections.Generic;

namespace MonsterArena
{
    class TheLegend27 : Monster
    {
        public TheLegend27(string name) : base(name)
        {
            AddBonusLuck(25);
            AddBonusStrength(60);
            AddBonusVitality(15);
        }
    }
}
