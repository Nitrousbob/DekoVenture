using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace RPG_Asn4
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
                Console.Write("Invalid input. Please enter a number: ");
            }
        }

        //using this method for menu choices, i wrote this for another project but its useful for this one.
        public static int PromptIntInstant(string prompt, params int[] validChoices)
        {
            ConsoleKeyInfo keyInfo;
            int choice = -1;
            bool isValid = false;

            var singleDigitChoices = validChoices.Where(c => c >= 0 && c <= 9).ToArray();

            do
            {
                Console.Write(prompt);
                keyInfo = Console.ReadKey(true); //true hides the key press then we check if its valid
                if (int.TryParse(keyInfo.KeyChar.ToString(), out choice))
                {
                    if (validChoices.Contains(choice))
                    {
                        isValid = true;
                        Console.Write(keyInfo.KeyChar);  //display keypress after we know it was valid
                    }
                }
                if (!isValid)
                {
                    //Clr.ErrorMsg("Invalid selection.");
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
                Console.Write(prompt + " ");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (validChoices.Contains(value))
                    {
                        return value; // sucessful
                    }
                    else
                    {
                        string validOptions = string.Join(", ", validChoices);
                        Display.Error($"\nPlease select an option like: {validOptions}");
                    }
                }
                else
                {
                    //displays mostly when empty input, or space
                    Display.Error("\nUnavailable choice. Try choosing better.");
                }
            }
        }

        public static int PromptIntRange(string prompt, int min, int max)
        {
            int[] choices = Enumerable.Range(min, max - min + 1).ToArray();
            return PromptInt(prompt, false, choices);
        }

        public static string GetPlayerName()
        {
            Display.Igm("\nEnter your character's name: ");
            return Console.ReadLine() ?? "Unnamed Hero";
        }

    }
}
