# 🎮 WASD Overlay & Speedometer 🏎️

A highly optimized, server-side Counter-Strike 2 plugin built on **CounterStrikeSharp**. It displays a sleek, real-time HTML center-HUD overlay showing a player's movement keys, shift/duck states, mouse clicks, and current 2D velocity (speedometer). 

Perfect for surf, bhop, KZ, or competitive servers where players want to spectate others, analyze movement, or track their own speed gains. 🚀

---

## ✨ Showcase
<div>
  <img src="https://raw.githubusercontent.com/Benjamin-Wall/CS2-WASD/main/.github/assets/gifs/cs2-wasd-showcase.gif" alt="WASD Overlay & Speedometer Showcase"/>
  
  <p align="center">
    <em>Real-time HTML center-HUD overlay with dynamic speedometer</em>
  </p>
</div>

---

## ✨ Features

* **Real-time Key Overlay**: Displays W, A, S, D, Jump, and Duck states instantly.
* **Optional Action Tracking**: Toggable HUD rows for SHIFT and mouse clicks (M1/M2).
* **Dynamic Speedometer**: Tracks absolute 2D velocity. The numbers automatically shift color dynamically based on speed thresholds (slower speed = cool colors, higher speed = hot/fast colors).
* **RGB Rainbow Mode**: Fully customizable overlay colors, including a dynamic rainbow shifting color effect.
* **Persistent Settings**: Automatically creates a `configs/player_data.json` folder on creation. Settings save automatically and persist across server restarts and map changes.
* **Enterprise-Grade Architecture**: Designed using OOP sub-command modules for easy maintenance and zero redundant CPU overhead.

---

## 🛠️ Dependencies

Before installing, ensure your game server has the modern CS2 server management framework installed:

1. **Metamod:Source** (Development Build 2.0+)
2. **CounterStrikeSharp** (Latest stable release)

---

## 📦 Installation

1. Go to the **Releases** tab on the right side of this repository and download the latest `.zip` file (e.g., `cs2-wasd-v1.0.2.zip`).
2. Navigate to your Counter-Strike 2 server's main installation directory:
   `.../game/csgo/`
3. Extract the contents of the downloaded `.zip` file directly into that `csgo/` folder. 
4. Restart your server or type `css_plugins load cs2-wasd` in your server console to start tracking movement!
5. **Note**: On the very first run, the plugin will seamlessly generate a `/configs/` sub-folder to securely isolate player tracking data. Do not delete this folder during future updates to preserve player settings.

---

## 💬 Usage & Commands

Players interact with the plugin entirely via chat commands using the `!wasd` prefix (or `/wasd` for silent text entry).

| Command | Action | Description / Options |
| :--- | :--- | :--- |
| **`!wasd`** | Toggle Overlay | Toggles the main WASD overlay UI completely on or off. |
| **`!wasd help`** | Help Menu | Prints an interactive menu explaining all subcommands. |
| **`!wasd speed`** | Toggle Speedometer | Toggles the 2D speedometer display line at the bottom. |
| **`!wasd shift`** | Toggle Shift Row | Toggles whether the SHIFT tracking row is visible. |
| **`!wasd mouse`** | Toggle Click Row | Toggles whether Left/Right click (`M1` / `M2`) tracking is visible. |
| **`!wasd reset`** | Reset Overlay Preferences | Resets the overlays settings back to the default |
| **`!wasd color <name>`** | Change Color | Updates your overlay text color. <br> *Options:* `rainbow`, `white`, `lightblue`, `blue`, `purple`, `magenta`, `red`, `orange`, `yellow`, `green`, `aqua`, `pink`. |

---

## 📂 File Structure (For Developers)

For maintenance or packaging releases, the plugin code adheres to the following organization standard:

```text
cs2-wasd/
├── Helpers/
│   ├── FileHelper.cs       
│   └── UtilitiesHelper.cs  
├── Commands/               
│   ├── BaseSubCommand.cs   # Base command structure, all plugins must inherit this
│   ├── ColorCommand.cs     
│   ├── HelpCommand.cs     
│   ├── MouseCommand.cs    
│   ├── ResetCommand.cs    
│   ├── ShiftCommand.cs     
│   └── SpeedCommand.cs     
├── Models/
│   └── models.cs       
├── cs2-wasd.cs             # Main entry, game hooks, performance tick loop
└── configs/
    └── player_data.json    # Auto-generated server storage (Do not delete on updates)
```

---

## 🛡️ Admin Maintenance & Updates

### How to Update the Plugin
When a new version of `cs2-wasd` is released, updating it is simple and safe. Follow these steps to ensure you don't overwrite your server's player data:

1. **Download the New Release:** Grab the latest `cs2-wasd-vX.X.X.zip` from the Releases tab.
2. **Extraction:** Extract the `.zip` file directly into your server's `.../game/csgo/` directory, letting your OS overwrite the files when prompted.
3. **Configuration Safety:** Because the release zip only contains the core application binaries, extracting it will **never** overwrite or delete your `configs/player_data.json` file. Your players' custom color settings, speedometers, and layout preferences are completely safe during updates!
4. **Hot-Reloading:** You do not need to restart your entire CS2 server to apply an update. Simply run the following command in your server console to hot-reload the plugin instantly:
   ```text
   css_plugins reload cs2-wasd
   ```

---

## 📷 Additional Media
<div>
  <img src="https://raw.githubusercontent.com/Benjamin-Wall/CS2-WASD/main/.github/assets/imgs/cs2-wasd-default.png" alt="WASD Overlay & Speedometer Showcase"/>
  
  <p align="center">
    <em>Default WASD overlay</em>
  </p>
</div>

<div>
  <img src="https://raw.githubusercontent.com/Benjamin-Wall/CS2-WASD/main/.github/assets/imgs/cs2-wasd-full.png" alt="WASD Overlay with all settings enabled"/>
  
  <p align="center">
    <em>WASD Overlay with all settings enabled</em>
  </p>
</div>

<div>
  <img src="https://raw.githubusercontent.com/Benjamin-Wall/CS2-WASD/main/.github/assets/imgs/cs2-wasd-help.png" alt="WASD Overlay help menu"/>
  
  <p align="center">
    <em>WASD Overlay help menu</em>
  </p>
</div>