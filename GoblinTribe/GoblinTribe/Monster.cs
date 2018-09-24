namespace GoblinTribe
{
    public abstract class Monster
    {
        protected int hp = 100;
        protected int damage = 50;

        public void Attack(Monster other)
        {
            other.hp -= damage;
        }

        public int GetHp()
        {
            return hp;
        }
    }
}
