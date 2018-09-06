using System;

namespace MonsterDuel
{
    class Goblin : Monster
    {
        public Goblin(string firstName) : base(firstName)
        {
            bonusDamage = 10;
        }

        protected override void OnDeath()
        {
            Console.WriteLine($"{name}: My kin will... avenge me...");
        }
    }
}
