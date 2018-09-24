using System.Linq;
using System.Collections.Generic;


namespace MurderInvitation
{



    class DavidSurvivorController : ActorController
    {

        public static int counter = 0;
        public static int turns = 0;
        public static int priority1_target = 0;

        private static string Head_to_Kill = "";
        private static string Victim = "";
        private static string suspect = "";

        private static Location test;
        
        MurderInvitation.Location Head_to_Kill_location;
       
        private static List<string> Players = new List<string>();

        
        private static List<string> names = new List<string>();
        private static Dictionary<int, string> info = new Dictionary<int , string>();
        private static int[,] info_array = new int[5,1];
        private static int[,] info_array2 = new int[5, 1];
        private static int[,] target_array = new int[5, 1];





        public DavidSurvivorController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        {
            int target = 0;
           
            
            int alive_count = 0;
            



        var myQuery = from actor in gameData.actorDataList
                        where actor.Name == name
                        select actor;

            ActorData myData = myQuery.First();

            var actorsAliveQuery = from actor in gameData.actorDataList
                                   where actor.Hp > 0
                                   select actor;

            if (counter == 0)
            {
                for (int x = 0; x < 5; x++)
                {
                    info.Add(x, gameData.actorDataList[x].Name);
                    names.Add(gameData.actorDataList[x].Name);
                    info_array[x, 0] = gameData.actorDataList[x].Hp;
                }

            }
            if (counter % 2 == 0 && counter != 0)
            {
                turns += 2;
                for (int x = 0; x <5; x++)
                {
                    info_array2[x, 0] = gameData.actorDataList[x].Hp;
                }
               

            }
            else
            {
                for (int x = 0; x < 5; x++)
                {
                    info_array[x, 0] = gameData.actorDataList[x].Hp;
                }
            }
            for (int A = 0; A < 5; A++)
            {
                if (info_array[A, 0] - 50 == info_array2[A, 0] || info_array2[A, 0] - 50 == info_array[A, 0])
                {
                    Players.Add(gameData.actorDataList[A].Name);
                }
            }
            for (int count = 0; count < 5; count++)
            {
                if(gameData.actorDataList[count].Hp > 0 )
                                          
                {
                    alive_count++;
                }

            }

            priority1_target = 0;
            
            
                

                for (int Y = 0; Y < 5; Y++)
                {
                    target = 0;
                   
                    if (gameData.actorDataList[Y].Hp <= 0)
                    {
                        target -= 100;
                        if (Head_to_Kill == gameData.actorDataList[Y].Name)
                    {
                        priority1_target = 0;
                        Head_to_Kill = "";
                    }
                        
                    }

                    


                    if (counter%2 == 0)
                    {
                        
                            if (info_array[Y,0] == gameData.actorDataList[Y].Hp + 50)
                            {
                                Players.Add(gameData.actorDataList[Y].Name);
                                test = gameData.actorDataList[Y].CurrentLocation;
                                Victim = gameData.actorDataList[Y].Name;
                                target -= 10;
                            }
                            if (test == gameData.actorDataList[Y].CurrentLocation)
                            {
                                target += 3;
                                suspect = gameData.actorDataList[Y].Name;
                            }
                            if (suspect == gameData.actorDataList[Y].Name)
                            {
                            target += 5;
                            }
                            

                                
                            
                        
                    }
                    if (counter % 2 !=0)
                    {
                            if (info_array2[Y, 0] == gameData.actorDataList[Y].Hp + 50)
                            {
                                Players.Add(gameData.actorDataList[Y].Name);
                                
                                Victim = gameData.actorDataList[Y].Name;
                                target -= 10;




                            }
                            if (test == gameData.actorDataList[Y].CurrentLocation)
                            {
                                target += 2;
                                if (gameData.actorDataList[Y].Name == suspect && suspect != Victim)
                                {
                                    target += 15;

                                }


                            }
                        
                         
                    }



                    if (gameData.actorDataList[Y].Hp <= 50)
                    {
                        target -= 2;
                    }
                    
                        target_array[Y, 0] =+ target;
                    
                    if (target_array[Y, 0] > priority1_target)
                    {



                            
                            priority1_target = target_array[Y, 0];
                        if (gameData.actorDataList[Y].Name != "David" && !Players.Contains(gameData.actorDataList[Y].Name))
                        {
                            
                            Head_to_Kill = gameData.actorDataList[Y].Name;
                            Head_to_Kill_location = gameData.actorDataList[Y].CurrentLocation;
                        }
                        
                       

                        

                    }




                    //System.Diagnostics.Debugger.Break();






                }
            
            //System.Diagnostics.Debugger.Break();
            if (myData.Hp <= 50 && myData.Items.Contains(Item.Medkit))
            {
                return new GameMove(myData.CurrentLocation, GameAction.UseMedkit, myData.Name, "I feel better.");
            }

            if (alive_count <3 && !myData.Items.Contains(Item.Gun))
            {
                if(gameData.generatorHp > 0 || gameData.gateHp > 25)
                {
                    if (gameData.isGunTaken == true)
                    {
                        return new GameMove(Head_to_Kill_location, GameAction.NormalAttack, myData.Name, "shit");
                    }
                }
                if (gameData.generatorHp > 0 || gameData.gateHp > 25)
                {
                    if (gameData.isGunTaken == false)
                    {
                        if (gameData.isSafeUnlocked == false)
                        {
                            return new GameMove(Location.Armory, GameAction.UnlockSafe, myData.Name, "shit");
                        }
                        if (gameData.isSafeUnlocked == true)
                        {
                            return new GameMove(Location.Armory, GameAction.TakeGun, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                        }
                    }
                }
            }
            
            ////logic
            if (myData.Hp <= 50 && myData.Items.Contains(Item.Medkit))
            {
                return new GameMove(myData.CurrentLocation, GameAction.UseMedkit, myData.Name, "I feel better.");
            }
            else
            {
                ///turn 1///////////////////////
                if (counter == 0)
                {
                    counter++;
                    return new GameMove(Location.Bathroom, GameAction.TakeMedkit, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                }
                ///turn 2///////////////////////
                if (counter == 1)
                {
                    counter++;
                    if (gameData.isSafeUnlocked == true && gameData.isGunTaken == false)
                    {
                        return new GameMove(Location.Armory, GameAction.TakeGun, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");

                    }
                    else
                    {
                        return new GameMove(Location.Basement, GameAction.RepairGenerator, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                    }
                }
                ///turn 3///////////////////////
                if (counter == 2)
                {
                    counter++;
                    if (gameData.isSafeUnlocked == true && gameData.isGunTaken == false)
                    {
                        return new GameMove(Location.Armory, GameAction.TakeGun, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");

                    }
                    if (myData.Items.Contains(Item.Gun))
                    {
                        if (gameData.generatorHp > 0)
                        {
                            return new GameMove(Location.Basement, GameAction.NormalAttack, Head_to_Kill, "powpow");
                        }
                        else
                        {
                            return new GameMove(Location.Exit, GameAction.NormalAttack, Head_to_Kill, "powpow");
                        }

                    }
                    else
                    {
                        if (gameData.generatorHp > 0)
                        {
                            return new GameMove(Location.Basement, GameAction.RepairGenerator, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                        }
                    }
                }
                ///turn 4///////////////////////
                if (counter == 3)
                {
                    counter++;
                    if (gameData.isSafeUnlocked == true && myData.Items.Contains(Item.Gun) == false && gameData.isGunTaken == false)
                    {
                        return new GameMove(Location.Armory, GameAction.TakeGun, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");

                    }
                    if (myData.Items.Contains(Item.Gun))
                    {
                        if (gameData.generatorHp > 0)
                        {
                            return new GameMove(Location.Basement, GameAction.NormalAttack, Head_to_Kill, "powpow");
                        }
                        else
                        {
                            return new GameMove(Location.Exit, GameAction.NormalAttack, Head_to_Kill, "powpow");
                        }

                    }
                    else
                    {
                        if (gameData.generatorHp != 0)
                        {
                            return new GameMove(Location.Basement, GameAction.RepairGenerator, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                        }
                        if (gameData.generatorHp == 0)
                        {
                            return new GameMove(Location.Exit, GameAction.RepairGate, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                        }
                    }
                }
                ///turn 5 & + ///////////////////////
                if (counter >= 4)
                {
                    counter++;
                    if (gameData.isSafeUnlocked == true && myData.Items.Contains(Item.Gun) == false && gameData.isGunTaken ==false)
                    {
                        return new GameMove(Location.Armory, GameAction.TakeGun, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");

                    }
                    if (myData.Items.Contains(Item.Gun))
                    {
                        if (gameData.generatorHp > 0)
                        {
                            return new GameMove(Location.Basement, GameAction.NormalAttack, Head_to_Kill, "powpow");
                        }
                        else
                        {
                            return new GameMove(Location.Exit, GameAction.NormalAttack, Head_to_Kill, "powpow");
                        }

                    }
                    else
                    {
                        if (gameData.generatorHp != 0)
                        {
                            return new GameMove(Location.Basement, GameAction.RepairGenerator, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                        }
                        if (gameData.generatorHp == 0)
                        {
                            return new GameMove(Location.Exit, GameAction.RepairGate, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
                        }
                    }
                }
            }


            
            return new GameMove(Location.Bathroom, GameAction.TakeMedkit, myData.Name, "repair gen!!!!!!!!!!!!!!!!!!");
        }
    }

    class DavidKillerController : ActorController
    {
        public DavidKillerController(string name) : base(name)
        {
        }

        public override GameMove GenerateMove(GameData gameData)
        { 
            if (gameData.generatorHp != 0)
            {
                return new GameMove(Location.Basement, GameAction.NormalAttack, "", "repair gen!!!!!!!!!!!!!!!!!!");
            }
            else
            {
                return new GameMove(Location.Exit, GameAction.NormalAttack, "", "repair gen!!!!!!!!!!!!!!!!!!");
            }
        }
    }

    class DavidControllerFactory : ActorControllerFactory
    {
        public DavidControllerFactory(string name) : base(name)
        {
        }

        public override ActorController CreateSurvivorController()
        {
            return new DavidSurvivorController(name);
        }

        public override ActorController CreateKillerController()
        {
            return new DavidKillerController(name);
        }
    }
    
}
