namespace DekoVenture
{
    public static class TakeInput
    {
        public static int GetMenuChoice()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    return choice;
                }
                UI.ShowError("Invalid input. Please enter a number: ");
            }
        }

        //using this method for menu choices, i wrote this for another project but its useful for this one.
        public static int PromptIntInstant(string prompt, params int[] validChoices)
        {
            //I added this if statement to allow for testing and coding in vs code, since Console.ReadKey doesn't
            //work well in that environment. It will just fallback to the confirmed method if input is
            //redirected.  I used chatGPT to help with this fix because i could not get the program to run.
            if (Console.IsInputRedirected)

            {
                return PromptIntConfirmed(prompt, validChoices);
            }

            ConsoleKeyInfo keyInfo;
            int choice = -1;
            bool isValid = false;

            var singleDigitChoices = validChoices.Where(c => c >= 0 && c <= 9).ToArray();

            do
            {
                UI.ShowText(prompt);
                keyInfo = Console.ReadKey(true); //true hides the key press then we check if its valid
                if (int.TryParse(keyInfo.KeyChar.ToString(), out choice))
                {
                    if (validChoices.Contains(choice))
                    {
                        isValid = true;
                        UI.ShowText(keyInfo.KeyChar.ToString());  //display keypress after we know it was valid
                    }
                }
                if (!isValid)
                {
                    UI.ShowError("Invalid selection.");
                }
            } while (!isValid);
            return choice;
        }

        public static int PromptInt(string prompt, bool requireConfirm, params int[] validChoices)
        {
            if (requireConfirm == false)
            {
                return PromptIntInstant(prompt, validChoices);
            }
            else
            {
                return PromptIntConfirmed(prompt, validChoices);
            }
        }
        public static int PromptIntConfirmed(string prompt, params int[] validChoices)
        {
            while (true)
            {
                UI.ShowText(prompt + " ");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (validChoices.Contains(value))
                    {
                        return value; // sucessful
                    }
                    else
                    {
                        string validOptions = string.Join(", ", validChoices);
                        UI.ShowError($"Please select an option like: {validOptions}");
                    }
                }
                else
                {
                    //displays mostly when empty input, or space
                    UI.ShowError("Unavailable choice. Try choosing better.");
                }
            }
        }

        public static int PromptIntRange(string prompt, int min, int max)
        {
            while (true)
            {
                UI.ShowText(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value >= min && value <= max)
                    {
                        return value; // successful input
                    }
                }
                UI.ShowError($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }

        public static string GetPlayerName()
        {
            UI.Narrate("Enter your character's name: ");
            return Console.ReadLine() ?? "Unnamed Hero";
        }

        //Upon a code review from Gemini, this function helps decouple my TakeInput class to handle basic string inputs no matter where they come from
        public static string GetString(string prompt)
        {
            UI.Narrate(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

    }
}
