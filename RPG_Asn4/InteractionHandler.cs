namespace RPG_Asn4
{
    public static class InteractionHandler
    {
        public static bool InteractWith(Scene scene, Player player)
        {
            var targets = scene.CurrentLocation.Interactables;
            var exits = scene.CurrentLocation.Exits.ToList();

            Display.Igm("You see the following:");

            int optionNumber = 1;

            for (int i = 0; i < targets.Count; i++)
            {
                string stateLabel = "";

                if (targets[i] is Actor actor && actor.StateMachine.CurrentState != null)
                {
                    stateLabel = $" ({actor.StateMachine.CurrentState.Name})";
                }
                Display.List($"{optionNumber}. {targets[i].Name}{stateLabel}");
                optionNumber++;
            }

            // List all available Exits dynamically
            for (int i = 0; i < exits.Count; i++)
            {
                Display.List($"{optionNumber}. Go {exits[i].Key} to {exits[i].Value.Name}");
                optionNumber++;
            }

            int waitOption = optionNumber++;  //this adds a wait option to let an npc finish their state before they are ready to interact
            int exitOption = optionNumber; //  adds the final selection
            Display.Igm($"{waitOption}. Wait a moment.");
            Display.Igm($"{exitOption}. Back to Main Menu.");
            int choice = TakeInput.PromptIntRange("Your selection adventurer: ", 1, exitOption);

            if (choice == exitOption)
            {
                Display.Igm("You step back from the interaction.");
                return false;
            }
            else if (choice == waitOption)
            {
                Display.Igm("You stand quietly, watching the area.");
                return true;
            }
            else if (choice <= targets.Count)
            {
                targets[choice - 1].OnInteract(player);
                return true;
            }
            else if (choice <= targets.Count + exits.Count) // They picked an Exit
            {
                int exitIndex = choice - 1 - targets.Count;
                var selectedExit = exits[exitIndex];
                Display.Action($"\nYou travel {selectedExit.Key}...");
                scene.CurrentLocation = selectedExit.Value; // MOVEMENT HAPPENS HERE!
                return true;
            }
            else
            {
                Display.Error("Invalid Choice.");
                return true;
            }
            
        }
    }
}
