using System.Linq;

namespace MurderInvitation
{
    class FelixSurvivorController : ActorController
    {
        public FelixSurvivorController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        {
            var query = from actor in gameData.actorDataList
                        where actor.Name == name
                        select actor;

            ActorData myData = query.First();

            if (myData.Hp < 100 && myData.Items.Contains(Item.Medkit))
            {
                return new GameMove(myData.CurrentLocation, GameAction.UseMedkit, myData.Name);
            }
            else if (gameData.actorDataList.Count <= 2)
            {
                return new GameMove(GameMove.GetRandomLocation(), GameAction.Attack);
            }
            return new GameMove(GameMove.GetRandomLocation(), GameMove.GetRandomAction());
        }
    }

    class FelixKillerController : ActorController
    {
        public FelixKillerController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        {
            return new GameMove(GameMove.GetRandomLocation(), GameAction.Attack);
        }
    }

    class FelixControllerFactory : ActorControllerFactory
    {
        public FelixControllerFactory(string name) : base(name)
        {
        }

        public override ActorController CreateSurvivorController()
        {
            return new FelixSurvivorController(name);
        }

        public override ActorController CreateKillerController()
        {
            return new FelixKillerController(name);
        }
    }
}
