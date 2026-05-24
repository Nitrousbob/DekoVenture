namespace DekoVenture
{
    public class SaveData
    {
        public int SaveVersion { get; set; } = 1;
        //public PlayerSaveData Player {get; set;}


        public void Migrate()
        {
            if (SaveVersion < 2)
            {
                //apply version 2 fixes
                SaveVersion = 2;
            }

            if (SaveVersion < 3)
            {
                //apply version 3 fixes
                SaveVersion = 3;
            }

        }
    }
}