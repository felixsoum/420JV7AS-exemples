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
            AddBonusLuck(100);
        }
    }
}
