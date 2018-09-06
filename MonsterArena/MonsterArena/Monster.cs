using System;
using System.Collections.Generic;

namespace MonsterArena
{
    abstract class Monster : IComparable<Monster>
    {
        protected const int BonusMaxCount = 100;

        string name;
        int hp;
        int level = 1;
        static Random random = new Random();
        bool isSpawned;

        public int BaseStrength { get; } = 25;
        public int BaseVitality { get; } = 25;
        public int BaseDexterity { get; } = 25;
        public int BaseLuck { get; } = 25;
        public int BonusStrength { get; set; }
        public int BonusVitality { get; set; }
        public int BonusDexterity { get; set; }
        public int BonusLuck { get; set; }

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
            int damage = BaseStrength + BonusStrength;
            bool isCrit = random.Next(0, 100) < BaseLuck + BonusLuck;
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
                BonusDexterity += target.BonusDexterity;
                BonusStrength += target.BonusStrength;
                BonusVitality += target.BonusVitality;
                BonusLuck += target.BonusLuck;
                Heal();
                Console.WriteLine($"{target} dies! {this} is now level {++level}!");
            }
        }

        public override string ToString()
        {
            return $"{name} the {GetType().Name} (hp:{hp}/lvl:{level})";
        }

        public int GetSpeed()
        {
            return BaseDexterity + BonusDexterity;
        }

        public bool IsDead()
        {
            return hp <= 0;
        }

        protected void AddBonusStrength(int value)
        {
            if (value < 0)
                return;
            BonusStrength = Math.Min(value, GetRemainingBonus());
        }

        protected void AddBonusVitality(int value)
        {
            if (value < 0)
                return;
            BonusVitality = Math.Min(value, GetRemainingBonus());
        }

        protected void AddBonusDexterity(int value)
        {
            if (value < 0)
                return;
            BonusDexterity = Math.Min(value, GetRemainingBonus());
        }

        protected void AddBonusLuck(int value)
        {
            if (value < 0)
                return;
            BonusLuck = Math.Min(value, GetRemainingBonus());
        }

        int GetRemainingBonus()
        {
            return Math.Max(BonusMaxCount - BonusStrength - BonusVitality - BonusDexterity - BonusLuck, 0);
        }

        void Heal()
        {
            hp = 2 * (BaseVitality + BonusVitality);
        }

        public int CompareTo(Monster other)
        {
            return -GetSpeed().CompareTo(other.GetSpeed());
        }
    }
}
