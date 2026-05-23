# DekoVenture Location Creation Guide

## Purpose

This note is a focused reference for creating new **Locations** in DekoVenture.

The goal is to answer:

> “I want to add another place to the world. What class do I use, what does a location contain, how do exits work, and how does the player actually move into it?”

This is intentionally smaller than the full game-flow diagram. It focuses on the **Location creation decision process**.

---

# 1. Mental Model

In DekoVenture, a **Location** is one place the player can currently stand in.

A `Location` is like a room, clearing, cabin, hallway, shop, cave, field, dock, or any other explorable area.

The simple rule:

> **Use `Location` when the player needs a named place with a description, interactable things, and exits to other places.**

A `Location` is not the whole world.

A `Zone` is the container that tracks the current location, weather, time of day, and turn updates.

So the basic relationship is:

```text
Zone
  owns CurrentLocation
    which is a Location
      which owns Interactables
      which owns Exits to other Locations
```

Another way to think about it:

```text
Zone = the current adventure area
Location = one node/place inside that area
Exits = connections between location nodes
Interactables = things sitting in that location
```

---

# 2. What `Location` Gives You

A `Location` is currently a simple container class.

## Location core data

| Feature | Meaning |
|---|---|
| `Name` | The location’s display name |
| `Description` | The location’s flavor/setting text |
| `Interactables` | Things the player can select or interact with in that location |
| `Exits` | A dictionary of movement commands pointing to destination locations |

## Location core behavior

| Method | Purpose |
|---|---|
| `Location(string name, string description)` | Creates a location with an empty interactable list and empty exit map |
| `AddExit(string command, Location destination)` | Adds a movement option from this location to another location |

The key thing is that `AddExit()` only adds an exit **from the current location to the destination**.

It does not automatically create the return path.

So this:

```csharp
clearing.AddExit("north", darkWoods);
```

means:

```text
Small Clearing --north--> Dark Woods
```

But the player cannot automatically go south unless you also add:

```csharp
darkWoods.AddExit("south", clearing);
```

---

# 3. Main Location Creation Choices

## Quick decision table

| What you are making | What to use |
|---|---|
| A normal place the player can visit | `new Location(name, description)` |
| A starting place | Create a `Location`, then pass it into `new Zone(...)` |
| A connected area | Create multiple `Location` objects and connect them with `AddExit()` |
| A place with NPCs/items/enemies | Add objects to `Location.Interactables` |
| A one-way path | Add one exit only |
| A two-way path | Add an exit on both locations |
| A locked/blocked path | Use an interactable like `Door`, or later add conditional exit logic |

---

# 4. Location vs Zone

## `Location`

A `Location` answers:

```text
Where am I standing?
What is this place called?
What can I see here?
What exits are available from here?
```

It owns:

```text
Name
Description
Interactables
Exits
```

## `Zone`

A `Zone` answers:

```text
What larger area am I in?
What location am I currently in?
What is the time of day?
What is the weather?
What updates each turn?
```

It owns:

```text
Name
CurrentLocation
CurrentWeather
CurrentTime
turn updates
```

## Practical rule

Use `Location` for the map nodes.

Use `Zone` to hold the active map state.

```text
Location = place
Zone = place manager
```

---

# 5. What Can Go Inside a Location?

A location stores objects in:

```csharp
public List<IInteractable> Interactables { get; set; }
```

That means anything added to a location must work as an `IInteractable`.

Current examples include:

- `Npc`
- `SecretNpc`
- `SecretMiser`
- `Animal`
- `SecretAnimal`
- `Enemy`
- `Item`
- `CurrencyItem`
- `Consumable`
- `Door`

The important idea:

> **If the player should see it in the location menu, add it to `Location.Interactables`.**

Example:

```csharp
clearing.Interactables.Add(HumanNpcFactory.GetStandardTier(1));
clearing.Interactables.Add(ItemFactory.CreateGoldCoin(5));
smallCabin.Interactables.Add(new Enemy("Cabin ghoul", 5));
```

---

# 6. How the Interaction Menu Uses a Location

The `InteractionHandler` looks at the current location and builds the menu from two things:

```text
1. CurrentLocation.Interactables
2. CurrentLocation.Exits
```

The menu lists interactables first.

Then it lists exits.

Then it adds extra options like wait, exit game, and inventory.

The logic is basically:

```text
Get current location
  ↓
List everything in Interactables
  ↓
List every exit in Exits
  ↓
Let player choose a number or inventory command
  ↓
If they choose an interactable:
    - CurrencyItem adds gold
    - Item goes into inventory
    - Other interactables call OnInteract(player)
  ↓
If they choose an exit:
    zone.CurrentLocation = selected destination
```

This is the main reason locations matter.

A location is not just flavor text. It is what drives the player’s visible options.

---

# 7. Basic Location Example

Here is the smallest useful location:

```csharp
Location clearing = new Location(
    "Small Clearing",
    "You stand in a quiet clearing surrounded by a dense forest."
);
```

