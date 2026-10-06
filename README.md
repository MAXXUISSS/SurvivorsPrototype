# Vampire Survivors-like — Unity & C#

A 2D action game inspired by the **Vampire Survivors** genre, developed in **Unity and C#** as a personal portfolio and learning project.

The project focuses on building a complete gameplay loop while applying **Object-Oriented Programming, SOLID principles, composition, interfaces, events, ScriptableObjects, factories and object pooling** where they provide real value.

The goal is **not to recreate Vampire Survivors**, but to use the genre as a practical framework for developing gameplay systems, improving C# and Unity skills, and demonstrating clean and maintainable code.

---

## 🎮 Project Overview

The core gameplay is based around surviving against progressively increasing enemy pressure, defeating enemies, collecting experience, leveling up and choosing upgrades.

The main gameplay loop is:

```text
Move
  ↓
Fight enemies
  ↓
Defeat enemies
  ↓
Collect experience
  ↓
Level up
  ↓
Choose an upgrade
  ↓
Improve the player
  ↓
Continue playing
```

The project is being developed incrementally, prioritizing **functional gameplay first** and introducing additional architecture only when it solves an actual problem.

---

## 🛠️ Technologies

- **Unity**
- **C#**
- **Unity 2D Physics**
- **C# Events**
- **ScriptableObjects**
- **Object-Oriented Programming**
- **Composition**
- **Inheritance & Polymorphism**
- **Interfaces**
- **Factory Pattern**
- **Object Pooling**
- **Git / GitHub**

---

# 🧱 Architecture

The project is structured around independent gameplay responsibilities instead of placing the entire game inside a single manager or player class.

The current architecture follows a simple principle:

> **Each system should be responsible for its own problem and communicate with other systems through clear contracts.**

For example:

```text
GameManager
    → Game state and game flow

PlayerSpawner
    → Player creation

EnemySpawner
    → Enemy spawn timing and positioning

EnemyPool
    → Enemy reuse and lifecycle

EnemyHealth
    → Enemy health and death

EnemyMovement
    → Enemy movement

PlayerExperience
    → Experience and level progression

PlayerUpgradeSystem
    → Upgrade generation and application

UIManager
    → UI/gameplay communication
```

The architecture is intentionally kept simple. Patterns and abstractions are introduced when they provide a concrete benefit rather than for the sake of using patterns.

---

# 🧩 Design Principles

## Single Responsibility

Gameplay systems have focused responsibilities.

For example, `EnemyHealth` is responsible for health and death, while `EnemyPool` is responsible for reusing enemy instances.

This allows systems to evolve independently.

---

## Composition

The player is composed from multiple components:

```text
Player
├── PlayerMovement
├── PlayerAttack
├── PlayerExperience
└── PlayerUpgradeSystem
```

Instead of creating one large `Player` class containing every gameplay mechanic, each component handles a specific responsibility.

---

## Interfaces

Interfaces are used where systems need to communicate through a common contract.

For example:

```csharp
IDamageable
```

allows the attack system to deal damage without directly depending on a specific enemy health implementation.

The combat flow is therefore:

```text
PlayerAttack
      ↓
IDamageable
      ↓
EnemyHealth
```

---

## Events

C# events are used to communicate gameplay changes while keeping systems relatively decoupled.

Examples include:

```text
EnemyHealth
    → OnDied

PlayerExperience
    → OnLevelUp

PlayerUpgradeSystem
    → OnUpgradeOptionsGenerated

GameManager
    → OnPlayerSpawned
```

For example, when an enemy dies, `EnemyHealth` does not need to know what systems are interested in that event.

```text
EnemyHealth
     ↓
  OnDied
     ├──→ EnemyPool
     └──→ EnemyExperienceDrop
```

---

# ⚔️ Combat System

The player automatically searches for enemies within an attack range and damages the closest valid target.

The attack system currently handles:

- Attack range
- Damage
- Attack interval
- Enemy layer filtering
- Finding the closest enemy
- Applying damage through `IDamageable`

The system is intentionally separated from enemy implementation.

```text
PlayerAttack
     ↓
Find enemies
     ↓
Find closest target
     ↓
IDamageable
     ↓
TakeDamage()
```

The attack system also exposes methods for modifying combat values through upgrades, such as:

```text
IncreaseDamage()
IncreaseAttackSpeed()
```

---

# 👾 Enemy System

Enemies are composed of independent systems rather than one large enemy controller.

Current responsibilities include:

```text
Enemy
├── EnemyHealth
├── EnemyMovement
└── EnemyExperienceDrop
```

### Enemy Health

`EnemyHealth` implements `IDamageable`.

When health reaches zero, the enemy emits:

```text
OnDied(GameObject enemy)
```

Other systems can react to the death without the health system needing to know about them.

---

# ♻️ Enemy Object Pooling

Enemies are reused through an `EnemyPool` instead of continuously being instantiated and destroyed.

The lifecycle is:

