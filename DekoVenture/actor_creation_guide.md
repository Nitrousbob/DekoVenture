# DekoVenture Actor Creation Guide

## Purpose

This note is a focused reference for creating new **Actors** in DekoVenture.

The goal is to answer:

> “I want to add another character. What base class should I use, what interfaces matter, and what do I need to fill in?”

This is intentionally smaller than the full game-flow diagram. It focuses on the **Actor creation decision process**.

---

# 1. Mental Model

In DekoVenture, an **Actor** is the shared base for things in the world that behave like living or damageable entities.

Current Actor-style types include:

- `Player`
- `Npc`
- `Animal`
- `Enemy`
- special subclasses like `SecretNpc`, `SecretMiser`, and `SecretAnimal`
- object-like actors such as doors if they need health, states, or damage behavior

The simple rule:

> **Use `Actor` when the thing needs a name, health/vitals, alive/dead state, damage/healing, equipment, interaction cooldowns, or a state machine.**

---

# 2. What `Actor` Gives You

When a class extends `Actor`, it gets the common character foundation.

## Actor core data

| Feature | Meaning |
|---|---|
| `Name` | The actor’s display name |
| `Vitals` | A `HealthManager` object |
| `Health` | Wrapper around current health |
| `IsAlive` | Convenience check for `Health > 0` |
| `Gold` | Money held by the actor |
| `interactionCooldown` | Temporarily prevents interaction |
| `canInteract` | True when cooldown is finished |
| `StateMachine` | Allows idle/talking/combat/etc. behavior |
| `EquippedWeapon` | Optional weapon |
| `EquippedArmor` | Optional armor |

## Actor core behavior

| Method | Purpose |
|---|---|
| `TakeDamage(int amount)` | Reduces health, using armor if equipped |
| `Heal(int amount)` | Restores health through `HealthManager` |
| `BlockInteraction(int turns)` | Makes the actor unavailable for a few turns |
| `TickInteractionCooldown()` | Counts cooldown down |
| `OnInteract(Player player)` | Abstract method that subclasses must define |

---

# 3. Main Actor Type Choices

## Quick decision table

| What you are making | Start with |
|---|---|
| A normal person the player can talk to | `Npc` |
| A person with a secret | `SecretNpc` |
| A person who requires an item before revealing a secret | `SecretMiser` |
| An animal the player can inspect/pet/react to | `Animal` |
| An animal that reveals something special | `SecretAnimal` |
| A hostile creature or monster | `Enemy` |
| A new custom thing with health/states/damage | Create a new class that extends `Actor` |

---

# 4. Interface Option Matrix

Interfaces decide what commands work on the actor.

Think of them as “capability switches.”

## Interaction interfaces

| Interface | Unlocks | You must provide |
|---|---|---|
| `IInspectable` | `look` command | `Name`, `GetDescription()` |
| `ITalkable` | `talk` command | `Name`, `GetTalkResponse()` |
| `IQuestionable` | basic `ask` command | `Name`, `GetQuestionResponse()` |
| `ISecretKeeper` | secret/item-gated `ask` or `give` behavior | `TryRevealSecret(Player player, Item? offeredItem = null)` |
| `IReactable` | social reactions like slap/laugh/flirt/fart | `OnAgitate()`, `OnLaughedAt()`, `OnFlirtedWith()`, `OnFartedAt()` |
| `IPettable` | `pet` command | `Name`, `GetPetResponse()` |

## Combat and damage interfaces

| Interface | Unlocks | You must provide |
|---|---|---|
| `IDamageable` | basic damage/healing behavior | `TakeDamage(int damage)`, `Heal(int amount)` |
| `IDestructible` | can be attacked/damaged as a target | `IsAlive`, `TakeDamage(int damage)` |
| `IAttackable` | can fight back | `EnterCombat(Player player)` |
| `IWeapon` | can be equipped as a weapon | `Name`, `MinDamage`, `MaxDamage`, `GetDamage()` |
| `IArmor` | can reduce incoming damage | `Name`, `DefenseValue`, `CalculateDamageReduction(int incomingDamage)` |

---

# 5. Current Actor Presets

## `Npc`

Use this for most people.

Current `Npc` already includes:

- `Actor`
- `IInspectable`
- `ITalkable`
- `IQuestionable`
- `IReactable`
- `IAttackable`

That means a normal `Npc` can usually be:

- looked at
- talked to
- asked questions
- socially reacted to
- attacked
- moved into combat

### Basic example

```csharp
Npc guard = new Npc("Gate Guard", 15, true)
{
    IdleAction = "stands watch near the gate.",
    BusyStart = "checks the latch on the old gate.",
    BusyAction = "leans close, listening for movement beyond the wall.",
    BusyEnd = "steps back and rests a hand on his belt.",
    BusyRefusal = "is too focused on guard duty to talk right now."
};
```

