using System;
using System.Collections.Generic;

namespace MonsterArena
{
    /*
     * AUTHOR: Samuel Paquette
     * DATE: 30 August 2018
     */
    class PereFwetar : Monster
    {

        //Stats of pere fwetar
        public const int VITALITY = 110;
        public const int DAMAGE = 90;
        public const int DEXT = 30;
        public const int LUCK = 25;

        /*
         * Constructor for the NPC Pere Fwetar
         */
        public PereFwetar(string name) : base(name)
        {
            AddBonusVitality(30);
            AddBonusStrength(65);
            AddBonusDexterity(5);
        }

        /*
         * Algorithm used by the Pere Fwetar to know who to attack
         */
        public override int GetAttackIndex(List<MonsterData> monsters)
        {

            //Creates another list with only the monsters that are ALIVE and that arent us
            List<MonsterData> monstersToAttack = new List<MonsterData>();

            //we create a dummy NPC temporary until we determine which npc is the most efficient to attack
            MonsterData toAttack = new MonsterData(" ", " ", 1, 1);

            //Makes sure there are any monsters left
            if (monsters.Count > 0)
            {
                foreach (MonsterData monster in monsters)
                {
                    //if the hp is more than 0, that means the monster is alive
                    if (monster.hp > 0)
                    {
                        //If its ourself, do not add it to the list, cause we dont want to attack ourself (prevent suicide)
                        string ourType = GetType().Name;
                        if(monster.type != ourType)
                        {
                            monstersToAttack.Add(monster);
                        }
                    }
                }
            }

            //Create another list with the npcs that we can kill this turn
            List<MonsterData> canKill = new List<MonsterData>();

            //Make sure there are atleast 1 enemy to attack
            if (monstersToAttack.Count > 0)
            {
                foreach (MonsterData monster in monstersToAttack)
                {
                    //If you can kill the NPC, focus that NPC
                    if(monster.hp <= DAMAGE)
                    {
                        canKill.Add(monster);
                    }
                    //Checks for the NPC with the highest hp
                    if(toAttack.hp < monster.hp)
                    {
                        toAttack = monster;
                    }
                }
            }

            //Checks if we have atleast 1 npc that we can kill this turn
            if(canKill.Count > 0)
            {
                //Checks for the best npc that we can kill this turn and choose that one
                foreach (MonsterData monster in canKill)
                {
                    //we focus the monster that is the highest level, since we steal the stats of that npc if we kill it
                    if(monster.level > toAttack.level)
                    {
                        toAttack = monster;
                    }
                }
            }

            return monsters.IndexOf(toAttack);
        }

    }
}