At this point, the location exists, but it has:

```text
No interactables
No exits
```

So the player can stand there, but there is not much to do yet.

---

# 8. Adding Interactables

After creating the location, add things to it.

```csharp
clearing.Interactables.Add(ItemFactory.CreateGoldCoin(5));
clearing.Interactables.Add(ItemFactory.CreateHealthDrink(2));
clearing.Interactables.Add(HumanNpcFactory.GetStandardTier(1));
```

Now the player can see and select those objects from the interaction menu.

## Different interactable outcomes

| Type added | What happens when selected |
|---|---|
| `CurrencyItem` | Player gold increases, item is removed from location |
| `Item` | Item goes into player inventory, item is removed from location |
| `Npc` / `Animal` / `Enemy` / `Door` | `OnInteract(player)` runs |
| Dead `Enemy` with no loot | Enemy can be removed from the location |

---

# 9. Adding Exits

Exits connect one `Location` to another.

```csharp
clearing.AddExit("north", darkWoods);
darkWoods.AddExit("south", clearing);
```

This creates a two-way path:

```text
Small Clearing --north--> Dark Woods
Dark Woods --south--> Small Clearing
```

## One-way exits

This creates a one-way path:

```csharp
trapDoor.AddExit("down", basement);
```

That means the player can go down to the basement, but cannot return unless you add another exit:

```csharp
basement.AddExit("up", trapDoor);
```

## Important habit

Create both locations first.

Then connect them.

```csharp
Location clearing = new Location("Small Clearing", "A quiet clearing.");
Location darkWoods = new Location("Dark Woods", "The trees are thick and ominous.");

clearing.AddExit("north", darkWoods);
darkWoods.AddExit("south", clearing);
```

This helps avoid trying to connect to a destination that does not exist yet.

---

# 10. WorldBuilder’s Job

`WorldBuilder` is currently where authored locations are created and connected.

It is doing four main jobs:

```text
Create locations
  ↓
Connect exits
  ↓
Add interactables
  ↓
Return a Zone with a starting location
```

The current pattern looks like this:

```csharp
public static Zone CreateStartingZone()
{
    Location clearing = new Location(
        "Small Clearing",
        "You stand in a quiet clearing surrounded by a dense forest"
    );

    Location darkWoods = new Location(
        "Dark Woods",
        "The trees are thick and ominous, The light barely pierces the canopy"
    );

    clearing.AddExit("north", darkWoods);
    darkWoods.AddExit("south", clearing);

    clearing.Interactables.Add(HumanNpcFactory.GetStandardTier(1));
    clearing.Interactables.Add(ItemFactory.CreateGoldCoin(5));

    return new Zone("Starting Zone", clearing);
}
```

The return line is important:

```csharp
return new Zone("Starting Zone", clearing);
```

That tells the game:

```text
The zone starts with CurrentLocation = clearing
```

---

# 11. Text Flow: Creating a New Location

Use this as the plain-English flow.

## Step 1: Decide the purpose of the location

Ask:

```text
Is this a normal room/place?
Is this a transition area?
Is this a reward area?
Is this a danger/combat area?
Is this a puzzle/secret area?
Is this a shop/town/social area?
```

The purpose helps decide what interactables and exits it should have.

---

## Step 2: Create the Location object

Every location needs at least:

```text
name
description
```

Example:

```csharp
Location oldBridge = new Location(
    "Old Bridge",
    "A crooked wooden bridge stretches over a foggy ravine."
);
```

---

## Step 3: Decide what is inside it

Ask:

```text
Are there NPCs here?
Are there animals here?
Are there enemies here?
Are there items here?
Are there doors or objects here?
Are there secret-giving characters here?
```

Then add them to `Interactables`.

```csharp
oldBridge.Interactables.Add(new Enemy("Bridge goblin", 8));
oldBridge.Interactables.Add(ItemFactory.CreateGoldCoin(3));
```

---

## Step 4: Decide where the player can go from here

Ask:

```text
Can the player go north?
Can the player go south?
Can the player enter a building?
Can the player climb down?
Can the player board a ship?
Can the player return the way they came?
```

Then add exits.

```csharp
oldBridge.AddExit("east", watchTower);
watchTower.AddExit("west", oldBridge);
```

Remember:

> **Exits are one-way unless you add the return exit yourself.**

---

## Step 5: Add the location into the map structure

A location becomes reachable when another location points to it.

```csharp
darkWoods.AddExit("east", oldBridge);
oldBridge.AddExit("west", darkWoods);
```

If no other location has an exit to it, the location exists in code but the player cannot reach it through normal movement.

---

## Step 6: Decide if it should be the starting location

If this is the first place in the zone, pass it into the `Zone` constructor.

```csharp
return new Zone("Starting Zone", oldBridge);
```

If it is not the starting location, connect it with exits instead.

---

## Step 7: Test it in the interaction menu

When the player enters the location, verify:

```text
Does the location name display?
Do the interactables appear?
Do exits appear?
Can items be picked up?
Can NPCs be interacted with?
Can the player move to the connected locations?
Can the player return, if intended?
```

