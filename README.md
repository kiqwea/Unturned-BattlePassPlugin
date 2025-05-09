BattlePass plugin adds a battle pass system to Unturned, allowing players to receive rewards for reaching certain levels.

## Features

- Level system with rewards
- Player progress saving
- Game UI integration
- Level reward system
- Automatic progress saving

## Requirements

- Unturned 3.x
- RocketMod
- .NET Framework 4.8

## Installation

1. Download the latest version of the plugin from the Releases section
2. Extract the .dll file from the compiled project
3. Place the .dll file in your server's `Rocket/Plugins` folder
4. Restart the server

## Configuration

### Reward Configuration

Rewards are configured in the `PassConfiguration.cs` file. For each reward, you need to specify:

```csharp
list.Add(new PassList(
    "item_1",           // Unique reward name
    17,                 // In-game item ID
    5,                  // Item amount
    "image_url",        // Reward image URL
    "used_image_url",   // Used reward image URL
    "locked_image_url", // Locked reward image URL
    1                   // Level at which the reward is given
));
```

### Images

1. Upload images to any image hosting service (e.g., imgur.com)
2. Copy direct links to the images
3. Insert the links into the reward configuration

## Commands

- `/bp` or `/pass` - open battle pass menu
- `/bp info` - show information about received rewards

## Level System

- Each level requires 230 experience
- Experience is earned through in-game actions
- Players receive a notification when reaching a new level
- Rewards can only be received once

## Data Storage

Player progress data is saved in the `BattlePass.txt` file in the server's root folder.

**2023-2024 created**
