namespace MonsterArena
{
    class Daniel : Monster
    {
        public Daniel(string name) : base(name)
        {
            AddBonusStrength(70);
			AddBonusVitality(25);
            AddBonusLuck(5);
        }
    }
}
