using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterArena;

namespace Nana
{
   
   
    class Lich : MonsterArena.Monster
    {
        int turn = 0;
        int type = 0;
 
        public Lich(string name) : base(name)
        {
            
            AddBonusDexterity(50);
            AddBonusStrength(10);
        
            AddBonusVitality(40);
            AddBonusLuck(0);
  
        }
        private Lich(string name, int f): base (name)
        {
            type = f;
            switch (f)
            {
                case (1):
                    name = "[Vigor & Force]";
                    AddBonusDexterity(0);
                    AddBonusStrength(10);
                    AddBonusVitality(80);
                    AddBonusLuck(10);
                    break;
                case (2):
                    name = "[Swiftness and Agility]";
                     AddBonusDexterity(80);
                    AddBonusStrength(10);

                    AddBonusVitality(10);
                    AddBonusLuck(0);
                    var ea = new Lich("[Vigor & Force]", 1);
                    ea.Spawn();
                    while (ea.GetData().hp > 0)
                        Attack(ea);
                    break;
                case (3):
                    name = "[Favor & Blessing]";
                    AddBonusDexterity(0);
                    AddBonusStrength(70);

                    AddBonusVitality(0);
                    AddBonusLuck(30);
                    var e = new Lich("[Swiftness & Agility]",2);
                    e.Spawn();
                    while (e.GetData().hp > 0)
                        Attack(e);
                    break;
                case (4):
                    name = "[Accurency & Resillience]";
                    AddBonusDexterity(10);
                    AddBonusStrength(40);

                    AddBonusVitality(40);
                    AddBonusLuck(10);
                
                    break;
                default:
                    break;
            }
        }



 
        bool flex = false;
        public override string ToString()
        {
            if (type == 0)
                return $"{GetData().name} the {GetType().Name} (hp:{GetData().hp}/lvl:{GetData().level}/pwr:{GetDamage()})";
            else return "Spirit - " +GetData().name;
        }

        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            if (type != 0) return 0;
            if (turn == 0)
            {
                Console.WriteLine("");
                Console.WriteLine(GetData().name + " has stopped the time!");
                //System.Threading.Thread.Sleep(1000);
                Console.Write(GetData().name + ": ");
                for (int i = 1; i < 70; i++)
                {
                    Console.Write("Useless! ");
                    //System.Threading.Thread.Sleep(700 / i);
                }
                Console.Write("It's useless!");
                //System.Threading.Thread.Sleep(1000);
                Console.WriteLine("");
                Console.WriteLine(GetData().name + ": Your Efforts is for naught!");
                //System.Threading.Thread.Sleep(1000);
            }

            turn++;
            var who = 0;

            if (turn <= 5)
            {

                Lich e;

                if (turn == 1) e = new Nana.Lich("[Favor & Blessing]", 3);
                else if (turn == 2) e = new Nana.Lich("[Swiftness and Agility]", 2);
                else if (GetData().hp < 400 && turn > 3) e = new Lich("[Vigor & Force]", 1);
                else e = new Nana.Lich("[Accurency & Resillience]", 4);

                Console.WriteLine(" ");
                //if (turn < 2) System.Threading.Thread.Sleep(300);
                Console.Write(GetData().name + ": Come forth,");
                //if (turn < 2) System.Threading.Thread.Sleep(300);
                Console.Write(e.GetData().name + "!");
                //if (turn < 2) System.Threading.Thread.Sleep(500);
                Console.WriteLine(" ");
                for (int i = 0; i < 3; i++)
                {
                    e.Spawn();
                    while (e.GetData().hp > 0)
                    {
                        Attack(e);
                        //if (turn < 2) System.Threading.Thread.Sleep(200);
                    }

                }

                Console.WriteLine(GetData().name + " absorbed " + e.GetData().name + "!");
                if (turn == 0) Console.WriteLine("Time Resumed!");
                //if (turn < 2) System.Threading.Thread.Sleep(300);

            }

            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].name == GetData().name) continue;
                if (monsters[i].hp <= 0) continue;
                who = i;
                if (monsters[who].hp > monsters[i].hp && monsters[i].name != GetData().name) who = i;


            }
            Console.WriteLine(" ");
            if (GetData().hp > 100)
            {
                if (turn == 1)
                    Console.WriteLine(GetData().name + ": This isn't even a fraction of my power!");
                else if (turn == 2)
                    Console.WriteLine(GetData().name + ": Suffer!");
                else if (turn == 3)
                    Console.WriteLine(GetData().name + ": Hmph, What pathetic weaklings.");
                else if (turn == 4)
                    Console.WriteLine(GetData().name + ": You were already dead.");
                else if (turn == 5)
                    Console.WriteLine(GetData().name + ": Told you, you ought to surrender, mortal.");
                //System.Threading.Thread.Sleep(100);
            }
            else if (GetData().hp < 40)
            {
                Console.WriteLine(GetData().name + ": I-impossible!");
            }
            if (GetDamage() > 500 && !flex)
            {
                Console.WriteLine(GetData().name + ": It's Over.");
                //System.Threading.Thread.Sleep(1200);
                Console.WriteLine(GetData().name + ": My power level is over 500. Stop struggling, there isn't anything you can do. I'll give you a painless death.");
                //System.Threading.Thread.Sleep(300);
                flex = true;
            }


            Console.WriteLine(" ");
            // if(monster[who].hp < )
            return who;
        }
         

    }






}