---

# 12. Location Creation Checklist

Use this when adding a new location.

## Basic identity

- [ ] Location name chosen
- [ ] Location description written
- [ ] Location purpose chosen
- [ ] Location variable name chosen

## Map connection

- [ ] Created the new `Location` object
- [ ] Created destination locations before adding exits
- [ ] Added exit from old location to new location
- [ ] Added return exit, if the path should be two-way
- [ ] Used clear movement command names like `north`, `south`, `enter cabin`, `down`, `up`
- [ ] Checked that `AddExit()` lowercases the command

## Interactables

- [ ] Added NPCs, if needed
- [ ] Added animals, if needed
- [ ] Added enemies, if needed
- [ ] Added items, if needed
- [ ] Added currency, if needed
- [ ] Added doors or special objects, if needed
- [ ] Confirmed everything added to `Interactables` is an `IInteractable`

## Gameplay behavior

- [ ] Does the location need danger?
- [ ] Does the location need treasure?
- [ ] Does the location need a secret?
- [ ] Does the location need a blocked/locked path?
- [ ] Does the location need a reason to revisit?
- [ ] Does the location connect logically to nearby places?

## Testing

- [ ] Location appears in menu after traveling there
- [ ] Interactables appear in the correct order
- [ ] Exits appear after interactables
- [ ] Selecting an exit changes `zone.CurrentLocation`
- [ ] Items can be picked up
- [ ] NPCs/animals/enemies run `OnInteract(player)`
- [ ] Wait option still advances the world
- [ ] Inventory option still works
- [ ] Dead enemies with no loot are removed correctly

---

# 13. Simple Text Flow Summary

```text
Start
  ↓
Do I need a new place the player can stand in?
  ↓
Yes → create a new Location
  ↓
Give it:
  - name
  - description
  ↓
Decide what belongs inside:
  - NPCs
  - animals
  - enemies
  - items
  - doors
  - secrets
  ↓
Add those things to Location.Interactables
  ↓
Decide how the player reaches it
  ↓
Use AddExit(command, destination)
  ↓
Should the player be able to go back?
  ↓
Yes → add a return exit on the destination location
No → leave it one-way
  ↓
If this is the first location:
  return new Zone("Zone Name", startingLocation)
  ↓
If this is not the first location:
  connect it from another reachable location
  ↓
Run the game
  ↓
Check the interaction menu:
  - interactables listed
  - exits listed
  - movement works
  - objects behave correctly
  ↓
Done
```

---

# 14. Practical Example: Adding a New Cabin Cellar

This example adds a new location connected to the `Small Cabin`.

```csharp
Location smallCabin = new Location(
    "Small Cabin",
    "It's a small house on an open field area."
);

Location cellar = new Location(
    "Dusty Cellar",
    "The air is cold and stale. Old shelves lean against damp stone walls."
);

smallCabin.AddExit("down", cellar);
cellar.AddExit("up", smallCabin);

cellar.Interactables.Add(new Enemy("Cellar rat king", 8)
{
    GoldReward = 2,
    LootItems = new List<Item>
    {
        ItemFactory.CreateHealthDrink(1)
    }
});

cellar.Interactables.Add(ItemFactory.CreateGoldCoin(3));
```

The player path would be:

```text
Small Cabin
  ↓ choose "Go down to Dusty Cellar"
Dusty Cellar
  ↓ interact with enemy/items
  ↓ choose "Go up to Small Cabin"
Small Cabin
```

---

# 15. Common Mistakes to Watch For

## Forgetting the return exit

```csharp
clearing.AddExit("north", darkWoods);
```

This does not automatically create:

```csharp
darkWoods.AddExit("south", clearing);
```

Add both if the path should be two-way.

---

## Creating a location but never connecting it

This creates the location:

```csharp
Location hiddenGrove = new Location("Hidden Grove", "A quiet secret place.");
```

But the player cannot reach it until another location points to it:

```csharp
darkWoods.AddExit("crawl through vines", hiddenGrove);
```

---

## Adding things that are not interactable

The list is:

```csharp
List<IInteractable> Interactables
```

So the object must support the interaction contract.

If it should appear in the menu, make sure it works as an `IInteractable`.

---

## Expecting all locations to update every turn

Current `Zone.TickTurn()` updates interactables in the **current location**.

So if an NPC is in another location, it is not currently being updated by the normal turn tick unless you later expand the system.

That is okay for now, but it matters if you add roaming NPCs or world-wide events later.

---

# 16. Practical Rule

When adding a location, do **not** start by asking:

> “Where do I put this code?”

Start by asking:

> “What should the player see, touch, fight, pick up, or travel to from this place?”

Then build the location from that answer.

The `Location` gives the place a name and contents.

The `Interactables` list gives the player things to do.

The `Exits` dictionary gives the player movement.

The `Zone` decides which location is currently active.

The `InteractionHandler` turns all of that into the menu the player sees.
