namespace Cours03LINQ
{
    class Goblin
    {
        public string Tribe { get; set; }
        public int Hp { get; set; }
        public string Name { get; set; }

        public Goblin(string tribe, int hp, string name)
        {
            Tribe = tribe;
            Hp = hp;
            Name = name;
        }
    }
}