---

## `SecretNpc`

Use this when the NPC can reveal a secret.

```csharp
SecretNpc snitch = new SecretNpc(
    "Da Snitch",
    5,
    "That guy over there did it."
);
```

Use this when the secret can be revealed without requiring a special item.

---

## `SecretMiser`

Use this when the NPC will only reveal a secret after receiving a specific item.

```csharp
SecretMiser miser = new SecretMiser(
    "William Buffalo",
    15,
    "There is a hidden trapdoor beneath the roots of the oldest tree in the Dark Woods.",
    "Lotion"
)
{
    IdleAction = "rubs his dry, cracked hands together",
    BusyStart = "pulls out an empty lotion bottle.",
    BusyAction = "tries to squeeze a final drop of lotion from the bottle.",
    BusyEnd = "sighs and puts the empty bottle away.",
    BusyRefusal = "is too busy lamenting his dry skin to talk."
};
```

Use this when you want a small quest-like interaction:

> give item → receive secret

---

## `Animal`

Use this when the actor is an animal instead of a talking person.

Current `Animal` includes:

- `Actor`
- `IInspectable`
- `IPettable`
- `IReactable`

That means it can be:

- looked at
- petted
- socially reacted to

It does **not** use the same talk/ask behavior as `Npc`.

```csharp
Animal fox = new Animal("Thistlepaw", 7, "Fox", true);
```

---

## `SecretAnimal`

Use this when petting the animal can reveal a hidden item.

```csharp
SecretAnimal dog = new SecretAnimal(
    "Precious",
    10,
    "Dog",
    true,
    ItemFactory.CreateGoldCoin(10)
);
```

Use this for behavior like:

> pet animal → animal reveals hidden item

---

## `Enemy`

Use this for a hostile character.

Current `Enemy` includes:

- `Actor`
- `IAttackable`
- `IInspectable`
- loot behavior

```csharp
Enemy ghoul = new Enemy("Cabin ghoul", 5)
{
    GoldReward = 4,
    LootItems = new List<Item>
    {
        ItemFactory.CreateHealthDrink(1)
    }
};
```

Use this when the actor should:

- be hostile
- be attackable
- fight back
- drop loot after defeat

---

# 6. Text Flow: Creating a New Actor

Use this as the plain-English flow.

## Step 1: Decide if this thing should be an Actor

Ask:

- Does it have health?
- Can it take damage?
- Can it die or be destroyed?
- Does it need a state machine?
- Does it need to interact like a character?
- Does it need equipment, gold, or status effects?

If yes, use `Actor` or one of its existing subclasses.

If no, it may be better as an `Item`, plain `IInteractable`, or another simpler class.

---

## Step 2: Pick the closest existing Actor type

Ask:

```text
Is it a person?
    Use Npc.

Is it a person with a secret?
    Use SecretNpc.

Is it a person who requires an item for a secret?
    Use SecretMiser.

Is it an animal?
    Use Animal.

Is it an animal with a hidden reward?
    Use SecretAnimal.

Is it hostile by default?
    Use Enemy.

Is it something totally new?
    Create a new class that extends Actor.
```

---

## Step 3: Decide what commands should work

Use interfaces as command switches.

```text
Should the player be able to look at it?
    Add IInspectable.

Should the player be able to talk to it?
    Add ITalkable.

Should the player be able to ask it questions?
    Add IQuestionable.

Should it reveal secrets?
    Add ISecretKeeper.

Should it react to slap/laugh/flirt/fart?
    Add IReactable.

Should the player be able to pet it?
    Add IPettable.

Should it be damageable/attackable?
    Use IDestructible.

Should it fight back?
    Use IAttackable.
```

---

## Step 4: Fill in required constructor values

Most Actor subclasses need at least:

```text
name
health
```

Some need extra values:

```text
Npc:
    name
    health
    hasEyes

Animal:
    name
    health
    species
    hasEyes

SecretNpc:
    name
    health
    secret

SecretMiser:
    name
    health
    secret
    required item name

Enemy:
    name
    health
    optional gold reward
    optional loot items

SecretAnimal:
    name
    health
    species
    hasEyes
    hidden item
```

---

## Step 5: Fill in flavor and behavior text

For `Npc`, especially factory-created or authored NPCs, fill in behavior strings:

```text
IdleAction
BusyStart
BusyAction
BusyEnd
BusyRefusal
```

These help the state machine make the NPC feel alive.

Example:

