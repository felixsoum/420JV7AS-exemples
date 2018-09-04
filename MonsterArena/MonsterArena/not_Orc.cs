namespace MonsterArena
{
    class Human : Monster
    {
        public Human(string name) : base(name)
        {
            AddBonusStrength(50);
            AddBonusVitality(50);
        }
    }
}
