using System.Linq;

namespace MurderInvitation
{
    class ChrisSurvivorController : ActorController
    {
        public ChrisSurvivorController(string name) : base(name)
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
            
            if ((Location.Armory == myData.CurrentLocation) && (myData.Items.Contains(Item.Gun)))
            {
                return new GameMove(myData.CurrentLocation, GameAction.NormalAttack, "", "");
            }

            else if ((Location.Armory == myData.CurrentLocation) && (gameData.isSafeUnlocked))
            {
                return new GameMove(myData.CurrentLocation, GameAction.TakeGun, "", "ILL FOKIN SHOOT YOU MATE");
            }

            else if (Location.Armory == myData.CurrentLocation)
            {
                return new GameMove(myData.CurrentLocation, GameAction.UnlockSafe, myData.Name, "Be gentle...");
            }

            return new GameMove(Location.Armory, GameAction.Nothing, "", "Fuck a duck");
            //return new GameMove(GameMove.GetRandomLocation(), GameMove.GetRandomAction(), "", "Hmm...");
        }
    }

    class ChrisKillerController : ActorController
    {
        public ChrisKillerController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        {
            return new GameMove(GameMove.GetRandomLocation(), GameAction.StabAttack, "", "My blade thirsts...");
        }
    }

    class ChrisControllerFactory : ActorControllerFactory
    {
        public ChrisControllerFactory(string name) : base(name)
        {
        }

        public override ActorController CreateSurvivorController()
        {
            return new ChrisSurvivorController(name);
        }

        public override ActorController CreateKillerController()
        {
            return new ChrisKillerController(name);
        }
    }
}
