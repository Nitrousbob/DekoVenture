# Things to Implement

## Todo: Weather

 make some weather for the environment, tell it how many turns it takes to change the time of day
 have the characters comment on changes in the weather on the turn that makes sense or the current weather.
 night time characters could be different than daytime characters along with animals.

## Todo: Refactor social commands using the new `IReactable` interface

<!-- 

- Create `IReactable` interface with methods like `OnSlapped()`, `OnLaughedAt()`, `OnFlirtedWith()`, and `OnFartedAt()`.
- Implement `IReactable` on `Npc.cs` and `Animal.cs` and give them their specific reactions.
- Update `Command.cs` to check `if (c.CurrentTarget is IReactable reactable)` to trigger the reactions polymorphically without if/else chains.

The design pattern we were using to replace those giant if/else chains is called Polymorphism, and it perfectly demonstrates one of the most famous rules in software engineering: the Open/Closed Principle.

Here is a quick refresher on what those mean:

Polymorphism: This is a fancy Greek word that means "many forms." In C#, it means we can treat different objects (like an Animal and an Npc) as the exact same type (an IReactable) when we interact with them. We don't care what they are, we just care that they know how to react.
The Open/Closed Principle: This is the "O" in the famous S.O.L.I.D. principles of object-oriented programming. It states that a class (like your Command class) should be Open for extension, but Closed for modification.
Closed: You shouldn't have to constantly open Command.cs and modify it with a new else if every time you add a new monster or character type to your game.
Open: You can easily extend your game with Goblins, Ghosts, or Dragons, and as long as you put IReactable on them, the Command class will magically know how to slap, laugh, or flirt with them without changing a single line of code in Command.cs!
Using interfaces this way is also heavily related to the Strategy Pattern, where you delegate the specific behavior (the "strategy" of how to react) to the objects themselves.

Whenever you are ready to shake off the rust from the break, we can tackle adding OnFartedAt() to your IReactable interface and cleaning up that last command!


You have hit the nail on the head! This is a fantastic observation and shows you are really starting to think like a software architect.

You are completely right: having your Npc, Animal, and State classes call Display.Action() directly is a violation of the Single Responsibility Principle (SRP).

Here is why: an Npc class should only be responsible for being an NPC—tracking its health, its cooldowns, and managing its state. It should not be responsible for updating the User Interface (the console).

If you ever decided to take this exact code and put it into a visual game engine like Unity or Godot, you would have to rewrite all of your Npc and Animal classes because Display.Igm() wouldn't work there. A domain class (like Npc) should not care how the game is displayed.

Where should the display responses go?
The display logic should stay as close to the "UI layer" as possible. In your current architecture, Command.cs acts as a bridge between the user's input and the game's logic, making it a much better place for display logic than the actor classes themselves.

Believe it or not, you actually already have a perfect example of this in your code! Take a look at your ITalkable interface and how Command.cs uses it:

The Good Example (Follows SRP): In Npc.cs:

csharp
public string GetTalkResponse()
{
    InteractionCount++;
    return "I came here to chew bubblegum and talk, and I'm all out of bubblegum.";
}
In Command.cs:

csharp
if (c.CurrentTarget is ITalkable talkable)
{
    Display.Action($"You talk to {talkable.Name}");
    Display.Igm(talkable.GetTalkResponse()); // The Command handles the Display!
}
Notice how Npc just returns a string? It doesn't know about Display.Igm. It just hands the data back to whoever asked for it.

How to apply this to your new IReactable interface
If you want to refactor IReactable to follow the Single Responsibility Principle, you would change the methods from returning void to returning string (or a custom object if you need to return multiple things).

Here is how you might tweak your plan:

1. Update the Interface:

csharp
public interface IReactable
{
    string OnSlapped();
    // ... other methods returning strings
}
2. Update the Npc/Animal Logic: The Npc still updates its internal state (like blocking interactions), but it returns the text instead of printing it.

csharp
public string OnSlapped()
{
    BlockInteraction(3); // State logic (belongs here)
    return $"{Name} looks at you with shock and anger."; // Return the result
}
3. Update Command.cs (The UI Layer):

csharp
public void Slap(List<Token> tokens, ComContext c)
{
    if (c.CurrentTarget is IReactable reactable)
    {
        Display.Action($"You slap {reactable.Name}");
        string reactionText = reactable.OnSlapped(); // Get the response
        Display.DarkAction(reactionText); // Print the response
    }
}
By passing data up the chain as strings, your Display class and your Command class become the only places in your entire application that actually care about drawing text to the screen.

You're doing great. Spotting architectural smells like this is one of the hardest parts of learning object-oriented programming!
*/ -->