namespace RPG_Asn4
{
    internal class Program
    {
        //You will need to create a Game class and a Player class. Game will handle an instance of
        //Player.Game will provide a method to save the Player class to a json file. When the program
        //starts, it should prompt for a name. If that name already has a file it should, read it in
        //and print out all the values stored in Player.If it does not exist it should create the file
        //and then save it as it's own file. The program should then exit.

        static void Main(string[] args)
        {
            Game g = new Game();
            Display.ShowWelcomeMessage();
            while (true)
            {
                Display.ShowMenuChoices(new string[] { "Create Character", "Load Character", "Start Game", "Exit" });
                switch (TakeInput.PromptIntInstant("Please select an option: ", 1, 2, 3, 4))
                {
                    case 1:
                        g.CreatePlayer();
                        break;
                    case 2:
                        g.LoadPlayer();
                        break;
                    case 3:
                        g.StartGame();
                        break;
                    case 4:
                        Display.Igm("\nExiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
