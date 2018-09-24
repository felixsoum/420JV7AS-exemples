using System.Linq;

namespace MurderInvitation
{
    class TheoSurvivorController : ActorController
    {
        public TheoSurvivorController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        {
            var myQuery = from actor in gameData.actorDataList
                          where actor.Name == name
                          select actor;

            ActorData myData = myQuery.First();

            var actorsAliveQuery = from actor in gameData.actorDataList
                                   where actor.Hp > 0
                                   select actor;

            if (myData.Hp < 74 && myData.Items.Contains(Item.Medkit))
            {
                return new GameMove(myData.CurrentLocation, GameAction.UseMedkit, myData.Name, "I feel better.");
            }
            //if (gameData.isSafeUnlocked && !gameData.isGunTaken)
            //{
            //    return new GameMove(Location.Armory, GameAction.TakeGun, myData.Name, "I got the gun ! Get behind me, I'll protect you !");
            //}
            //if (!gameData.isMedkitTaken)
            //{
            //    return new GameMove(Location.Bathroom, GameAction.TakeMedkit, myData.Name, "I'm the support now, I'll take good care of you !");
            //}
            if (gameData.gateHp > 0)
            {
                return new GameMove(Location.Exit, GameAction.RepairGate, myData.Name, "Let's get out of here !");
            }
            if (gameData.generatorHp > 0)
            {
                return new GameMove(Location.Basement, GameAction.RepairGenerator, myData.Name, "Let there be light !");
            }

            if (actorsAliveQuery.Count() <= 2)
            {
                return new GameMove(GameMove.GetRandomLocation(), GameAction.NormalAttack, "", "I don't wanna die!");
            }
            return new GameMove(GameMove.GetRandomLocation(), GameMove.GetRandomAction(), "", "Hmm...");
        }
    }

    class TheoKillerController : ActorController
    {
        public TheoKillerController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        {
            var myQuery = from actor in gameData.actorDataList
                          where actor.Name == name
                          select actor;

            ActorData myData = myQuery.First();

            var actorsAliveQuery = from actor in gameData.actorDataList
                                   where actor.Hp > 0
                                   select actor;

            //var actorsItemsQuery = from actor in gameData.actorDataList
            //                       where actor.Items.Contains(Item.Gun) || actor.Items.Contains(Item.Medkit)
            //                       select actor;

            //ActorData dangerousActor = null;
            if (gameData.isSafeUnlocked == false)
            {
                return new GameMove(Location.Armory, GameAction.UnlockSafe, myData.Name, "I opened the safe !");
            }
            if (gameData.isSafeUnlocked && gameData.isGunTaken == false)
            {
                return new GameMove(Location.Armory, GameAction.TakeGun, myData.Name, "I got the gun ! Get behind me, I'll protect you !");
            }

            foreach (var actor in actorsAliveQuery)
            {
                if (myData.Items.Contains(Item.Gun) && actor.Name != myData.Name)
                {
                    return new GameMove(actor.CurrentLocation, GameAction.NormalAttack, actor.Name, "Take this !");
                }
                else if (actor.Items.Count > 0 && actor.Name != myData.Name)
                {
                    return new GameMove(actor.CurrentLocation, GameAction.StabAttack, actor.Name, "I love blood...");
                    //dangerousActor = actor;
                }
                else if (actor.Hp <= 50 && actor.Hp > 0 && actor.Name != myData.Name)
                {
                    return new GameMove(actor.CurrentLocation, GameAction.StabAttack, actor.Name, "I love blood...");
                }
                
            }
            //if (actorsItemsQuery != null)
            //{
            //    ActorData dangerousActor = actorsItemsQuery.First();
            //}
            ActorData survivorActor = actorsAliveQuery.First();
            // je voulais faire une liste des actors dangereux


            if (myData.Hp < 74 && myData.Items.Contains(Item.Medkit))
            {
                return new GameMove(myData.CurrentLocation, GameAction.UseMedkit, myData.Name, "I feel better.");
            }
            
            
            //if (dangerousActor != null)
            //{
            //    return new GameMove(dangerousActor.CurrentLocation, GameAction.StabAttack, "", "I need your life...");
            //}
            //if (actorsAliveQuery.ToList())
            //{
            //    return new GameMove(survivorActor.CurrentLocation, GameAction.StabAttack, "", "I need your life...");
            //}
            
            if (!gameData.isMedkitTaken)
            {
                return new GameMove(Location.Bathroom, GameAction.TakeMedkit, myData.Name, "I'm the support now, I'll take good care of you !");
            }
            
            return new GameMove(GameMove.GetRandomLocation(), GameAction.StabAttack, "", "My blade thirsts...");
        }
    }

    class TheoControllerFactory : ActorControllerFactory
    {
        public TheoControllerFactory(string name) : base(name)
        {
        }

        public override ActorController CreateSurvivorController()
        {
            return new TheoSurvivorController(name);
        }

        public override ActorController CreateKillerController()
        {
            return new TheoKillerController(name);
        }
    }
}
