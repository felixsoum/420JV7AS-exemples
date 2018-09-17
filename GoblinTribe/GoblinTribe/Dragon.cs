namespace GoblinTribe
{
    public class Dragon : Monster
    {
        public Dragon()
        {
            hp = 1000;
            damage = 500;
            Goblin.Tribe = "Fire";
        }

        public override string ToString()
        {
            return "A powerful dragon.";
        }
    }
}
