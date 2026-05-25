##This file helps me to remember how the game works.  //some summarzied by AI for conciseness

Here is how the data flows in the game:

Game.cs holds the Player object and the CurrentZone.
Zone.cs holds the CurrentLocation.
Location.cs holds the NPCs, Items, and Exits (in the Interactables list and Exits dictionary).
Because this is a single-player game, you don't need to put the Player inside the Location.Interactables list. Instead, when the game runs, Game.cs grabs the Player and tosses it into the CurrentZone's methods (like CurrentZone.Describe(player)).

This is a great example of the Single Responsibility Principle at work. The Location is strictly responsible for knowing what is inside the room, and the Zone/Game is responsible for tracking the player's whereabouts!