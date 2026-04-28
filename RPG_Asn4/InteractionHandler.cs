namespace RPG_Asn4
{
    public static class InteractionHandler
    {
        public static void HandleSelection(List<IInteractable> targets)
        {
            if (targets.Count == 0) return;

            int choice = TakeInput.GetMenuChoice();

            if (choice > 0 && choice <= targets.Count)
            {
                targets[choice - 1].OnInteract();
            }

        }

        public static void InteractWith(List<IInteractable> targets)
        {
            bool interacting = true;

            while (interacting)
            {
                if (targets.Count == 0)
                {
                    Display.Igm("There is nothing to interact with.");
                    return;
                }

                Display.Igm("You see the following:");

                for (int i = 0; i < targets.Count; i++)
                {
                    Display.List($"{i + 1}. {targets[i].Name}"); //This lists the interactable targets by their type (e.g., Npc, Item, etc.)
                }
                int exitOption = targets.Count + 1;
                Display.Igm($"{exitOption}. Exit Interaction");
                int choice = TakeInput.PromptIntRange("Select an option to interact with: ", 1, exitOption);

                if (choice == exitOption)
                {
                    interacting = false;
                    Display.Igm("You step back from the interaction.");
                }
                else if (choice > 0 && choice <= targets.Count)
                {
                    targets[choice - 1].OnInteract();
                }
                else
                {
                    Display.Error("Invalid Choice.");
                }
            }
        }
    }
}
