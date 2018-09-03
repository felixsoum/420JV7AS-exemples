using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterArena;

namespace Nana
{
    class Spirit : MonsterArena.Monster
    {
        public Spirit(string name) : base(name)
        {
            AddBonusDexterity(0);
            AddBonusStrength(50);

            AddBonusVitality(50);
            AddBonusLuck(0);
        }


    }
    class Lich : MonsterArena.Monster
    {
        int turn = 0;
        public Lich(string name) : base(name)
        {
            AddBonusDexterity(40);
            AddBonusStrength(20);
        
            AddBonusVitality(40);
            AddBonusLuck(0);
          
        }
        public override int GetAttackIndex(List<MonsterData> monsters)
        {
       
            Console.WriteLine(GetData().name + " has stopped the time!");
            Console.WriteLine("");
			turn++;
            var who = 0;



            if (turn <=5)
            {
               
                var e = new Nana.Spirit("Strenght and Vigority");
                Console.WriteLine(GetData().name + ": My spirit! Give me strengh! Offer your life to me and please your master!");
                for (int i = 0; i < 3; i++)
                {
                    e.Spawn();
                    while (e.GetData().hp > 0)
                    {
                        Attack(e);
                    }
    
                }

                Console.WriteLine(GetData().name + " is getting more power!");
                
            }
          
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].name == GetData().name) continue;
                if (monsters[i].hp <= 0) continue;
                who = i;
                if (monsters[who].hp > monsters[i].hp && monsters[i].name != GetData().name) who = i;
            
            }
            if(turn == 0)
            Console.WriteLine(GetData().name + ": This isn't even a fraction of my power!");
            else if (turn == 1)
                Console.WriteLine(GetData().name + ": Suffer!");
            else if (turn == 2)
                Console.WriteLine(GetData().name + ": Hmph, What pathetic weaklings.");
            else if (turn == 3)
                Console.WriteLine(GetData().name + ": You were already dead.");
            else if (turn == 4)
                Console.WriteLine(GetData().name + ":AHAHAHAHAH! What tremendous power!");

          
           // if(monster[who].hp < )
            return who;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
     
    }
}
