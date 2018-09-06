namespace MonsterArena
{
    class Orc : Monster
    {
        public Orc(string name) : base(name)
        {
            AddBonusStrength(50);
            AddBonusVitality(50);
        }
    }
}
