using System.Collections.Generic;

namespace MonsterArena
{
    class Compte_Harebourg : Monster
    {
        public Compte_Harebourg(string name) : base(name)
        {
            AddBonusStrength(70);
            AddBonusVitality(10);
            AddBonusDexterity(20);
            AddBonusLuck(0);
        }

        public override int GetAttackIndex(List<MonsterData> monsters)
        {
            System.Console.WriteLine("Prepaire to die!");
            List<MonsterData> possibleTargets = new List<MonsterData>();
            List<MonsterData> goodTarget = new List<MonsterData>();
            List<MonsterData> betterTarget = new List<MonsterData>();
            MonsterData bestTarget=monsters[0];

            foreach(MonsterData md in monsters)
            {
                if(md.hp>0&& md.type != this.GetData().type)
                {
                    possibleTargets.Add(md);
                }
            }

            foreach(MonsterData md in possibleTargets)
            {
                if(md.hp<=this.GetDamage())
                {
                    goodTarget.Add(md);
                }
            }
            int GetBestLevel(List<MonsterData> targets)
            {
                int lvl=0;
                foreach(MonsterData md in targets)
                {
                    if(md.level>lvl)
                    {
                        lvl = md.level;
                    }
                }
                return lvl;
            }
            foreach(MonsterData md in goodTarget)
            {
                if(md.level==GetBestLevel(possibleTargets))
                {
                    betterTarget.Add(md);
                }
            }
            int GetLowestHp(List<MonsterData> targets)
            {
                int Hp=1000000;
                foreach (MonsterData md in targets)
                {
                    if (md.hp < Hp)
                    {
                        Hp = md.hp;
                    }
                }
                return Hp;
            }
            foreach(MonsterData md in betterTarget)
            {
                if(md.hp==GetLowestHp(betterTarget))
                {
                    bestTarget = md;
                }
            }
            return monsters.IndexOf(bestTarget);
        }

        

    }
}
