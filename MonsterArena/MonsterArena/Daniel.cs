using System.Collections.Generic;
using MonsterArena;

namespace MonsterArena
{
    
    class Daniel : Monster
    {
        int instance = 0;
        
        public Daniel(string name) : base(name)
        {
            AddBonusStrength(75);
            AddBonusVitality(25);
            AddBonusDexterity(0);
            AddBonusLuck(0);
        
        
        
          
           
        }


        public override string ToString()
        {
            if (GetData().hp < (GetData().hp/2))
            {
            
                return "lowkey Daniel";

            }
            else
            {
                return "David Daniel";
            }
        }
        


        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            List<int> monsterWeight = new List<int>();

            int Damage;
            int instance = 0;
            Damage = 50;
            Damage = GetDamage();
           
           
            for (int i = 0; i < monsters.Count; i++)
            {
                int weight = 0;
               
                

                if (monsters[i].name == GetData().name)
                        weight -= 10000;
                if (monsters[i].hp <= 0)
                    weight -= 10000;
                
                if (instance == 0)
                {
                    if (monsters[i].hp <= Damage)
                    {
                        weight += (monsters[i].level) * 200;
                        weight += (monsters[i].hp) * 100;
                    }

                }
                if (instance > 0) 
                {
                    if (monsters[i].hp > Damage)
                    {
                        weight -= (monsters[i].level) * 1000;

                    }

                }

                weight += (monsters[i].level - 1) * 20;
                monsterWeight.Add(weight);
                instance++;
            }



            


            int index = 0;
            for (int i = 0; i < monsterWeight.Count; i++)
            {
                if (monsterWeight[i] > monsterWeight[index])
                    index = i;
            }
            instance++;
            return index;
        }
        

    }
}


