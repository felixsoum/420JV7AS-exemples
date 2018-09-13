using System.Collections.Generic;

namespace MonsterArena
{
    class TheLegend27 : Monster
    {
        public TheLegend27(string name) : base(name)
        {
            AddBonusLuck(25);
            AddBonusStrength(60);
            AddBonusVitality(15);
        }
		
		public override int GetAttackIndex(List<MonsterData> monsters)
        {
			List<int> monsterWeight = new List<int>();

			for (int i = 0; i < monsters.Count; i++)//For each monster
			{
				int weight = 0;

				if (monsters[i].name == GetData().name)//I'm not tasty
					weight -= 10000;
				if (monsters[i].hp <= 0)//Dead people aren't tasty either
					weight -= 10000;
				if (GetDamage() >= monsters[i].hp)//It's more tasty if I can kill it right now
					weight += 100;

				weight = monsters[i].hp;//The more HP, the more tasty
				weight += (monsters[i].level - 1) * 20;//More LVL -> more tasty

				monsterWeight.Add(weight);
			}

			int index = 0;
			for (int i = 0; i < monsterWeight.Count; i++)
			{
				if (monsterWeight[i] > monsterWeight[index])
					index = i;
			}

			return index;
		}
    }
}
