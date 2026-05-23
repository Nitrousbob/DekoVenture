namespace DekoVenture
{
    public static class InteractionHandler
    {
        private interface IInteractionCommand
        {
            bool Execute();
        }
        public static bool InteractWith(Zone zone, Player player)
        {
            var targets = zone.CurrentLocation.Interactables;
            var exits = zone.CurrentLocation.Exits.ToList();

            UI.Narrate("You see the following:");

            int optionNumber = 1;

            for (int i = 0; i < targets.Count; i++)
            {
                string stateLabel = "";

                if (targets[i] is Actor actor && actor.StateMachine.CurrentState != null)
                {
                    stateLabel = $" ({actor.StateMachine.CurrentState.Name})";
                }
                UI.ShowListItem($"{optionNumber}. {targets[i].Name}{stateLabel}");
                optionNumber++;
            }

            // List all available Exits dynamically
            for (int i = 0; i < exits.Count; i++)
            {
                UI.ShowListItem($"{optionNumber}. Go {exits[i].Key} to {exits[i].Value.Name}");
                optionNumber++;
            }
            
            int waitOption = optionNumber++;  //this adds a wait option to let an npc finish their state before they are ready to interact
            int exitOption = optionNumber; //  adds the final selection
            
            UI.Narrate($"{waitOption}. Wait a moment.");
            UI.Narrate($"{exitOption}. Exit Game.");
            UI.Narrate($"(I)nventory ");

            while (true)
            {
                string input = TakeInput.GetString("Your selection adventurer: ").Trim().ToLower();

                if (input == "i" || input == "inventory")
                {
                    player.ShowInventory();
                    return true;
                }

                if (int.TryParse(input, out int choice))
                {
                    if (choice == exitOption)
                    {
                        UI.Narrate("You step back from the interaction.");
                        return false;
                    }
                    else if (choice == waitOption)
                    {
                        UI.Narrate("You stand quietly, watching the area.");
                        return true;
                    }
                    else if (choice >= 1 && choice <= targets.Count)
                    {
                        var target = targets[choice - 1];

                        if (target is CurrencyItem currency)
                        {
                            UI.ShowNpcAction($"You picked up the {currency.Name}.");
                            player.Gold += currency.Amount;
                            zone.CurrentLocation.Interactables.Remove(currency);
                        }
                        else if (target is Item item)
                        {
                            UI.ShowNpcAction($"You picked up {item.Quantity}x {item.Name}.");

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
    }
}
