using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena
{
    class GoblinRoberto : Monster
    {
        static Random mrandom = new Random();
        public GoblinRoberto(string name) : base(name)
        {
            AddBonusDexterity(0);
            AddBonusLuck(30);
            AddBonusStrength(40);
            AddBonusVitality(30);
        }
        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            int index = 0;
            MonsterData selectedtMonster;

            if (MonsterVivant(monsters))
            {
                do
                {
                    index = mrandom.Next(0, monsters.Count);
                    selectedtMonster = monsters[index];

                } while ((selectedtMonster.hp <= 0 || selectedtMonster.name == "GolbinRoberto") && (!(selectedtMonster.hp <= 65)));
                
            }
            else
            {
                do
                {
                    index = mrandom.Next(0, monsters.Count);
                    selectedtMonster = monsters[index];

                } while (selectedtMonster.hp <=0 || selectedtMonster.name == "GolbinRoberto");

            }


            return index;
        }

        public bool MonsterVivant(List<MonsterData> monsters)
        {
            bool trouve = false;

            foreach (MonsterData monster in monsters)
            {
                if (monster.hp <= 50)
                {
                    trouve = true;
                    break;
                }
            }
            return trouve;
        }
    }
    }
