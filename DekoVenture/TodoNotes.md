# Things to Implement

 //night time characters could be different than daytime characters along with animals.

 ## More than one Hardcoded Scene, where are they, in front of a shop, a path?
 
 ## Add a way for NPCs to talk about their current location and what they're doing there.

 ## Continue to refine the SRP and Proper Design Patterns, Preferring Composition over Inheritance
 // Note: While composition is generally preferred, use Inheritance for simple "is-a" relationships and specialized behavior overrides (e.g., SecretMiser is simply a more specific version of a SecretNpc).

 ## The NPCs dont talk about the time of day, the time of day may be important to the story, they dont have to , but sometimes it should happen
 // if the player needs to do something time specific, the NPCs should talk about it, if they dont, then the player may miss out on important information

 ## Implement a quest system, where the player can receive quests from NPCs and complete them for rewards. This will add more depth and variety to the gameplay, as well as give the player a sense of purpose and direction.

 ## Add a combat system, allowing the player to engage in battles with enemies. This could include turn-based combat, real-time combat, or a combination of both. The combat system should be engaging and strategic, requiring the player to make thoughtful decisions in order to succeed.
 //Attacking does not stay in combat, you attack and then it just says what would you like to do, i think the combat state should be held unless the npc is not combative/passive
//maybe need a thing to decide whether or not an npc would attack like an enemy or not, so enemies can attack upon encounter and maybe drunk npc's in a bar.

 // Combat Note: Ensure the HealthManager and Status Effect systems are heavily integrated into the combat loop so battles are dynamic and interesting. Need to create actual Enemy classes.

 ## Game Theme & Architecture: Shift the game's flavor towards a "Retro Futurism" style. Keep the core class naming conventions (Actor, Zone, Item, etc.) flexible and generic so the engine can support any setting!

 ## World Building: Author the new map based on the paper scaffold using the existing WorldBuilder and Factory systems.

 ## Implement a leveling system, where the player can gain experience points and level up their character. This will allow the player to improve their stats and abilities, making them stronger and more capable as they progress through the game.

 ## Add a skill tree, allowing the player to choose and upgrade their character's skills and abilities. This will give the player more customization options and allow them to tailor their character to their playstyle.

 ## implement a trap system, poison, physical, magical, etc. The player should be able to detect and disarm traps, or suffer the consequences if they fail.  --Status Effects have Been Implemented

 //this can also be used in the environment and with beasts, such as a poisonous plant or a bleeding wound that needs to be treated.

 ## Rooms

//Port the game to UNITY to have a graphical version

//prioritize moving your Rooms, Items, and NPCs into JSON files so WorldBuilder parses files rather than instantiating hardcoded new Location(...) objects.
 <!-- treasure
// traps
//a containment trap could drop and you could break your way out of it if you are strong enough using the IDestructable, example.
// items - interactable windows, plants desks portals
