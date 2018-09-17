using System;
using System.Collections.Generic;

namespace MonsterArena
{
    abstract class Monster : IComparable<Monster>
    {
        int baseStrength = 25;
        int baseVitality = 25;
        int baseDexterity = 25;
        int baseLuck = 25;

        int bonusStrength;
        int bonusVitality;
        int bonusDexterity;
        int bonusLuck;
        protected const int BonusMaxCount = 100;

        string name;
        int hp;
        int level = 1;
        static Random random = new Random();
        bool isSpawned;

        public Monster(string name)
        {
            this.name = name;
        }

        public void Spawn()
        {
            if (!isSpawned)
            {
                isSpawned = true;
                Heal();
                Console.WriteLine($"{this} enters the battle!");
            }
        }

        protected int GetDamage()
        {
            return baseStrength + bonusStrength;
        }

        public virtual int GetAttackIndex(List<MonsterData> monsters)
        {
            return random.Next(0, monsters.Count);
        }

        public MonsterData GetData()
        {
            return new MonsterData(name, GetType().Name, hp, level);
        }

        public void Attack(Monster target)
        {
            if (target.IsDead())
            {
                Console.WriteLine($"{this} attacks {target}, but it's already dead.");
                return;
            }
            int damage = GetDamage();
            bool isCrit = random.Next(0, 100) < (baseLuck + bonusLuck);
            if (isCrit)
            {
                damage *= 2;
            }
            string damageType = isCrit ? "CRITICAL DAMAGE" : "damage";
            Console.WriteLine($"{this} attacks {target} for {damage} {damageType}!");

            target.hp -= damage;
            if (target.IsDead())
            {
                if (this == target)
                {
                    Console.WriteLine($"{this} dies by suicide...");
                    return;
                }
                bonusDexterity += target.bonusDexterity;
                bonusStrength += target.bonusStrength;
                bonusVitality += target.bonusVitality;
                bonusLuck += target.bonusLuck;
                level += target.level;
                Heal();
                Console.WriteLine($"{target} dies! {this} is now level {level}!");
            }
        }

        public override string ToString()
        {
            return $"{name} the {GetType().Name} (hp:{hp}/lvl:{level})";
        }

        public int GetSpeed()
        {
            return baseDexterity + bonusDexterity;
        }

        public bool IsDead()
        {
            return hp <= 0;
        }

        protected void AddBonusStrength(int value)
        {
            if (value < 0)
                return;
            bonusStrength = Math.Min(value, GetRemainingBonus());
        }

        protected void AddBonusVitality(int value)
        {
            if (value < 0)
                return;
            bonusVitality = Math.Min(value, GetRemainingBonus());
        }

        protected void AddBonusDexterity(int value)
        {
            if (value < 0)
                return;
            bonusDexterity = Math.Min(value, GetRemainingBonus());
        }

        protected void AddBonusLuck(int value)
        {
            if (value < 0)
                return;
            bonusLuck = Math.Min(value, GetRemainingBonus());
        }

        int GetRemainingBonus()
        {
            return Math.Max(BonusMaxCount - bonusStrength - bonusVitality - bonusDexterity - bonusLuck, 0);
        }

        void Heal()
        {
            hp = 2 * (baseVitality + bonusVitality);
        }

        public int CompareTo(Monster other)
        {
            return -GetSpeed().CompareTo(other.GetSpeed());
        }
    }
}
