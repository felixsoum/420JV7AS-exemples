using System.Linq;

namespace MurderInvitation
{
    class AlonsoSurvivorController : ActorController
    {
        public AlonsoSurvivorController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        {


            

            ;

            var hasGun = from actor in gameData.actorDataList
                         where actor.Name != name && actor.Items.Contains(Item.Gun) && actor.Hp > 0
                         select actor;
           
            if (hasGun.Count() != 0)
            {
                ActorData badGuy = hasGun.First();
                return new GameMove(badGuy.CurrentLocation, GameAction.NormalAttack, badGuy.Name);

            }
           
            else if (gameData.actorDataList.Count <= 2)
            {
                var BadGuyLocation = from actor in gameData.actorDataList
                                     where actor.Name != name
                                     select actor.CurrentLocation;
                                        
                                        
                return new GameMove(BadGuyLocation.First(), GameAction.NormalAttack);
            }
            if (gameData.generatorHp > 0)
            {
                return new GameMove(Location.Basement, GameAction.RepairGenerator);
            }
            else if (gameData.gateHp > 0) {
                return new GameMove(Location.Exit, GameAction.RepairGate);

            }
            return new GameMove(GameMove.GetRandomLocation(), GameMove.GetRandomAction());
        }
    }

    class AlonsoKillerController : ActorController
    {
        public AlonsoKillerController(string name) : base(name)
        {
        }
        
        public override GameMove GenerateMove(GameData gameData)
        {
            var ezKill = from actor in gameData.actorDataList
                         where actor.Hp <= 50 && actor.Hp > 0
                         select actor;
            var everybody = from actor in gameData.actorDataList
                            where actor.Hp > 0 && actor.Name != name
                            select actor;
            if (ezKill.Count() != 0) {
                return new GameMove(ezKill.First().CurrentLocation, GameAction.StabAttack);

            }
            return new GameMove(everybody.First().CurrentLocation , GameAction.StabAttack);
        }
    }

    class AlonsoControllerFactory : ActorControllerFactory
    {
        public AlonsoControllerFactory(string name) : base(name)
        {
        }

        public override ActorController CreateSurvivorController()
        {
            return new AlonsoSurvivorController(name);
        }

        public override ActorController CreateKillerController()
        {
            return new AlonsoKillerController(name);
        }
    }
}
