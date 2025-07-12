# 🌍 GAIA's TRIAL : THE FINAL HOUR – VR Escape Room for Environmental Sustainability

**A cross-platform immersive experience built using Unity, designed to educate and engage users on key Sustainable Development Goals (SDGs) through interactive escape room gameplay.**

---

## 📁 Repository Notice

This repository contains **only the core C# script files** used in the development of the GAIA's TRIAL project.

Due to platform limitations, the **complete Unity project including assets, scenes, models, and build files** could not be uploaded. The full project contains large 3D models, textures, animations, and other asset files that significantly exceed repository size constraints.

---

## Gameplay Controls

- `H` - Move to home living room from intro
- `Space bar` - Remove the TV plug to reduce phantom energy consumption
- `Q` - Switch off the laptop to restore sustainability back to Dark Grid
- `E` - Move E-waste into black bin 
- `R` - Move Non-biodegradable & Non-recyclable waste into red bin 
- `T` - Move Non-biodegradable & recyclable waste into blue bin 
- `Y` - Move Biodegradable waste into green bin 
- `F` - Move to next room - Forest
- `B` - Put out the forest fire using the bucket of water for climate action
- `U` - Lastly, move to the final judge, the very Earth personified and answer sustainability questions
- `N/M` - N for Option A and M for Option B

---

## 📂 Included

- `ChoiceSequenceManager.cs` – Controls ordered user choices and consequences in puzzles  
- `CrossUniverse.cs` – Handles transitions between different themed worlds/zones  
- `CrossHome.cs` – Manages logic specific to returning to the main hub (living room) 
- `CrossPlatformSceneLoader.cs` – Detects platform and loads appropriate build (WebGL/VR)  
- `SlideDrop.cs` – Enables puzzle interactions like placing or sliding objects  
- `Teleporter.cs` – Manages player teleportation mechanics between room zones  
- `ObjectCollision.cs` – Triggers events upon collision with interactive objects  
- `ObjectDisappearance.cs` – Controls visibility or removal of objects upon interaction  
- `PlatformSwitcher.cs` – Adapts controls/UI for Meta Quest or WebGL platforms
- Other helper scripts for interaction, animation triggers, and environmental feedback

---

## 🔧 Technologies Used

- **Unity (C#)** – Main game engine where all assets, interactions, and room logic are built and managed.
- **Meta Quest 2 (XR Toolkit)** – Interact with the escape room in a virtual environment.
- **C# scripts** - Powers the core game logic, handling player movement, puzzle interactions, and room transitions.
- **WebGL** – Lightweight web deployment version  
- **Blender** – 3D modeling of environments and props  
- **Figma** – UI/UX prototyping and asset design

---

## 📌 Note

To explore the full project, please contact the team for access to the complete Unity project folder, which includes assets and build configurations.

