namespace MonsterArena
{
    class ArenaMaster
    {
        static bool isCreated;
        static bool isAttackAllowed;

        private ArenaMaster() { }

        public static ArenaMaster CreateSingleton()
        {
            if (isCreated)
            {
                return null;
            }
            isCreated = true;
            return new ArenaMaster();
        }

        public static bool IsAttackAllowed()
        {
            return isAttackAllowed;
        }

        public void AllowAttack(bool value)
        {
            isAttackAllowed = value;
        }
    }
}
