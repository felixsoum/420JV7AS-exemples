using System.Collections.Generic;

namespace MonsterArena
{
    class Goblin : Monster
    {
        public Goblin(string name) : base(name)
        {
            AddBonusDexterity(25);
            AddBonusLuck(25);
            AddBonusStrength(25);
            AddBonusVitality(25);
        }

        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            int index = monsters.FindIndex(m => m.level > 1);
            //Added the hp > 0 to fix the infinite loop if there are only 2 goblins left in the arena not attacking each others
            if (index >= 0 && monsters[index].hp > 0)
            {
                return index;
            }
            else
            {
                return base.GetAttackIndex(monsters);
            }
        }
    }
}
