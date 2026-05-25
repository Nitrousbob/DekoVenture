namespace DekoVenture
{
    public static class InteractionHandler
    {

        private class InteractionContext
        {
            public Zone Zone { get; }
            public Player Player { get; }

            //added a convienience property to access currentlocation 
            //as most interactions happen there but we have zone for bigger things
            public Location CurrentLocation => Zone.CurrentLocation;

            public InteractionContext(Zone zone, Player player)
            {
                Zone = zone;
                Player = player;
            }
        }
        private interface IInteractionCommand
        {
            bool Execute();
        }
        private class WaitCommand : IInteractionCommand
        {
            public bool Execute()
            {
                UI.Narrate("You stand quietly, watching the area.");
                return true;
            }
        }

        private class InventoryCommand : IInteractionCommand
        {
            private readonly Player player;

            public InventoryCommand(Player player)
            {
                this.player = player;
            }

            public bool Execute()
            {
                player.ShowInventory();
                return true;
            }
        }
        public static bool InteractWith(Zone zone, Player player)
        {
            var targets = zone.CurrentLocation.Interactables;
            var exits = zone.CurrentLocation.Exits.ToList();

            UI.Narrate("\nYou see the following:");

            int optionNumber = 1;

            for (int i = 0; i < targets.Count; i++)
            {
                UI.ShowListNumber($"{optionNumber}.");
                ShowTargetOption(targets[i]);
                optionNumber++;
            }
            
            // List all available Exits dynamically
            for (int i = 0; i < exits.Count; i++)
            {
                UI.ShowListNumber($"{optionNumber}.");
                UI.ShowLocation($"Go {exits[i].Key} to {exits[i].Value.Name}\n");  //displays the direction token and the name.
                optionNumber++;
            }

            int waitOption = optionNumber++;  //this adds a wait option to let an npc finish their state before they are ready to interact
            //int exitOption = optionNumber; //  adds the final selection

            UI.Narrate($"{waitOption}. Wait a moment.");
            //these should be under a help or command help display
            //UI.AllMenuOption($"e(X)it Game, ");
            //UI.AllMenuOption($"(I)nventory ");

            while (true)
            {
                string input = TakeInput.GetString("Your selection adventurer: ").Trim().ToLower();
                if (input == "h" || input == "help")
                {
                    UI.ShowHelp("\n[ e(X)it, (I)nventory, (H)elp ]");
                    return true;
                }
                if (input == "x" || input == "exit")
                {
                    return false; // Signal to exit the game
                }
                if (input == "i" || input == "inventory")
                {
                    return new InventoryCommand(player).Execute();
                }

                if (int.TryParse(input, out int choice))
                {
                    if (choice == waitOption)
                    {
                        return new WaitCommand().Execute();
                    }
                    else if (choice >= 1 && choice <= targets.Count)
                    {
                        var target = targets[choice - 1];

                        if (target is CurrencyItem currency)
                        {
                            UI.ShowCurrencyItem($"You picked up the {currency.Name}.");
                            player.Gold += currency.Amount;
                            zone.CurrentLocation.Interactables.Remove(currency);
                        }
                        else if (target is Item item)
                        {
                            UI.AllMenuOption($"You picked up ");
                            UI.ShowMagnitude($"{item.Quantity}x ");
                            UI.ShowItem($"{item.Name}.");


                            var existing = player.Inventory.FirstOrDefault(i => i.Name == item.Name && i.isStackable);
                            if (existing != null)
                            {
                                existing.Quantity += item.Quantity;
                            }
                            else
                            {
                                player.Inventory.Add(item);
                            }

                            zone.CurrentLocation.Interactables.Remove(item);
                        }
                        else
                        {
                            target.OnInteract(player);
                            if (target is Enemy enemy && !enemy.IsAlive && !enemy.HasLoot())
                            {
                                zone.CurrentLocation.Interactables.Remove(enemy);
                                UI.EnemyDeath($"{enemy.Name}'s remains fade away.");
                            }
                        }
                        return true;
                    }
                    else if (choice > targets.Count && choice <= targets.Count + exits.Count) // They picked an Exit
                    {
                        int exitIndex = choice - 1 - targets.Count;
                        var selectedExit = exits[exitIndex];
                        UI.ShowNpcAction($"\nYou travel {selectedExit.Key}...");
                        zone.CurrentLocation = selectedExit.Value; // MOVEMENT HAPPENS HERE!
                        return true;
                    }
                }

                UI.ShowError("Invalid Choice.");
            }
        }

        //Shows the target options and colors them appropriately
        private static void ShowTargetOption(IInteractable optiontarget)
        {
            var target = optiontarget;
            string stateLabel = "";

            if (target is Actor actor && actor.StateMachine.CurrentState != null)
            {
                stateLabel = $" ({actor.StateMachine.CurrentState.Name})";
            }
            if (target is CurrencyItem currency) //currency item inherits from Item so I need it to find this branch
            {
                UI.ShowCurrencyItem($"{target.Name} ({currency.Amount})");
            }
            else if (target is Item)
            {
                UI.ShowItem($"{target.Name}");
            }
            else
            {
                UI.ShowNpc($"{target.Name}{stateLabel}");
            }
        }
    }



}
