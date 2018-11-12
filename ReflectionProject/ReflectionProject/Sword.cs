namespace ReflectionProject
{
    class Sword
    {
        [Clamp(1, 10)]
        public int damage;

        public Sword(int dmg)
        {
            damage = dmg;
        }
    }
}
