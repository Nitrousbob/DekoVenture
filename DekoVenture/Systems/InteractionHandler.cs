﻿namespace DekoVenture
{
    public static class InteractionHandler
    {
        public static bool InteractWith(Zone zone, Player player)
        {
            var targets = zone.CurrentLocation.Interactables;
            var exits = zone.CurrentLocation.Exits.ToList();

            UI.Narrate("\n<LGr>You see the following:</LGr>");

            //start with menu option number 1
            int optionNumber = 1;

            // List all available targets dynamically
            for (int i = 0; i < targets.Count; i++)
            {
                UI.ShowListNumber($"{optionNumber}. ");
                ShowTargetOption(targets[i]);
                optionNumber++;
            }
            
            // List all available Exits dynamically
            for (int i = 0; i < exits.Count; i++)
            {
                UI.ShowListNumber($"{optionNumber}. ");
                UI.ShowLocationOption($"Go {exits[i].Key} to {exits[i].Value.Name}\n");  //displays the direction token and the name.
                optionNumber++;
            }

            int waitOption = optionNumber++;  //this adds a wait option to let an npc finish their state before they are ready to interact
            //int exitOption = optionNumber; //  adds the final selection

            UI.Narrate($"{waitOption}.Wait a moment.");
            
            while (true)
            {
                string input = TakeInput.GetString("You selection: ").Trim().ToLower();
                string [] inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (inputParts.Length == 0) continue;
                string command = inputParts[0];

                // --- UNIVERSAL PARSER INTEGRATION ---
                var ast = new Tokenizer().Tokenize(input);
                if (ast != null && ast.Count > 0)
                {
                    var verb = ast[0];
                    if (verb.Name == TokenType.verb && DialogFactory.lookupTable.ContainsKey(verb.Value))
                    {
                        ComContext comCtx = new ComContext(player, null, zone);
                        DialogFactory.lookupTable[verb.Value](ast, comCtx);
                        
                        if (comCtx.EndInteration) return false; // Exit game signal
                        return true;
                    }
                }

                //Handle "verb" + number commands (e.g. inspect 1 or use 2)
                if (inputParts.Length > 1 && int.TryParse(inputParts[1], out int targetNum))
                {
                    if(targetNum >= 1 && targetNum <= targets.Count)
                    {
                        var target = targets[targetNum - 1];
                        if(command == "inspect" || command == "look" || command == "x")
                        {
                            if(target is Item i) UI.Narrate($"\n<LB>Inspect:</LB> {i.GetDescription()}\n");
                            else UI.Narrate($"\nYou look closely at {target.Name}.");                        
                            return true;
                        }
                        if (command == "use")
                        {
                            if (target is Item i && i.Use(player))
                            {
                                i.Quantity--;
                                if(i.Quantity <= 0) zone.CurrentLocation.Interactables.Remove(i);
                            }
                            return true;
                        }
                    }
                }
                if (int.TryParse(input, out int choice))
                {
                    if (choice == waitOption)
                    {
                        DialogFactory.lookupTable["wait"](new List<Token>(), new ComContext(player, null, zone));
                        return true;
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
                            //default action for items : pick them up
                            UI.ShowItem($"You picked up <P>{item.Quantity}x</P> <Y>{item.Name}</Y>.");
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
                UI.ShowCurrencyItem($"{target.Name} (<LG>{currency.Amount}</LG>)");
            }
            else if (target is Item)
            {
                UI.ShowItem($"{target.Name}");
            }
            else
            {
                UI.ShowNpc($"{target.Name}<DB>{stateLabel}</DB>");
            }
        }
    }



}
