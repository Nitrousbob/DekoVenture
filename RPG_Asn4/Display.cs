namespace RPG_Asn4
{
    public static class Display
    {
        public static void ShowWelcomeMessage()
        {
            Console.Title = "RPG Game";
            Console.WriteLine("Welcome to the RPG Game!");
        }

        public static void ShowMenuChoices(string[] choices)
        {
            Console.WriteLine();
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {choices[i]}");
            }
        }
                
        public static void List(string input)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public static void Igm(string input)  // Igm stands for "In Game Message"
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public static void ShowPlayerInfo(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Player Name: {player.Name}, Health: {player.Health}");
            Console.ResetColor();
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }
    }
}
