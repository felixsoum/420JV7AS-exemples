namespace GoblinTribe
{
    public class Goblin : Monster
    {
        public static string Tribe { get; set; } = "Hob";
        public int Number { get; private set; }
        static int count = 1;

        public Goblin()
        {
            Number = count++;
        }

        public static void Reset()
        {
            count = 1;
            Tribe = "Hob";
        }

        public override string ToString()
        {
            return $"Goblin #{Number} from the {Tribe} tribe.";
        }
    }
}
