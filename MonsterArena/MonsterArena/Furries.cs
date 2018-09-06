using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class Furry : Monster
    {
        public Furry(string name) : base(name) {

            AddBonusStrength(97);
            AddBonusLuck(1);
            AddBonusVitality(1);
            AddBonusDexterity(1);
        }









    }
}