```csharp
npc.IdleAction = "shifts their weight idly.";
npc.BusyStart = "pulls out a yo-yo.";
npc.BusyAction = "does the 'Walk the Dog' trick with their yo-yo.";
npc.BusyEnd = "puts the yo-yo away.";
npc.BusyRefusal = "is too focused on their yo-yo to talk right now.";
```

---

## Step 6: Decide where it should be created

There are two main creation styles.

## Option A: Create directly in `WorldBuilder`

Use this for authored, special, hand-placed characters.

```csharp
Location darkWoods = new Location(
    "Dark Woods",
    "The trees are thick and ominous..."
);

SecretNpc snitch = new SecretNpc(
    "Da Snitch",
    5,
    "That guy over there did it."
);

darkWoods.Interactables.Add(snitch);
```

Use direct creation when the character is:

- story-specific
- location-specific
- hand-authored
- special or unique

---

## Option B: Add to a factory

Use this for reusable/random/generated characters.

Example concept:

```csharp
private static readonly Func<Npc>[] _tier1Townsfolk =
{
    () => new Npc(RandomNpcName(), Random.Shared.Next(10, 15), RollHasEyes(90)),
};
```

Use a factory when the character is:

- generic
- repeatable
- randomized
- part of a group
- something you want to spawn many times

---

## Step 7: Add the actor to a location

Most world characters become usable by being added to a location’s `Interactables`.

```csharp
clearing.Interactables.Add(npc);
darkWoods.Interactables.Add(animal);
smallCabin.Interactables.Add(enemy);
```

Once it is in `Interactables`, the player can select it through the interaction menu.

---

# 7. Actor Creation Checklist

Use this when adding a new Actor.

## Basic identity

- [ ] Name chosen
- [ ] Health value chosen
- [ ] Is this a person, animal, enemy, or custom actor?
- [ ] Picked base class: `Npc`, `Animal`, `Enemy`, or custom `Actor`

## Interaction behavior

- [ ] Can the player look at it?
- [ ] Can the player talk to it?
- [ ] Can the player ask it questions?
- [ ] Can the player give it an item?
- [ ] Can the player pet it?
- [ ] Can the player socially react to it?
- [ ] Can the player attack it?
- [ ] Can it fight back?

## Interface selection

- [ ] `IInspectable`
- [ ] `ITalkable`
- [ ] `IQuestionable`
- [ ] `ISecretKeeper`
- [ ] `IReactable`
- [ ] `IPettable`
- [ ] `IDestructible`
- [ ] `IAttackable`

## Actor details

- [ ] Description written
- [ ] Talk response written
- [ ] Question response written
- [ ] Reaction responses written
- [ ] Secret written, if needed
- [ ] Required item chosen, if needed
- [ ] Pet response written, if needed
- [ ] Combat behavior chosen, if needed
- [ ] Loot chosen, if needed
- [ ] Gold reward chosen, if needed

## State behavior

- [ ] Idle behavior
- [ ] Busy start behavior
- [ ] Busy action behavior
- [ ] Busy end behavior
- [ ] Busy refusal behavior
- [ ] Combat state behavior, if needed
- [ ] Special override behavior, if needed

## Placement

- [ ] Created directly in `WorldBuilder`, or
- [ ] Added to a factory recipe
- [ ] Added to `Location.Interactables`
- [ ] Tested with commands like `look`, `talk`, `ask`, `pet`, `hit`, `give`

---

# 8. Simple Text Flow Summary

```text
Start
  ↓
Do I need a living/damageable/interactable world character?
  ↓
Yes → use Actor or an Actor subclass
  ↓
Pick the closest existing type:
  - Person → Npc
  - Secret person → SecretNpc
  - Item-gated secret person → SecretMiser
  - Animal → Animal
  - Secret animal → SecretAnimal
  - Hostile creature → Enemy
  - Something new → custom class : Actor
  ↓
Choose command capabilities:
  - look → IInspectable
  - talk → ITalkable
  - ask → IQuestionable
  - secrets/give → ISecretKeeper
  - social reactions → IReactable
  - pet → IPettable
  - can be damaged → IDestructible
  - fights back → IAttackable
  ↓
Fill in required constructor data:
  - name
  - health
  - type-specific values
  ↓
Fill in flavor:
  - description
  - responses
  - idle/busy behavior
  - secret/loot/combat details
  ↓
Choose creation location:
  - one-off special character → WorldBuilder
  - reusable/random character → Factory
  ↓
Add to Location.Interactables
  ↓
Test commands
  ↓
Done
```

---

# 9. Practical Rule

When adding a new character, do **not** start by asking:

> “What class do I make?”

Start by asking:

> “What should the player be able to do to this thing?”

Then choose the interfaces from that answer.

The base class gives the body.

The interfaces give the player-facing abilities.

The factory or `WorldBuilder` decides how the actor enters the world.
