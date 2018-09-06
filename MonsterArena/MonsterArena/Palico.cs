namespace MonsterArena
{
    class Palico : Monster
    {
        public Palico(string name) : base(name)
        {
            AddBonusStrength(26);
            AddBonusVitality(1);
            AddBonusDexterity(73);
        }
    }
}
