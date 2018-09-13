using System;

namespace MurderInvitation
{
    public enum Item { Gun, Medkit };

    public enum Location { Armory, Bathroom, Basement, Exit }

    public enum GameAction
    {
        Nothing,
        Attack,
        RepairGenerator,
        RepairGate,
        UnlockSafe,
        TakeGun,
        TakeMedkit,
        UseMedkit
    }

    class GameMove
    {
        public Actor actor; // Actor doing the move
        public Location nextLocation;
        public GameAction gameAction;
        public string actionTarget;
        static Random random = new Random();

        public GameMove(Location nextLocation, GameAction gameAction, string actionTarget = "")
        {
            this.nextLocation = nextLocation;
            this.gameAction = gameAction;
            this.actionTarget = actionTarget;
        }

        public static Location GetRandomLocation()
        {
            return (Location) random.Next(0, Enum.GetValues(typeof(Location)).Length);
        }

        public static GameAction GetRandomAction()
        {
            return (GameAction)random.Next(0, Enum.GetValues(typeof(GameAction)).Length);
        }
    }
}
