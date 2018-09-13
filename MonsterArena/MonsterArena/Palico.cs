using System.Collections.Generic;

namespace MonsterArena
{
    class Palico : Monster
    {
        public Palico(string name) : base(name)
        {
            AddBonusStrength(35);
            AddBonusVitality(8);
            AddBonusDexterity(57);
        }
        
        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            int target = 0;
            List<MonsterData> PotentialTargets = new List<MonsterData>(monsters.Count);

            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].name != GetData().name && monsters[i].hp > 0)
                {
                    if (monsters[i].hp <= GetDamage())
                    {
                      PotentialTargets.Add(monsters[i]);
                        
                    }

                }


            }
            if (PotentialTargets.Count > 1)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < PotentialTargets.Count - 1; i++)
                    {
                        if (PotentialTargets[i].hp < PotentialTargets[i + 1].hp)
                        {
                            MonsterData temp = PotentialTargets[i + 1];
                            PotentialTargets[i + 1] = PotentialTargets[i];
                            PotentialTargets[i] = temp;

                        }

                    }
                }
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].type == PotentialTargets[0].type)
                    {
                        target = i;

                        break;
                        ;
                    }
                }
            }
            else
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].name != GetData().name && monsters[i].hp > 0 && monsters[i].hp / 2 < GetDamage())
                    {
                        target = i;
                        break;

                    }
                    else
                    {
                        for (int j = 0; j < monsters.Count; j++)
                        {
                            if (monsters[j].name != GetData().name && monsters[j].hp > 0)
                            {
                                target = j;
                                break;

                            }

                        }

                    }
                }

            }
           
            
            return target;
        }
    }
}
