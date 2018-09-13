using System;
using System.Collections.Generic;
using System.Linq;

namespace MurderInvitation
{
    class Program
    {
        public enum GameState
        {
            Ongoing,
            SurvivorVictory,
            KillerVictory
        }

        public static Random random = new Random();
        static void Main(string[] args)
        {
            Actor killer = null;
            var actors = new List<Actor>();
            var actorControllers = new List<ActorController>();
            var factories = new List<ActorControllerFactory>()
            {
                new FelixControllerFactory("Brad"),
                new FelixControllerFactory("Samuel"),
                new FelixControllerFactory("Chris"),
                new FelixControllerFactory("David"),
                new FelixControllerFactory("Yassine"),
            };
            
            // Create 1 killer and 4 survivors
            while (actors.Count < 5)
            {
                ActorControllerFactory factory = factories[random.Next(0, factories.Count)];
                factories.Remove(factory);

                if (actors.Count == 0)
                {
                    killer = new Actor(factory.name)
                    {
                        isKiller = true
                    };
                    actors.Add(killer);
                    actorControllers.Add(factory.CreateKillerController());
                }
                else
                {
                    int insertionIndex = random.Next(0, actors.Count + 1);
                    actors.Insert(insertionIndex, new Actor(factory.name));
                    actorControllers.Insert(insertionIndex, factory.CreateSurvivorController());
                }
            }

            var actorDataQuery = from actor in actors
                                 select actor.actorData;

            var actorDataList = actorDataQuery.ToList();

            var gameData = new GameData(actorDataList);
            GameState gameState = GameState.Ongoing;

            // Main game loop
            while (gameState == GameState.Ongoing)
            {
                // Generate every actor's move
                var moves = new List<GameMove>();
                for (int i = 0; i < actors.Count; i++)
                {
                    Actor actor = actors[i];
                    if (actor.hp <= 0)
                    {
                        continue;
                    }
                    ActorController controller = actorControllers[i];
                    GameMove move = controller.GenerateMove(gameData.Clone());
                    move.actor = actor;
                    moves.Add(move);
                }

                // Process all game moves
                foreach (var move in moves)
                {
                    if (move.actor.hp <= 0)
                    {
                        continue;
                    }
                    ProcessMove(move, actors, gameData);
                    gameState = GetGameState(gameData, killer);
                    if (gameState != GameState.Ongoing)
                    {
                        break;
                    }
                }
            }

            foreach (var actor in actors)
            {
                if (actor.hp > 0 && !actor.isKiller)
                {
                    Console.WriteLine($"{actor.name} has survived the murder invitation.");
                }
            }
        }

        static GameState GetGameState(GameData gameData, Actor killer)
        {
            // Everything is repaired
            if (gameData.gateHp == 0 && gameData.generatorHp == 0)
            {
                Console.WriteLine($"Generator and exit gate repaired!\nSurvivors escape! {killer.name} disappears in the shadows...");
                killer.hp -= 100;
                return GameState.SurvivorVictory;
            }

            var aliveQuery = from actor in gameData.actorDataList
                             where actor.Hp > 0
                             select actor;

            // Killer is dead
            if (killer.hp <= 0)
            {
                Console.WriteLine($"Survivors have defeated {killer.name} the killer!");
                return GameState.SurvivorVictory;
            }

            // Only killer alive
            if (aliveQuery.Count() == 1)
            {
                Console.WriteLine($"The {killer.name} the killer has eliminated all survivors!");
                return GameState.KillerVictory;
            }
            return GameState.Ongoing;
        }

        static void ProcessMove(GameMove gameMove, List<Actor> actors, GameData gameData)
        {
            Actor currentActor = gameMove.actor;
            gameMove.actor.currentLocation = gameMove.nextLocation;

            switch (gameMove.gameAction)
            {
                default:
                case GameAction.Nothing:
                    break;
                case GameAction.Attack:
                    Actor attackTarget = actors.Find(actor => actor.name == gameMove.actionTarget);
                    if (attackTarget == null)
                    {
                        var otherActorsQuery = from actor in actors
                                               where actor.currentLocation == gameMove.nextLocation
                                               && actor.hp > 0
                                               && actor != currentActor
                                               select actor;

                        var result = otherActorsQuery.ToList();
                        if (result.Count > 0)
                        {
                            attackTarget = result[random.Next(0, result.Count)];
                        }
                    }
                    if (attackTarget != null)
                    {
                        if (currentActor.items.Contains(Item.Gun))
                        {
                            Console.WriteLine($"BANG! {currentActor.name} shoots {attackTarget.name} with a gun!");
                            attackTarget.hp -= 100;
                        }
                        else
                        {
                            if (currentActor.isKiller)
                            {
                                Console.WriteLine($"{attackTarget.name} is stabbed by the killer in the shadows.");
                                attackTarget.hp -= 50;
                            }
                            else
                            {
                                Console.WriteLine($"{currentActor.name} punches {attackTarget.name} in the face!");
                                attackTarget.hp -= 50;
                            }
                        }
                        if (attackTarget.hp <= 0)
                        {
                            Console.WriteLine($"{attackTarget.name} has died...");
                            currentActor.items.AddRange(attackTarget.items);
                            attackTarget.items.Clear();
                        }
                    }
                    break;
                case GameAction.RepairGenerator:
                    if (currentActor.currentLocation == Location.Basement && gameData.generatorHp > 0)
                    {
                        gameData.generatorHp -= 25;
                        Console.WriteLine($"{currentActor.name} repairs the generator in the basement.");
                    }
                    break;
                case GameAction.RepairGate:
                    if (currentActor.currentLocation == Location.Exit && gameData.gateHp > 0)
                    {
                        gameData.gateHp -= 25;
                        Console.WriteLine($"{currentActor.name} repairs the exit gate.");
                    }
                    break;
                case GameAction.UnlockSafe:
                    if (currentActor.currentLocation == Location.Armory && !gameData.isSafeUnlocked)
                    {
                        gameData.isSafeUnlocked = true;
                        Console.WriteLine($"{currentActor.name} unlocks the safe containing the gun.");
                    }
                    break;
                case GameAction.TakeGun:
                    if (currentActor.currentLocation == Location.Armory && !gameData.isGunTaken && gameData.isSafeUnlocked)
                    {
                        gameData.isGunTaken = true;
                        currentActor.items.Add(Item.Gun);
                        Console.WriteLine($"{currentActor.name} takes the gun in the safe.");
                    }
                    break;
                case GameAction.TakeMedkit:
                    if (currentActor.currentLocation == Location.Bathroom && !gameData.isMedkitTaken)
                    {
                        gameData.isMedkitTaken= true;
                        currentActor.items.Add(Item.Medkit);
                        Console.WriteLine($"{currentActor.name} takes the medkit in the bathroom.");
                    }
                    break;
                case GameAction.UseMedkit:
                    if (currentActor.items.Contains(Item.Medkit))
                    {
                        Actor healTarget = actors.Find(actor => actor.name == gameMove.actionTarget);
                        if (healTarget == null)
                        {
                            var anyActorQuery = from actor in actors
                                                where actor.currentLocation == currentActor.currentLocation
                                                select actor;
                            var anyActorList = anyActorQuery.ToList();
                            healTarget = anyActorList[random.Next(0, anyActorList.Count)];
                        }

                        if (healTarget.currentLocation == currentActor.currentLocation)
                        {
                            currentActor.items.Remove(Item.Medkit);
                            healTarget.hp = 100;
                            Console.WriteLine($"{currentActor.name} heals {healTarget.name} with the medkit!");
                        }
                    }
                    break;
            }
        }
    }
}
