namespace MonsterArena
{
    struct MonsterData
    {
        public string name;
        public string type;
        public int hp;
        public int level;


        public MonsterData(string name, string type, int hp, int level)
        {
            this.name = name;
            this.type = type;
            this.hp = hp;
            this.level = level;
        }
    }
}
