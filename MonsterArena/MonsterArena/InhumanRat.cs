using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class InhumanRat:Monster
    {
        public InhumanRat(string name) : base(name)
        {
            AddBonusStrength(40);
            AddBonusVitality(50);
            AddBonusDexterity(0);
            AddBonusLuck(10);
        }

        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            int indexAttack = 0;
            int ennemyValue = 0;

            for (int i = 0; i < monsters.Count; i++)
            {
                int _ennemyValue = 0;
                if (monsters[i].name == GetData().name)// si meme nom que moi, pas attacker
                {
                    //logique de pas attacker
                    _ennemyValue -= 1000;
                }
                if (monsters[i].hp <= 0)// si hp négatif, pas attacker
                {
                    //logique de pas attacker
                    _ennemyValue -= 1000;
                }
                if (monsters[i].hp < GetDamage() || monsters[i].hp >= 0)
                {
                    //logique d'attacker en premier
                    _ennemyValue += 20;
                }
                if (monsters[i].level > 1)
                {
                    _ennemyValue += monsters[i].level;
                }
                if (_ennemyValue > ennemyValue)
                {
                    indexAttack = i;
                    ennemyValue = _ennemyValue;
                }
            }

            return indexAttack;
        }
    }
}