```text
EnemyPool
    ↓
Get()
    ↓
Reset Health
    ↓
Activate
    ↓
Gameplay
    ↓
Enemy Dies
    ↓
OnDied
    ↓
Return()
    ↓
Deactivate
    ↓
Reuse
```

The pool currently uses a:

```csharp
Queue<GameObject>
```

to store available enemies.

When an enemy is retrieved:

```text
Get()
 ↓
Dequeue()
 ↓
ResetHealth()
 ↓
SetActive(true)
```

When it is returned:

```text
Return()
 ↓
SetActive(false)
 ↓
Enqueue()
```

Movement state is also cleaned up when the enemy is disabled, preventing the Rigidbody from retaining its previous velocity when the enemy is reused.

The goal here is not to build a generic pooling framework, but to solve a real performance and lifecycle problem in the game.

---

# 👹 Enemy Spawning

The `EnemySpawner` controls **when and where** enemies appear.

The spawner is responsible for:

- Spawn timing
- Spawn distance
- Random spawn direction
- Difficulty progression
- Assigning the player as the enemy target

It does **not** create enemies directly.

Instead:

```text
EnemySpawner
      ↓
EnemyPool.Get()
      ↓
Enemy
      ↓
Set position
      ↓
Set target
```

Enemies currently spawn around the player at a configurable distance using a random direction.

The spawn cooldown becomes shorter as the game progresses.

Current progression:

```text
0–30 seconds     → 2.0s
30–60 seconds    → 1.5s
60–90 seconds    → 1.0s
90+ seconds      → 0.7s
```

This is intentionally simple and will be balanced further as the gameplay develops.

---

# ⭐ Experience System

Enemies provide experience when defeated.

Different enemy types can provide different amounts of experience.

For example:

```text
Skeleton
    → 1 XP

Stronger enemy
    → 5 XP
```

The reward is configured through `EnemyExperienceDrop`.

The flow is:

```text
Enemy
  ↓
Death
  ↓
EnemyExperienceDrop
  ↓
Experience Orb
  ↓
PlayerExperience
```

The experience amount is passed to the orb when it is created.

This keeps the responsibilities separated:

```text
EnemyExperienceDrop
    → decides the reward

ExperienceOrb
    → carries the experience value

PlayerExperience
    → manages player progression
```

---

# 📈 Level Progression

The player accumulates experience until reaching the required amount for the next level.

Excess experience is preserved.

For example:

```text
Current XP:       8
Required XP:     10
Experience:      +5
```

Results in:

```text
Level Up
Remaining XP:    3
```

The system also supports multiple level-ups when a large amount of experience is gained.

The required experience currently increases progressively as the player levels up.

---

# ⬆️ Upgrade System

When the player levels up, the `PlayerUpgradeSystem` generates up to three upgrade options.

The system uses:

```text
UpgradeData
      ↓
UpgradeFactory
      ↓
IUpgrade
      ↓
PlayerUpgradeSystem
```

The available upgrades are randomly selected without duplicates within the same selection.

The system is designed to support upgrades affecting different player systems.

Current player systems that can be modified include:

- Damage
- Attack speed
- Movement

The intention is to allow upgrades to accumulate during a run.

For example:

```text
Level 2
+10% Attack Speed

Level 3
+10% Attack Speed

Level 4
+10% Attack Speed
```

Eventually resulting in a stronger player without requiring the upgrade system to directly implement the details of every possible stat.

---

# 🖥️ Upgrade Selection UI

When the player levels up, the game enters the upgrade selection state.

The UI dynamically creates the available upgrade options.

The flow is:

```text
PlayerExperience
      ↓
OnLevelUp
      ↓
PlayerUpgradeSystem
      ↓
Generate Options
      ↓
UIManager
      ↓
UpgradeSelectionUI
      ↓
Player selects an option
      ↓
Upgrade applied
      ↓
Game resumes
```

Each option is represented by an `UpgradeOptionUI`.

The UI communicates the selected option back through an event rather than directly modifying the player.

---

# ⏸️ Game State Management

The project uses a simple game state system to control the main gameplay flow.

Current states:

```text
Playing
ChoosingUpgrade
Paused
GameOver
```

For example, when the player levels up:

```text
Playing
   ↓
ChoosingUpgrade
```

The game pauses while the player chooses an upgrade.

After the selection:

```text
ChoosingUpgrade
   ↓
Playing
```

`Time.timeScale` is currently used to pause and resume gameplay.

The system is intentionally simple for the current scope.

---

# 👤 Player Spawning

The player is instantiated through a dedicated `PlayerSpawner`.

The flow is:

```text
GameManager
    ↓
PlayerSpawner
    ↓
Spawn Player
    ↓
OnPlayerSpawned
    ↓
Other systems initialize references
```

For example, the `EnemySpawner` waits for the player to be spawned before obtaining its target.

This avoids forcing the spawner to directly know how or when the player is created.

---

# 🖥️ UI Architecture

The UI is separated into specific responsibilities.

```text
UIManager
    ↓
UpgradeSelectionUI
    ↓
UpgradeOptionUI
```

`UIManager` coordinates the interaction between gameplay systems and UI.

`UpgradeSelectionUI` is responsible for displaying the available choices.

`UpgradeOptionUI` represents an individual selectable option.

This keeps gameplay logic out of the individual UI elements.

---

# 📁 Project Structure

The project is organized around gameplay responsibilities.

A simplified structure:

```text
Assets/
└── _Game/
    ├── Scripts/
    │   ├── Player/
    │   ├── Enemies/
    │   ├── Experience/
    │   ├── Upgrades/
    │   ├── UI/
    │   └── Managers/
    │
    ├── Prefabs/
    ├── ScriptableObjects/
    └── ...
```

The structure will evolve as the project grows.

The goal is to keep related systems together without creating unnecessary folder or architectural complexity.

---

# 🧠 Learning Goals

This project is also being developed as a practical way to improve C# and Unity programming skills.

Main learning goals:

- C# fundamentals
- Object-Oriented Programming
- Encapsulation
- Composition
- Inheritance
- Polymorphism
- Interfaces
- Events
- SOLID principles
- Clean Code
- Design Patterns
- Unity component architecture
- Gameplay programming
- Game state management
- Object Pooling
- ScriptableObjects
- Data-driven gameplay

A major focus is understanding **why** a particular solution is appropriate instead of applying design patterns mechanically.

---

# 🎯 Portfolio Goals

This project is intended to demonstrate practical gameplay programming skills.

The main objectives are:

- Build a complete gameplay loop
- Demonstrate Unity and C# proficiency
- Practice maintainable code
- Demonstrate object-oriented design
- Demonstrate system communication
- Apply SOLID principles where appropriate
- Build reusable gameplay systems
- Document architectural decisions
- Iterate based on actual gameplay requirements

The project is intentionally developed incrementally so that each major system can be understood, tested and documented.

---

# 🚧 Development Roadmap

## Completed

- [x] Player movement
- [x] Player spawning
- [x] Basic enemy movement
- [x] Enemy health
- [x] Damage system
- [x] Automatic player attack
- [x] Enemy death events
- [x] Experience drops
- [x] Variable enemy experience rewards
- [x] Player experience system
- [x] Level progression
- [x] Upgrade generation
- [x] Upgrade selection UI
- [x] Game state management
- [x] Pause during upgrade selection
- [x] Enemy spawning
- [x] Enemy object pooling
- [x] Enemy reuse and health reset

## Next

- [ ] Expand player statistics
- [ ] Improve upgrade system
- [ ] Make upgrades stackable
- [ ] Add more upgrade types
- [ ] Improve combat progression
- [ ] Add additional enemy types
- [ ] Improve difficulty progression
- [ ] Balance XP progression

## Planned

- [ ] Additional weapons
- [ ] Additional enemy behaviours
- [ ] Game Over flow
- [ ] More gameplay feedback
- [ ] Audio
- [ ] Visual polish
- [ ] Gameplay balancing
- [ ] Final portfolio presentation
- [ ] Playable build

---

# 📊 Current Gameplay Loop

The current systems form the following gameplay loop:

```text
                    ┌─────────────────────┐
                    │                     ↓
                 Move → Enemy → Attack → Kill
                   ↑                    │
                   │                    ↓
                   └── Upgrade ← Level Up ← XP
```

More specifically:

```text
Player
  ↓
Enemy Spawner
  ↓
Enemy Pool
  ↓
Enemies
  ↓
Player Attack
  ↓
Enemy Health
  ↓
Enemy Death
  ├──→ Enemy Pool
  │
  └──→ Experience Drop
             ↓
       Experience Orb
             ↓
      Player Experience
             ↓
          Level Up
             ↓
     Upgrade Selection
             ↓
       Apply Upgrade
             ↓
          Playing
```

This represents the current foundation of the project.

---

# 🧭 Development Philosophy

The project follows a simple development philosophy:

> **Build the gameplay first. Add architecture when the gameplay requires it.**

The project intentionally avoids unnecessary abstractions and overly generic systems.

For example, object pooling was introduced when repeated enemy creation and destruction became a relevant lifecycle/performance concern.

Similarly, interfaces, events and factories are used where they provide clear separation between systems.

The goal is not to demonstrate the maximum number of design patterns.

The goal is to demonstrate **good engineering decisions for the actual problems of the game**.

---

# 📌 Current Status

The project currently has a functional gameplay foundation including:

```text
Player
  ↓
Combat
  ↓
Enemies
  ↓
Enemy Death
  ↓
Experience
  ↓
Level Up
  ↓
Upgrade Selection
  ↓
Upgrade Applied
  ↓
Continue Playing
```

The next major development stage focuses on expanding the player's statistics and upgrade progression while keeping the architecture simple, readable and scalable.

---

## 📚 About

This is a personal Unity and C# game development project created as part of my ongoing development as a gameplay programmer.

The project combines practical game development with the study and application of:

**C# • Unity • Gameplay Programming • OOP • SOLID • Clean Code • Game Systems • Game Design**

The repository documents the development process and the evolution of the game's architecture as new gameplay requirements are introduced.
```

